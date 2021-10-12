using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class HandsProcessor : LandMarksProcessor {

    public HandsProcessor() { }

    public override void OnPointsDeserialized(Vector3[] points) {
      data = new HandsData(points);
    }
  }
}
