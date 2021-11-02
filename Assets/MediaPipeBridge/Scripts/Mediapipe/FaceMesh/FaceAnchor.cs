using UnityEngine;

namespace MediaPipe {
  public class FaceAnchor : MonoBehaviour {

    public FaceMeshData.Constants.Anchor anchor;
    public Vector3 offset;
    [Header("Rotation")]
    public bool lookRotation = false;
    public Vector3 rotationOffset;
    public Vector3 rotationMultiplier;
    FaceMeshData face;
    LandMarkPointsSmoother landMarkPointsSmoother;

    private void Start() {
      landMarkPointsSmoother = new LandMarkPointsSmoother(30);
    }

    private void Update() {
      face = (FaceMeshData)MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());
      if(face != null) {
        landMarkPointsSmoother.Step(new Vector3[] { face.points[(int)anchor] }, Time.deltaTime);
        transform.position = landMarkPointsSmoother.points[0] + offset;
        transform.localScale = face.uniformScale * Vector3.one;
        if(lookRotation)
          transform.rotation = Quaternion.Euler(Vector3.Scale(face.rotation.eulerAngles + rotationOffset, rotationMultiplier));
      }
    }

  }

}