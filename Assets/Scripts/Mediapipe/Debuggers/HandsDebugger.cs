using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class HandsDebugger : MonoBehaviour {

    private void OnRenderObject() {
      if(Application.isPlaying) {
        HandsData hands = (HandsData)MediaPipeBridge.GetData(MediaPipeModule.Category.Hands.ToString());
        foreach(int[] connections in HandsData.Constants.CONNECTIONS)
          for(int i = 0; i < connections.Length; i++)
            if(i+1 < connections.Length)
              Draw.Line(hands.points[connections[i]],hands.points[connections[i + 1]],Color.yellow);
      }
    }
  }
}
