using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VoxelManager : MonoBehaviour
{
    public int depth;
    public float cubeSize;
    public bool intersection;
    public bool xor;
    private Vector3[] cubes;
    private float stepX;
    private float stepY;
    private float stepZ;
    private float radX;
    private float radY;
    private float radZ;
    private float cubePerSideX, cubePerSideY, cubePerSideZ;
    private Transform children;
    // Start is called before the first frame update
    void Start()
    {
        float minX, maxX, minY, maxY, minZ, maxZ;
        minX = maxX = minY = maxY = minZ = maxZ = 0;
        children = gameObject.GetComponentInChildren<Transform>();
        foreach (Transform child in children) {
            Vector3 pos = child.localPosition;
            OctreeSphere sphere = child.GetComponent<OctreeSphere>();
            if (minX > pos.x - sphere.radius) minX = pos.x - sphere.radius;
            if (maxX < pos.x + sphere.radius) maxX = pos.x + sphere.radius;
            if (minY > pos.y - sphere.radius) minY = pos.y - sphere.radius;
            if (maxY < pos.y + sphere.radius) maxY = pos.y + sphere.radius;
            if (minZ > pos.z - sphere.radius) minZ = pos.z - sphere.radius;
            if (maxZ < pos.z + sphere.radius) maxZ = pos.z + sphere.radius;
        }
        Vector3 min = new Vector3(minX, minY, minZ);
        Vector3 max = new Vector3(maxX, maxY, maxZ);
        Debug.Log(min);
        Debug.Log(max);
        float cubePerSide = Mathf.Pow(2, depth - 1);
        cubePerSideX = (max.x - min.x) / cubeSize;
        cubePerSideY = (max.y - min.y) / cubeSize;
        cubePerSideZ = (max.z - min.z) / cubeSize;
        stepX = (max.x - min.x) / cubePerSide;
        stepY = (max.y - min.y) / cubePerSide;
        stepZ = (max.z - min.z) / cubePerSide;
        radX = Mathf.Abs(max.x - min.x) / 2;
        radY = Mathf.Abs(max.y - min.y) / 2;
        radZ = Mathf.Abs(max.z - min.z) / 2;
        Debug.Log($"{radX} {radY} {radZ}");
        makeOctree2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeOctree()
    {
        if (depth == 1)
        {
            cubes = new Vector3[1];
            cubes[0] = gameObject.transform.position;
        } else {
            float cubePerSide = Mathf.Pow(2, depth - 1);
            Debug.Log(cubePerSide);
            cubes = new Vector3[(int)Mathf.Pow(8, depth - 1)];
            //cubes = new Vector3[(int)(cubePerSideX * cubePerSideY * cubePerSideZ)];
            int counter = 0;
            for (int i = 0; i < cubePerSide; i++)
                for (int j = 0; j < cubePerSide; j++)
                    for (int k = 0; k < cubePerSide; k++) {
                        float posX = -radX + stepX * i;
                        float posY = radY - stepY * j;
                        float posZ = radZ - stepZ * k;
                        Debug.Log($"{posX} {posY} {posZ}");
                        cubes[counter++] = new Vector3(posX + (stepX / 2), posY - (stepY / 2), posZ - (stepZ / 2));
                    }
        }
    }

    void makeOctree2()
    {
        if (depth == 1) {
            cubes = new Vector3[1];
            cubes[0] = gameObject.transform.position;
        } else {
            cubes = new Vector3[(int)(cubePerSideX * cubePerSideY * cubePerSideZ)];
            int counter = 0;
            for (int i = 0; i < cubePerSideX; i++)
                for (int j = 0; j < cubePerSideY; j++)
                    for (int k = 0; k < cubePerSideZ; k++) {
                        float posX = -radX + cubeSize * i;
                        float posY = radY - cubeSize * j;
                        float posZ = radZ - cubeSize * k;
                        Debug.Log($"{posX} {posY} {posZ}");
                        cubes[counter++] = new Vector3(posX + (cubeSize / 2), posY - (cubeSize / 2), posZ - (cubeSize / 2));
                    }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        if (cubes != null) {
            Gizmos.color = Color.red;

            Vector3 initPos = gameObject.transform.position;
            for (int i = 0; i < cubes.Length; i++) {
                int count = 0;
                Vector3 pos = cubes[i];
                foreach (Transform child in children) {
                    Vector3 childPos = child.localPosition;
                    float radius = child.GetComponent<OctreeSphere>().radius;
                    if (Mathf.Pow(cubes[i].x - childPos.x, 2) + Mathf.Pow(cubes[i].y - childPos.y, 2) + Mathf.Pow(cubes[i].z - childPos.z, 2) - radius * radius < 0) {
                        if (!intersection && !xor) {
                            Gizmos.DrawCube(cubes[i] + initPos, new Vector3(cubeSize, cubeSize, cubeSize));
                            break;
                        } else
                            count++;
                    }
                }
                if (intersection && !xor && count > 1)
                    Gizmos.DrawCube(cubes[i] + initPos, new Vector3(cubeSize, cubeSize, cubeSize));
                else if (!intersection && xor && count == 1)
                    Gizmos.DrawCube(cubes[i] + initPos, new Vector3(cubeSize, cubeSize, cubeSize));
            }
        }
    }
}
