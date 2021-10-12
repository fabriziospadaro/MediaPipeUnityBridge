using UnityEngine;

namespace MediaPipe {
  public abstract class LandMarksDeserializer : ILandMark {
    public LandMarksDeserializer() { }

    public System.Action<Vector3[]> onPointsDeserialized;
    public int landMarksCount = 0;
    private Camera cam;

    public void SetDeps(Camera cam) {
      this.cam = cam;
    }

    public void SetVars(System.Action<Vector3[]> onPointsDeserialized,int landMarksCount) {
      this.onPointsDeserialized = onPointsDeserialized;
      this.landMarksCount = landMarksCount;
    }

    public virtual void OnLandmarkCollected(string serializedPoints) {
      string[] dataChunk = serializedPoints.Split(new char[] { '*' });
      Vector3[] points = new Vector3[landMarksCount];
      for(int i = 0; i < landMarksCount*3; i += 3) {
        float.TryParse(dataChunk[i], out float x);
        float.TryParse(dataChunk[i + 1], out float y);
        float.TryParse(dataChunk[i + 2], out float z);
        //invert x,y and convert viewport to world space
        Vector3 sToW = cam.ViewportToWorldPoint(new Vector3(1 - x, 1 - y, 6 + (z * 7.5f)));
        points[i/3] = sToW;
      }

      onPointsDeserialized(points);
    }
  }
}
