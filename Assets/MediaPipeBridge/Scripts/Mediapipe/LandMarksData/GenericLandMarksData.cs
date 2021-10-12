using UnityEngine;
namespace MediaPipe {
  public abstract class GenericLandMarksData {
    public Vector3[] points;
    public Bounds bound;

    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;
    public Quaternion rotation;

    public GenericLandMarksData(Vector3[] points) {
      this.points = points;
      CalculateBounds();
      CalculateRotation();
    }
    
    public void CalculateBounds() {
      bound = GeometryUtility.CalculateBounds(points, Matrix4x4.identity);
    }

    public virtual void CalculateBasisVector() {
      throw new System.NotImplementedException("CalculateBasisVector not implemented");
    }

    public void CalculateRotation() {
      CalculateBasisVector();
      rotation = Quaternion.LookRotation(forward, up);
      rotation = Quaternion.Euler(-rotation.eulerAngles.x, 180 + rotation.eulerAngles.y, -rotation.eulerAngles.z);
    }
  }
}
