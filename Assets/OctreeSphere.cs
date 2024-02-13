using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctreeSphere : MonoBehaviour
{
    public int depth;
    public float radius;
    private Vector3[] cubes;
    private float cubeSize;
    // Start is called before the first frame update
    void Start()
    {
        if (depth == 1)
        {
            cubes  = new Vector3[1];
            cubes[0] = gameObject.transform.position;
            cubeSize = radius * 2;
        }
        else
        {
            float cubePerSide = Mathf.Pow(2, depth - 1);
            Debug.Log(cubePerSide);
            cubes = new Vector3[(int)Mathf.Pow(8, depth - 1)];
            cubeSize = (radius * 2) / cubePerSide;
            Debug.Log(cubeSize);
            int counter = 0;
            for (int i = 0; i < cubePerSide; i++)
            {
                for (int j = 0; j < cubePerSide; j++)
                {
                    for (int k = 0; k < cubePerSide; k++)
                    {
                        float posX = -radius + cubeSize * i;
                        float posY = radius - cubeSize * j;
                        float posZ = radius - cubeSize * k;
                        //Debug.Log($"{posX} {posY} {posZ}");
                        cubes[counter++] = new Vector3(posX + (cubeSize / 2), posY - (cubeSize / 2), posZ - (cubeSize / 2));
                    }
                }
            }
        }
        Debug.Log(cubes.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        if (cubes != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Vector3 initPos = gameObject.transform.position;
            for (int i = 0; i < cubes.Length; i++)
            {
                Vector3 pos = cubes[i];
                if (pos.x * pos.x + pos.y * pos.y + pos.z * pos.z - radius * radius < 0)
                    Gizmos.DrawCube(cubes[i] + initPos, new Vector3(cubeSize, cubeSize, cubeSize));
            }
        }
    }
}
