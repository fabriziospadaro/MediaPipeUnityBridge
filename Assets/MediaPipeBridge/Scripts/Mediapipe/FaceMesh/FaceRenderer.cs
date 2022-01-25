using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MediaPipe;
using System.Linq;
public class FaceRenderer : MonoBehaviour{
  MeshFilter meshFilter;
  public Mesh mesh;
  public FaceMeshData[] face;

  void Start(){
    meshFilter = GetComponent<MeshFilter>();
    mesh = meshFilter.mesh;
    mesh.triangles = mesh.triangles.Reverse().ToArray();
  }

  private void Update() {
    /*TODO: COMPLETLY BROKEN -> INSTANTIATE MULTIPLE FACEMESH IN THIS SCRIPT
    face = (FaceMeshData)MediaPipeBridge.GetResults(MediaPipeModule.Category.FaceMesh.ToString());
    if(face != null)
      mesh.vertices = face.points;
    */
  }
}

