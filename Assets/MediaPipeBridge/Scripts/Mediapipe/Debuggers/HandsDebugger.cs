using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class HandsDebugger : MonoBehaviour {
    private LandMarkPointsSmoother landMarkPointsSmoother;

    private void Start() {
      landMarkPointsSmoother = new LandMarkPointsSmoother(20);
    }
    private void OnRenderObject() {
      if(Application.isPlaying) {
        HandsData[] handsResult = MediaPipeBridge.GetResults<HandsData>(MediaPipeModule.Category.Hands.ToString());
        foreach(HandsData hand in handsResult) {
          landMarkPointsSmoother.Step(hand.points, Time.deltaTime);

          foreach(int[] connections in HandsData.Constants.CONNECTIONS)
            for(int i = 0; i < connections.Length; i++)
              if(i + 1 < connections.Length)
                Draw.Line(landMarkPointsSmoother.points[connections[i]], landMarkPointsSmoother.points[connections[i + 1]], Color.yellow);
        }
      }
    }
  }
}
