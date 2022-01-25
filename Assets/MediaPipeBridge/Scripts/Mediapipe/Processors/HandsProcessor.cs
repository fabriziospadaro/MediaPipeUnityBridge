using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class HandsProcessor : LandMarksProcessor {

    public HandsProcessor() { }

    public override void OnPointsDeserialized(Vector3[] points,int i) {
      Set(i, new HandsData(points));
    }

    public override void PostProcess() {
      if(results != null) {
        for(int i = 0; i < results.Length; i++) {
          for(int j = 0; j < results.Length; j++) {
            if(i != j && results[i]?.visibleState == true && results[j]?.visibleState == true) {
              Debug.Log(Vector2.Distance(results[i].bound.center, results[j].bound.center));
              if(Vector2.Distance(results[i].bound.center, results[j].bound.center) < 0.5)
                results[i] = null;
            }
          }
        }
      }
    }
  }
}
