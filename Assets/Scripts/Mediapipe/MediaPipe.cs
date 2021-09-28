using UnityEngine;
//https://github.com/tensorflow/tfjs-models/tree/master/facemesh

namespace MediaPipe {
  public class Constants {
    public static readonly int[] OVAL_INDICES = new int[] {
    10, 338, 297, 332, 284, 251, 389, 356, 454,
    323, 361, 288, 397, 365, 379, 378, 400, 377, 152,
    148, 176, 149, 150, 136, 172, 58, 132, 93, 234, 127,
    162, 21, 54, 103, 67, 109
    };

    public enum Anchor { LEFT_EYE,RIGHT_EYE,LEFT_EAR = 234, RIGHT_EAR = 454,HEAD_TOP = 10}
  }

  public class FaceData {
    public Vector3[] points;
    public Quaternion rotation;
    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;
    public Bounds bound;

    public FaceData(Vector3[] points) {
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