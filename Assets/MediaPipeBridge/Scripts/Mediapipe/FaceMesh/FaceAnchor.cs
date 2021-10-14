using UnityEngine;

namespace MediaPipe {
  public class FaceAnchor : MonoBehaviour {

    public FaceMeshData.Constants.Anchor anchor;
    public bool lookRotation = false;
    public Vector3 offset;
    FaceMeshData face;
    private void Update() {
      face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());
      if(face != null) {
        transform.position = face.points[(int)anchor] + offset;
        if(lookRotation)
          transform.rotation = face.rotation;
      }
    }

  }

}