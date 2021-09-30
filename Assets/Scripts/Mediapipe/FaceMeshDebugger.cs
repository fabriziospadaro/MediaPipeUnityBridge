using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace MediaPipe {
  public class FaceMeshDebugger : MonoBehaviour {

    private void OnDrawGizmos() {
#if UNITY_EDITOR
      FaceMeshData face = FaceManager.face;
      if(face != null) {
        Vector3[] ovalPoints = new Vector3[FaceMesh.OVAL_INDICES.Length];
        int j = 0;
        foreach(int i in FaceMesh.OVAL_INDICES) {
          ovalPoints[j] = face.points[i];
          j++;
        }
        Handles.DrawLines(ovalPoints);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(face.bound.center, face.bound.center + face.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(face.bound.center, face.bound.center + face.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(face.bound.center, face.bound.center + face.right);
      }
#endif
    }
  }
}
