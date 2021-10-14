  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class PoseProcessor : LandMarksProcessor {

    public PoseProcessor() { }

    public override void OnPointsDeserialized(Vector3[] points) {
      data = new PoseData(points);
    }
  }
}
