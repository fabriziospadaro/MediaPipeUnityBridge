using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  [System.Serializable]
  public class HandsData : GenericLandMarksData {
    public sealed class Constants {
      public const int WRIST = 0;

      public const int THUMB_CMC = 1;
      public const int THUMB_MCP = 2;
      public const int THUMB_IP = 3;
      public const int THUMB_TIP = 4;

      public const int INDEX_FINGER_MCP = 5;
      public const int INDEX_FINGER_PIP = 6;
      public const int INDEX_FINGER_DIP = 7;
      public const int INDEX_FINGER_TIP = 8;

      public const int MIDDLE_FINGER_MCP = 9;
      public const int MIDDLE_FINGER_PIP = 10;
      public const int MIDDLE_FINGER_DIP = 11;
      public const int MIDDLE_FINGER_TIP = 12;

      public const int RING_FINGER_MCP = 13;
      public const int RING_FINGER_PIP = 14;
      public const int RING_FINGER_DIP = 15;
      public const int RING_FINGER_TIP = 16;

      public const int PINKY_FINGER_MCP = 17;
      public const int PINKY_FINGER_PIP = 18;
      public const int PINKY_FINGER_DIP = 19;
      public const int PINKY_FINGER_TIP = 20;

      public static readonly int[] THUMB_POINTS = new int[] { 1, 2, 3, 4 };
      public static readonly int[] INDEX_FINGER_POINTS = new int[] { 5, 6, 7, 8 };
      public static readonly int[] MIDDLE_FINGER_POINTS = new int[] { 9, 10, 11, 12 };
      public static readonly int[] RING_FINGER_POINTS = new int[] { 13, 14, 15, 16 };
      public static readonly int[] PINKY_POINTS = new int[] { 17, 18, 19, 20 };

      public static readonly List<int[]> CONNECTIONS = new List<int[]> {
        new int[]{ 0,1,2,3,4, },
        new int[]{ 0,5,9,13,17,0},
        new int[]{ 5,6,7,8 },
        new int[]{ 9,10,11,12 },
        new int[]{ 13,14,15,16 },
        new int[]{ 17,18,19,20 },
      };
    }

    public HandsData(Vector3[] points) : base(points) {
    }

    public override void CalculateBasisVector() {
      up = -(points[Constants.WRIST] - points[Constants.MIDDLE_FINGER_MCP]).normalized;
      right = (points[Constants.INDEX_FINGER_MCP] - points[Constants.PINKY_FINGER_MCP]).normalized;
      forward = Vector3.Cross(up, right);
    }

    public bool isFacingFront {get { return points[Constants.INDEX_FINGER_MCP].x > points[Constants.INDEX_FINGER_MCP].x; }}
  }
  
}
