using UnityEngine;
namespace MediaPipe {
  public class GenericLandMarksData {
    public Vector3[] points;
    public Bounds bound;

    public GenericLandMarksData(Vector3[] points) {
      this.points = points;
      CalculateBounds();
    }
    
    public void CalculateBounds() {
      bound = GeometryUtility.CalculateBounds(points, Matrix4x4.identity);
    }
  }
}
