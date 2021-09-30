using UnityEngine;

namespace MediaPipe {
  public class FaceMeshData {
    public Vector3[] points;
    public Quaternion rotation;
    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;
    public Bounds bound;

    public FaceMeshData(Vector3[] points) {
      this.points = points;
      CalculateRotation();
      CalculateBounds();
    }

    void CalculateBasisVector() {
      up = -(points[152] - points[10]).normalized;
      right = (points[234] - points[454]).normalized;
      forward = Vector3.Cross(up, right);
    }

    void CalculateRotation() {
      CalculateBasisVector();
      rotation = Quaternion.LookRotation(forward, up);
      rotation = Quaternion.Euler(-rotation.eulerAngles.x, 180 + rotation.eulerAngles.y, -rotation.eulerAngles.z);
    }

    void CalculateBounds() {
      bound = GeometryUtility.CalculateBounds(points, Matrix4x4.identity);
    }
  }
}
