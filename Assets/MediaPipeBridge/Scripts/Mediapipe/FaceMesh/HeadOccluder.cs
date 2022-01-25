using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MediaPipe;
using UnityEngine;

public class HeadOccluder : MonoBehaviour{
  MeshFilter meshFilter;
  private Mesh mesh;
  private FaceMeshData[] results;
  private Transform volumeOccluder;
  [Range(0.1f,2)]
  public float volumeOccluderScale = 1.7f;
  [Range(-5, 5)]
  public float volumeOccluderDistance = 1.7f;

  void Start() {
    meshFilter = GetComponent<MeshFilter>();
    mesh = meshFilter.mesh;
    mesh.triangles = mesh.triangles.Reverse().ToArray();
    volumeOccluder = transform.GetChild(0);
  }

  private void Update() {
    results = MediaPipeBridge.GetResults<FaceMeshData>(MediaPipeModule.Category.FaceMesh.ToString());
    foreach(FaceMeshData face in results) {
      mesh.vertices = face.points;
      volumeOccluder.position = face.bound.center - (face.forward * volumeOccluderDistance);
      Vector3 eulerRot = face.rotation.eulerAngles;
      volumeOccluder.localScale = Vector3.one * volumeOccluderScale * face.uniformScale;
      volumeOccluder.rotation = Quaternion.Euler(-eulerRot.x - 6.796f, eulerRot.y + 180, -eulerRot.z);
    }
  }
}
