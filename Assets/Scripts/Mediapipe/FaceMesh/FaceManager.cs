using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class FaceManager : MonoBehaviour {
    public FaceMeshData face;
    public static FaceManager Instance;

    private void Start() {
      Instance = this;
    }
    private void Update() {
      face = (FaceMeshData)MediaPipeBridge.GetModule("FaceMesh").ProcessorData;
    }
  }
}
