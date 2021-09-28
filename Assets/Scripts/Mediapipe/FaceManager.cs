using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class FaceManager : MonoBehaviour {
    public static FaceData face;

    private void Start() {
      MediaPipeBridge.Instance.onPointDeserialized += SaveFaceData;
    }

    void SaveFaceData(FaceData face) {
      FaceManager.face = face;
    }
  }
}
