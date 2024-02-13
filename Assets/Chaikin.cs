using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Chaikin : MonoBehaviour
{
    public int depth;
    public bool isOpen;
    public Vector3[] positions;
    private Vector3[] newPositions;
    // Start is called before the first frame update
    void Start()
    {
        if (depth != 0 && positions != null)
        {
            for (int j = 0; j < depth; j++) {
                int i = 0;
                int count = 0;
                newPositions = new Vector3[positions.Length * 2];
                if (isOpen)
                    newPositions[count++] = positions[i];

                for (; i < positions.Length - 1; i++) {
                    newPositions[count++] = positions[i] * (3.0f/4.0f) + positions[i + 1] * (1.0f/4.0f);
                    newPositions[count++] = positions[i] * (1.0f/4.0f) + positions[i + 1] * (3.0f/4.0f);
                }

                if (isOpen)
                    newPositions[count++] = positions[i++];
                positions = newPositions;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        if (positions != null)
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < positions.Length; i++)
            {
                Gizmos.DrawCube(positions[i], new Vector3(0.1f, 0.1f, 0.1f));
            }
            Handles.DrawPolyLine(positions);
        }
    }
}
