using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawMultiPlane : MonoBehaviour
{
    Mesh msh;
    public Material mat;
    public int row;
    public int col;
    private Vector3 origin;
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        msh = new Mesh();
        vertices = new Vector3[(row + 1) * (col + 1)];
        triangles = new int[2 * row * col * 6];
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
        gameObject.AddComponent<MeshRenderer>();
        origin = gameObject.transform.position;

        for (int y = 0; y < row; y++) { 
            for (int x = 0; x < col; x++) {
                vertices.Append(new Vector3(origin.x + x, origin.y + y, 0));
            }
        }
        for (int i = 0; i < row * col; i++) {
            triangles.Append(0 + i * (col + 1)); //0
            triangles.Append((col + 1) * i); //1
            triangles.Append((col + 1) * i); //2

            triangles.Append((col + 1) * i); //2
            triangles.Append(1 + i * (col + 1)); //1
            triangles.Append(3 + i * (col + 1)); //3
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
