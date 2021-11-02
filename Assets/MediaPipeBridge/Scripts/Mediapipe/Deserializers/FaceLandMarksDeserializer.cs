using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class FaceLandMarksDeserializer : LandMarksDeserializer {
    public FaceLandMarksDeserializer() { }
    public override void OnLandmarkCollected(string serializedPoints) {
      string[] dataChunk = serializedPoints.Split(new char[] { '*' });
      Vector3[] points = new Vector3[landMarksCount];
      for(int i = 0; i < landMarksCount * 3; i += 3) {
        float.TryParse(dataChunk[i], out float x);
        float.TryParse(dataChunk[i + 1], out float y);
        float.TryParse(dataChunk[i + 2], out float z);
        //invert x,y and convert viewport to world space
        Vector3 sToW = cam.ViewportToWorldPoint(new Vector3(1 - x, 1 - y, 6 + (z * 8f)));
        //Vector3 sToW = cam.ViewportToWorldPoint(new Vector3(1 - x, 1 - y, 6 + z));
        points[i / 3] = sToW;
      }

      onPointsDeserialized(points);
    }
  }
}