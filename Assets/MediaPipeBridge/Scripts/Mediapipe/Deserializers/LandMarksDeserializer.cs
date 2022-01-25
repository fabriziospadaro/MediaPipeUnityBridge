using UnityEngine;

namespace MediaPipe {
  public abstract class LandMarksDeserializer : ILandMark {
    public LandMarksDeserializer() { }

    public System.Action<Vector3[],int> onPointsDeserialized;
    public int landMarksCount = 0;
    public Camera cam;
    System.Globalization.CultureInfo cultureInfo;
    public void SetDeps(Camera cam) {
      this.cam = cam;
    }

    public void SetVars(System.Action<Vector3[], int> onPointsDeserialized,int landMarksCount) {
      this.onPointsDeserialized = onPointsDeserialized;
      this.landMarksCount = landMarksCount;
      cultureInfo = new System.Globalization.CultureInfo("en-US");
    }

    public virtual void OnLandmarkCollected(string serializedPoints, int i) {
      
      string[] dataChunk = serializedPoints.Split(new char[] { '*' });
      Vector3[] points = new Vector3[landMarksCount];
      for(int j = 0; j < landMarksCount*3; j += 3) {
        float.TryParse(dataChunk[j], System.Globalization.NumberStyles.Float, cultureInfo, out float x);
        float.TryParse(dataChunk[j+1], System.Globalization.NumberStyles.Float, cultureInfo, out float y);
        float.TryParse(dataChunk[j+2], System.Globalization.NumberStyles.Float, cultureInfo, out float z);
        Vector3 s2w = cam.ViewportToWorldPoint(new Vector3(1 - x, 1 - y, 6 + z));
        points[j/3] = s2w;
      }
      onPointsDeserialized(points,i);
    }
  }
}
