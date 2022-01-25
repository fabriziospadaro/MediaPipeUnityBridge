using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class FaceLandMarksDeserializer : LandMarksDeserializer {
    public FaceLandMarksDeserializer() { }
    public override void OnLandmarkCollected(string serializedPoints, int i) {
      string[] dataChunk = serializedPoints.Split(new char[] { '*' });
      Vector3[] points = new Vector3[landMarksCount];
      var cultureInfo = new System.Globalization.CultureInfo("en-US");
      for(int j = 0; j < landMarksCount * 3; j += 3) {
        float.TryParse(dataChunk[j], System.Globalization.NumberStyles.Float, cultureInfo, out float x);
        float.TryParse(dataChunk[j + 1], System.Globalization.NumberStyles.Float, cultureInfo, out float y);
        float.TryParse(dataChunk[j + 2], System.Globalization.NumberStyles.Float, cultureInfo, out float z);
        //invert x,y and convert viewport to world space
        Vector3 s2w = cam.ViewportToWorldPoint(new Vector3(1 - x, 1 - y, 6 + (z * 8f)));
        points[j / 3] = s2w;
      }

      onPointsDeserialized(points, i);
    }
  }
}