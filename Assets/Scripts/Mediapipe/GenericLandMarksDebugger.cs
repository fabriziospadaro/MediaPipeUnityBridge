using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class GenericLandMarksDebugger : MonoBehaviour {
    public MediaPipeModule.Category category;

    private void OnDrawGizmos() {
      if(Application.isPlaying) {
        var data = MediaPipeBridge.GetModule(category.ToString()).ProcessorData;
        foreach(Vector3 v in data.points)
          Gizmos.DrawWireSphere(v, 0.06f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(data.bound.center, data.bound.size);
      }
  }
  }
}
