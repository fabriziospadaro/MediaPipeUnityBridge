using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MediaPipe {
  public class ObjectronDebugger : MonoBehaviour {
    public Transform orientationHelper;

    private void OnDrawGizmos() {
#if UNITY_EDITOR
      if(Application.isPlaying) {
        ObjectronData[] results = MediaPipeBridge.GetResults<ObjectronData>(MediaPipeModule.Category.Objectron.ToString());
        foreach(ObjectronData objectron in results) {
          Gizmos.color = Color.yellow;

          for(int i = 0; i < ObjectronData.Constants.BOX_CONNECTIONS.Length; i += 2) {
            var c = ObjectronData.Constants.BOX_CONNECTIONS[i];
            var c2 = ObjectronData.Constants.BOX_CONNECTIONS[i + 1];
            Gizmos.DrawLine(objectron.points[c], objectron.points[c2]);
          }

          Gizmos.color = Color.red;

          for(int i = 0; i < objectron.points.Length; i++) {
            Handles.Label(objectron.points[i], i.ToString());
          }
        }
      }
#endif
    }
  }
}
