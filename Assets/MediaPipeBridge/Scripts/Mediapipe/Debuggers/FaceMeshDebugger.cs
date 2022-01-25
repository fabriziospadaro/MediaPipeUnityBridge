using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class FaceMeshDebugger : MonoBehaviour {
    public bool debugIndices;
    private void Start() {
    }

    private void OnRenderObject() {
      if(Application.isPlaying) {
        FaceMeshData[] results = MediaPipeBridge.GetResults<FaceMeshData>(MediaPipeModule.Category.FaceMesh.ToString());
        foreach(FaceMeshData face in results) {
          if(face != null && face.points.Length > 0)
            for(int i = 0; i < FaceMeshData.Constants.SILHOUETTE.Length; i++)
              Draw.Line(face.points[FaceMeshData.Constants.SILHOUETTE[i]], face.points[FaceMeshData.Constants.SILHOUETTE[(i + 1) % FaceMeshData.Constants.SILHOUETTE.Length]]);
        }
      }
    }


    private void OnDrawGizmos() {
      #if UNITY_EDITOR
      if(Application.isEditor && Application.isPlaying) {
        FaceMeshData[] results = MediaPipeBridge.GetResults<FaceMeshData>(MediaPipeModule.Category.FaceMesh.ToString());
        foreach(FaceMeshData face in results) {
          if(debugIndices)
            for(int i = 0; i < (int)MediaPipeModule.Category.FaceMesh; i++)
              Handles.Label(face.points[i], i.ToString());
        }
      }
      #endif
    }
  }
}
