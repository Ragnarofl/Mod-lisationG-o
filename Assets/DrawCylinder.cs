using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawCylinder : MonoBehaviour
{
    Mesh msh;
    public Material mat;
    public int radius;
    public int height;
    public int meridian;
    private Vector3 origin;
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        msh = new Mesh();
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();
        vertices = new Vector3[meridian * 2 + 2];
        triangles = new int[meridian * 2 * 6 + (meridian * 2)];
        int counter = 0;

        for (int i = 0; i < meridian; i++) {
            double x = Math.Cos(2 * Math.PI * i / meridian);
            double y = Math.Sin(2 * Math.PI * i / meridian);
            vertices[i] = new Vector3((float)x, (float)y, (float)-height / 2);
        }
        for (int i = 0; i < meridian; i++) {
            double x = Math.Cos(2 * Math.PI * i / meridian);
            double y = Math.Sin(2 * Math.PI * i / meridian);
            vertices[i + meridian] = new Vector3((float)x, (float)y, (float)height / 2);
        }
        vertices[meridian * 2] = new Vector3(0, 0, -height / 2);
        vertices[meridian * 2 + 1] = new Vector3(0, 0, height / 2);

        for (int i = 0; i < meridian - 1; i++) {
            triangles[counter++] = i;                //0
            triangles[counter++] = i + 1;            //1
            triangles[counter++] = i + meridian;     //2

            triangles[counter++] = i + meridian;     //2
            triangles[counter++] = i + 1;            //1
            triangles[counter++] = i + 1 + meridian; //3
        }
        triangles[counter++] = meridian - 1;
        triangles[counter++] = 0;
        triangles[counter++] = meridian - 1 + meridian;

        triangles[counter++] = meridian - 1 + meridian;
        triangles[counter++] = 0;
        triangles[counter++] = 0 + meridian;

        for (int i = 0; i < meridian; i++) {
            triangles[counter++] = i;
            triangles[counter++] = meridian * 2;
            triangles[counter++] = i + 1;
        }
        triangles[counter++] = meridian - 1;
        triangles[counter++] = meridian * 2;
        triangles[counter++] = 0;

        for (int i = 0; i < meridian; i++)
        {
            triangles[counter++] = meridian + i;
            triangles[counter++] = meridian + i + 1;
            triangles[counter++] = meridian * 2 + 1;
        }
        triangles[counter++] = meridian * 2 - 1;
        triangles[counter++] = meridian;
        triangles[counter++] = meridian * 2 + 1;

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < meridian * 2 + 2; i++) {
            Gizmos.DrawSphere(vertices[i], 0.3f);
        }
    }*/
}
