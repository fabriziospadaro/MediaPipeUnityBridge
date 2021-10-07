using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace MediaPipe {
  public class FaceMeshDebugger : MonoBehaviour {

    private void OnDrawGizmos() {
#if UNITY_EDITOR
      FaceMeshData face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());
      if(face != null && face.points.Length > 0) {
        Vector3[] ovalPoints = new Vector3[FaceMesh.OVAL_INDICES.Length];
        int j = 0;
        foreach(int i in FaceMesh.OVAL_INDICES) {
          ovalPoints[j] = face.points[i];
          j++;
        }
        Handles.DrawLines(ovalPoints);
      }
#endif
    }
  }
}
