using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class FaceMeshProcessor : LandMarksProcessor {

    public FaceMeshProcessor() { }
    public FaceMeshData faceData;

    public override void OnPointsDeserialized(Vector3[] points) {
      faceData = new FaceMeshData(points);
    }
  }
}
