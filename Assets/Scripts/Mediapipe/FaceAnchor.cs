using UnityEngine;

namespace MediaPipe {
  public class FaceAnchor : MonoBehaviour {

    public FaceMesh.Anchor anchor;
    public bool lookRotation = false;
    public Vector3 offset;

    private void Update() {
      if(FaceManager.face != null) {
        transform.position = FaceManager.face.points[(int)anchor] + offset;
        if(lookRotation)
          transform.rotation = FaceManager.face.rotation;
      }
    }

  }

}