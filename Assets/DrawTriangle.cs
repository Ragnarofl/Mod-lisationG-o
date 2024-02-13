using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTriangle : MonoBehaviour
{
    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite �tre visualis�
        gameObject.AddComponent<MeshRenderer>();

        Vector3[] vertices = new Vector3[4];            // Cr�ation des structures de donn�es qui accueilleront sommets et  triangles
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 0, 0);            // Remplissage de la structure sommet 
        vertices[1] = new Vector3(1, 0, 0);
        vertices[2] = new Vector3(0, 1, 0);
        vertices[3] = new Vector3(1, 1, 0);

        triangles[0] = 0;                               // Remplissage de la structure triangle. Les sommets sont repr�sent�s par leurs indices
        triangles[1] = 1;                               // les triangles sont repr�sent�s par trois indices (et sont mis bout � bout)
        triangles[2] = 2;
        triangles[3] = 2;                               // Remplissage de la structure triangle. Les sommets sont repr�sent�s par leurs indices
        triangles[4] = 1;                               // les triangles sont repr�sent�s par trois indices (et sont mis bout � bout)
        triangles[5] = 3;

        Mesh msh = new Mesh();                          // Cr�ation et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du mat�riel
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
