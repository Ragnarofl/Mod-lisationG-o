using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class DrawSphere : MonoBehaviour
{
    Mesh msh;
    public Material mat;
    public int radius;
    public int meridian;
    public int parallels;
    private Vector3 origin;
    private Vector3[] vertices;
    private int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        msh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        vertices = new Vector3[meridian * parallels + 2];
        //triangles = new int[meridian * 2 * 6 * (parallels - 1) + (meridian * 2)];
        triangles = new int[meridian * parallels * 2 * 3 + (meridian * 2)];

        for (int j = 0; j < parallels; j++)
        {
            float phi = ((180 / (parallels + 1)) * (j + 1)) * (MathF.PI / 180);
            for (int i = 0; i < meridian; i++)
            {
                //float theta = 2 * Mathf.PI * i / meridian;
                float theta = 360 / meridian * (Mathf.PI / 180) * i;
                float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
                float y = radius * Mathf.Cos(phi);
                float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
                vertices[i + meridian * j] = new Vector3(x, y, z);
                Debug.Log($"{x} {y} {z}");
            }
        }
        vertices[meridian * parallels] = new Vector3(0, radius, 0);
        vertices[meridian * parallels + 1] = new Vector3(0, -radius, 0);

        int counter = 0;
        //Drawing sides
        for (int j = 0; j < parallels - 1; j++)
        {
            for (int i = 0; i < meridian - 1; i++)
            {
                triangles[counter++] = i + meridian * j;                //0
                triangles[counter++] = i + 1 + meridian * j;            //1
                triangles[counter++] = i + meridian * (j + 1);          //2

                triangles[counter++] = i + meridian * (j + 1);          //2
                triangles[counter++] = i + 1 + meridian * j;            //1
                triangles[counter++] = i + 1 + meridian * (j + 1);      //3
            }
            triangles[counter++] = meridian - 1 + meridian * j;
            triangles[counter++] = 0 + meridian * j;
            triangles[counter++] = meridian - 1 + meridian * (j + 1);

            triangles[counter++] = meridian - 1 + meridian * (j + 1);
            triangles[counter++] = 0 + meridian * j;
            triangles[counter++] = 0 + meridian * (j + 1);
        }

        //Closing top-bottom
        for (int i = 0; i < meridian; i++)
        {
            triangles[counter++] = i;
            triangles[counter++] = meridian * parallels;
            triangles[counter++] = i + 1;
        }
        triangles[counter++] = meridian - 1;
        triangles[counter++] = meridian * parallels;
        triangles[counter++] = 0;

        for (int i = 0; i < meridian; i++)
        {
            triangles[counter++] = meridian * (parallels - 1) + i;
            triangles[counter++] = meridian * (parallels - 1) + i + 1;
            triangles[counter++] = meridian * parallels + 1;
        }
        triangles[counter++] = meridian * parallels - 1;
        triangles[counter++] = meridian * (parallels - 1);
        triangles[counter++] = meridian * parallels + 1;

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;
        Debug.Log(triangles.Length);
        Debug.Log(counter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (vertices != null)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < vertices.Length; i++)
            {
                Gizmos.DrawSphere(vertices[i], 0.2f);
            }
        }
    }
}
