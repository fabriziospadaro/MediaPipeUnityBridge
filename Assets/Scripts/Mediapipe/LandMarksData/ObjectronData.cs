using UnityEngine;

namespace MediaPipe {
  [System.Serializable]
  public class ObjectronData : GenericLandMarksData {
    public sealed class Constants {
      public static readonly int[] BOX_CONNECTIONS = new int[] {
         1,2, 2,4, 4,3, 3,1, 5,6, 6,8,
         8,7, 7,5, 1,5, 2,6, 3,7, 4,8 
      };
    }

    public ObjectronData(Vector3[] points) : base(points) {}

    public override void CalculateBasisVector() {

      var right1 = -(points[8] - points[7]).normalized;
      var right2 = -(points[4] - points[3]).normalized;
      right = (right1 + right2) / 2f;

      var forward1 = -(points[4] - points[8]).normalized;
      var forward2 = -(points[3] - points[7]).normalized;
      forward = (forward1 + forward2) / 2f;

      up = -Vector3.Cross(forward, right);

    }
  }
  
}
