using UnityEngine;

namespace MediaPipe {
  [System.Serializable]
  public class FaceMeshData : GenericLandMarksData {
    public Quaternion rotation;
    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;

    public FaceMeshData(Vector3[] points) : base(points) {
      CalculateRotation();
    }

    void CalculateBasisVector() {
      //TODO HARDCODED POINTS, CONSTANTIZE THEM
      up = -(points[152] - points[10]).normalized;
      right = (points[234] - points[454]).normalized;
      forward = Vector3.Cross(up, right);
    }

    void CalculateRotation() {
      CalculateBasisVector();
      rotation = Quaternion.LookRotation(forward, up);
      rotation = Quaternion.Euler(-rotation.eulerAngles.x, 180 + rotation.eulerAngles.y, -rotation.eulerAngles.z);
    }
  }
}
