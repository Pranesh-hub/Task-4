using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Mesh_gen2 : MonoBehaviour
{
    [System.Serializable]
    public struct TerrainType{
        public string name;
        public float height;
        public Color color;
    }
    
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;
    public int xsize = 100;
    public int zsize = 100;
    public float noiseScale = 0.3f;
    public float amplitude = 2f;
    MeshCollider meshCollider;
    public TerrainType[] region;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();
        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xsize + 1) * (zsize + 1)];
        colors = new Color[vertices.Length];
        for (int z = 0, i = 0; z <= zsize; z++)
        {
            for (int x = 0; x <= xsize; x++)
            {
                float y = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) * amplitude;
                vertices[i] = new Vector3(x, y, z);
                for(int j = 0; j<region.Length; j++){
                    if(y<=region[j].height){
                        colors[i] = region[j].color;
                        break;
                    }
                }
                
                i++;
            }
        }

        triangles = new int[xsize * zsize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zsize; z++)
        {
            for (int x = 0; x < xsize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xsize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xsize + 1;
                triangles[tris + 5] = vert + xsize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    
}
