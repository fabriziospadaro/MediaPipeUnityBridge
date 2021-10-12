using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class FaceMeshDebugger : MonoBehaviour {

    private void OnRenderObject() {
      if(Application.isPlaying) {
        FaceMeshData face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());

        if(face != null && face.points.Length > 0) 
          for(int i = 0; i< FaceMesh.OVAL_INDICES.Length; i++) 
              Draw.Line(face.points[FaceMesh.OVAL_INDICES[i]], face.points[FaceMesh.OVAL_INDICES[(i + 1) % FaceMesh.OVAL_INDICES.Length]]);
          
        
      }
    }
  }
}
