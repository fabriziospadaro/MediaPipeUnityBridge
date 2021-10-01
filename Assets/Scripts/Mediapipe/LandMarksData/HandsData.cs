using UnityEngine;

namespace MediaPipe {
  [System.Serializable]
  public class HandsData : GenericLandMarksData {

    public HandsData(Vector3[] points) : base(points) {
    }
  }
}
