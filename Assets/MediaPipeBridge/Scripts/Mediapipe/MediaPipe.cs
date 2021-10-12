using UnityEngine;
//https://github.com/tensorflow/tfjs-models/tree/master/facemesh

namespace MediaPipe {
  public class FaceMesh {
    public static readonly int[] OVAL_INDICES = new int[] {
    10, 338, 297, 332, 284, 251, 389, 356, 454,
    323, 361, 288, 397, 365, 379, 378, 400, 377, 152,
    148, 176, 149, 150, 136, 172, 58, 132, 93, 234, 127,
    162, 21, 54, 103, 67, 109
    };

    public enum Anchor { LEFT_EYE,RIGHT_EYE,LEFT_EAR = 234, RIGHT_EAR = 454,HEAD_TOP = 10}
  }
}