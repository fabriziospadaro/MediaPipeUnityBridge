using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class FaceMeshDebugger : MonoBehaviour {
    private void Start() {
    }

    private void OnRenderObject() {
      if(Application.isPlaying) {
        FaceMeshData face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());

        if(face != null && face.points.Length > 0) 
          for(int i = 0; i < FaceMeshData.Constants.SILHOUETTE.Length; i++) 
              Draw.Line(face.points[FaceMeshData.Constants.SILHOUETTE[i]], face.points[FaceMeshData.Constants.SILHOUETTE[(i + 1) % FaceMeshData.Constants.SILHOUETTE.Length]]);
          
        
      }
    }
  }
}
