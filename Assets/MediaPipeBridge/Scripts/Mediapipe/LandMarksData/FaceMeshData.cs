using UnityEngine;
//https://github.com/google/mediapipe/blob/master/mediapipe/modules/face_geometry/data/canonical_face_model_uv_visualization.png
namespace MediaPipe {
  [System.Serializable]
  public class FaceMeshData : GenericLandMarksData {
    //https://github.com/tensorflow/tfjs-models/blob/master/facemesh/src/keypoints.ts
    public sealed class Constants {
      public static readonly int[] SILHOUETTE = new int[]{
        10,  338, 297, 332, 284, 251, 389, 356, 454, 323, 361, 288,
        397, 365, 379, 378, 400, 377, 152, 148, 176, 149, 150, 136,
        172, 58,  132, 93,  234, 127, 162, 21,  54,  103, 67,  109
      };

      public static readonly int[] LIPS_UPPER_OUTER = new int[] {
        61, 185, 40, 39, 37, 0, 267, 269, 270, 409, 291
      };

      public static readonly int[] LIPS_LOWER_OUTER = new int[] {
        146, 91, 181, 84, 17, 314, 405, 321, 375, 291
      };

      public static readonly int[] LIPS_UPPER_INNER = new int[] {
        78, 191, 80, 81, 82, 13, 312, 311, 310, 415, 308
      };

      public static readonly int[] LIPS_LOWER_INNER = new int[] {
        78, 95, 88, 178, 87, 14, 317, 402, 318, 324, 308
      };

      public static readonly int[] RIGHT_EYE_UPPER0 = new int[] {
        246, 161, 160, 159, 158, 157, 173
      };

      public static readonly int[] RIGHT_EYE_LOWER0 = new int[] {
        33, 7, 163, 144, 145, 153, 154, 155, 133
      };

      public static readonly int[] RIGHT_EYE_UPPER1 = new int[] {
        247, 30, 29, 27, 28, 56, 190
      };

      public static readonly int[] RIGHT_EYE_LOWER1 = new int[] {
        130, 25, 110, 24, 23, 22, 26, 112, 243
      };

      public static readonly int[] RIGHT_EYE_UPPER2 = new int[] {
        113, 225, 224, 223, 222, 221, 189
      };

      public static readonly int[] RIGHT_EYE_LOWER2 = new int[] {
        226, 31, 228, 229, 230, 231, 232, 233, 244
      };

      public static readonly int[] RIGHT_EYE_LOWER3 = new int[] {
        143, 111, 117, 118, 119, 120, 121, 128, 245
      };

      public static readonly int[] RIGHT_EYE_BROW_UPPER = new int[] {
        156, 70, 63, 105, 66, 107, 55, 193
      };

      public static readonly int[] RIGHT_EYE_BROW_LOWER = new int[] {
        35, 124, 46, 53, 52, 65
      };

      public static readonly int[] LEFT_EYE_UPPER0 = new int[] {
        466, 388, 387, 386, 385, 384, 398
      };

      public static readonly int[] LEFT_EYE_LOWER0 = new int[] {
        263, 249, 390, 373, 374, 380, 381, 382, 362
      };

      public static readonly int[] LEFT_EYE_UPPER1 = new int[] {
        467, 260, 259, 257, 258, 286, 414
      };

      public static readonly int[] LEFT_EYE_LOWER1 = new int[] {
        359, 255, 339, 254, 253, 252, 256, 341, 463
      };

      public static readonly int[] LEFT_EYE_UPPER2 = new int[] {
        342, 445, 444, 443, 442, 441, 413
      };

      public static readonly int[] LEFT_EYE_LOWER2 = new int[] {
        446, 261, 448, 449, 450, 451, 452, 453, 464
      };

      public static readonly int[] LEFT_EYE_LOWER3 = new int[] {
        372, 340, 346, 347, 348, 349, 350, 357, 465
      };

      public static readonly int[] LEFT_EYE_BROW_UPPER = new int[] {
        383, 300, 293, 334, 296, 336, 285, 417
      };

      public static readonly int[] LEFT_EYE_BROW_LOWER = new int[] {
        265, 353, 276, 283, 282, 295
      };

      public static readonly int[] MIDWAY_BETWEEN_EYES = new int[] { 168 };
      public static readonly int[] NOSE_TIP = new int[] { 1 };
      public static readonly int[] NOSE_BOTTOM = new int[] { 2 };
      public static readonly int[] NOSE_RIGHT_CORNER = new int[] { 98 };
      public static readonly int[] NOSE_LEFT_CORNER = new int[] { 327 };
      public static readonly int[] RIGHT_CHEEK = new int[] { 205 };
      public static readonly int[] LEFT_CHEEK = new int[] { 425 };
      public const int LEFT_EAR = 234;
      public const int RIGHT_EAR = 454;
      public enum Anchor { LEFT_EYE, RIGHT_EYE, LEFT_EAR = 234, RIGHT_EAR = 454, HEAD_TOP = 10, NOSE_TIP = 168 }
    }

    public FaceMeshData(Vector3[] points) : base(points) {
    }

    public override void CalculateBasisVector() {
      //TODO HARDCODED POINTS, CONSTANTIZE THEM
      up = -(points[152] - points[10]).normalized;
      right = (points[Constants.LEFT_EAR] - points[Constants.RIGHT_EAR]).normalized;
      forward = Vector3.Cross(up, right);
    }

    public override void CalculateScale() {
      uniformScale = (Vector3.Distance(points[Constants.LEFT_EAR], points[Constants.RIGHT_EAR]) / 2.56f) * 1.2f;
    }
  }
}
