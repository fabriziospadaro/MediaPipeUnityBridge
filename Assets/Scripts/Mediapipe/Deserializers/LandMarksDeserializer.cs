using UnityEngine;

namespace MediaPipe {
  public abstract class LandMarksDeserializer : ILandMark {
    public LandMarksDeserializer() { }

    public System.Action<Vector3[]> onPointsDeserialized;
    public int landMarksCapacity = 0;
    private Camera cam;

    public void SetDeps(Camera cam) {
      this.cam = cam;
    }

    public void SetVars(int landMarksCapacity, System.Action<Vector3[]> onPointsDeserialized) {
      this.landMarksCapacity = landMarksCapacity;
      this.onPointsDeserialized = onPointsDeserialized;
    }

    public virtual void OnLandmarkCollected(string serializedPoints) {
      string[] dataChunk = serializedPoints.Split(new char[] { '*' });
      Vector3[] points = new Vector3[landMarksCapacity];

      for(int i = 0; i < dataChunk.Length; i += 3) {
        float.TryParse(dataChunk[i], out float x);
        float.TryParse(dataChunk[i + 1], out float y);
        float.TryParse(dataChunk[i + 2], out float z);
        //mirror x,y
        x = 1 - x;
        y = 1 - y;
        //convert normalized space to screen space
        x *= Screen.width;
        y *= Screen.height;
        //convert screen space to world space
        Vector3 sToW = cam.ScreenToWorldPoint(new Vector3(x, y, 6 + (z * 7.5f)));
        points[i] = sToW;
      }

      onPointsDeserialized(points);
    }
  }
}
