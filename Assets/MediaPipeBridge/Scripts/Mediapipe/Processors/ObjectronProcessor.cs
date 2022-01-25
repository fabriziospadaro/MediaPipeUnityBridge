using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class ObjectronProcessor : LandMarksProcessor {

    public ObjectronProcessor() { }

    public override void OnPointsDeserialized(Vector3[] points,int i) {
      Set(i, new ObjectronData(points));
    }
  }
}
