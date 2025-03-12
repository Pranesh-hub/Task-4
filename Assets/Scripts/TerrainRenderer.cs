using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TerrainRenderer : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public int xsize = 40;
    public int zsize = 40;
    public float noiseScale = 0.4f;
    public float amplitude = 40f;
    MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshCollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CreateShape();
        UpdateMesh();
    }
    void CreateShape(){
        // vertices = new Vector3[]{
        //     new Vector3(0,0,0),
        //     new Vector3(0,0,1),
        //     new Vector3(1,0,0),
        //     new Vector3(1,0,1)
        // };
        // triangles[0] = 0;
        // triangles[1] = 1;
        // triangles[2] = 2;
        // triangles[3] = 2;
        // triangles[4] = 1;
        // triangles[5] = 3;
        vertices = new Vector3[(xsize+1)*(zsize+1)];
        for(int z = 0, i = 0; z < zsize; z++){
            for(int x = 0; x < xsize; x++){
                float y = Mathf.PerlinNoise(x*noiseScale,z*noiseScale) * amplitude;
                vertices[i] = new Vector3(x,y,z);
                i++;
            }
        }
        // triangles = new int[]{
        //     0,1,2,
        //     2,1,3
        // };
        int vert = 0;
        int tris = 0;
        triangles = new int[xsize*zsize*6];
        for(int x = 0; x < xsize; x++){
            for(int z = 0; z < zsize; z++){
                triangles[tris+0] = vert+0;
                triangles[tris+1] = vert+xsize+1;
                triangles[tris+2] = vert+1;
                triangles[tris+3] = vert+1;
                triangles[tris+4] = vert+xsize+1;
                triangles[tris+5] = vert+xsize+2;
                vert++;
                tris += 6;
            }
            //vert++;
        }
    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    // public void OnDrawGizmos(){
    //     if(vertices == null){
    //         return ;
    //     }
    //     for(int i = 0; i < vertices.Length; i++){
    //         Gizmos.DrawSphere(vertices[i],0.1f);
    //     }
    // }
    
}
