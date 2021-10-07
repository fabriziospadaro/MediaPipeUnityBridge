using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MediaPipe;
using System.Linq;
public class FaceRenderer : MonoBehaviour{
  public Vector3[] ovalPoints;
  MeshFilter meshFilter;
  public Mesh mesh;
  public FaceMeshData face;

  void Start(){
    meshFilter = GetComponent<MeshFilter>();
    mesh = meshFilter.mesh;
    ovalPoints = new Vector3[FaceMesh.OVAL_INDICES.Length];
    mesh.triangles = mesh.triangles.Reverse().ToArray();
  }

  private void Update() {
    face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());
    if(face != null)
      mesh.vertices = face.points;
  }

  void DrawBBQuad(Bounds b) {
    Vector3[] vertices = new Vector3[4]
    {
      new Vector3(b.min.x,b.min.y,b.max.z),
      new Vector3(b.max.x,b.min.y,b.max.z),
      new Vector3(b.max.x, b.max.y, b.min.z),
      new Vector3(b.min.x, b.max.y, b.min.z)
    };
    mesh.vertices = vertices;

    int[] tris = new int[6]{
      // lower left triangle
      0, 2, 1,
      // upper right triangle
      2, 0, 3
    };

    mesh.triangles = tris;

    Vector3[] normals = new Vector3[4]
    {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
    };
    mesh.normals = normals;

    Vector2[] uv = new Vector2[4]
    {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
    };
    mesh.uv = uv;

    meshFilter.mesh = mesh;
  }
}

