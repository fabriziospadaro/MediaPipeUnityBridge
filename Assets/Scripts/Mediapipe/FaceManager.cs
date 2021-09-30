using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class FaceManager : MonoBehaviour {
    public static FaceMeshData face;

    private void Start() {
      //MediaPipeBridge.Instance.onPointDeserialized += SaveFaceData;
    }

    void SaveFaceData(FaceMeshData face) {
      FaceManager.face = face;
    }
  }
}
