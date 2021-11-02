using UnityEngine;
namespace MediaPipe {
  public abstract class GenericLandMarksData {
    public Vector3[] points;
    public Bounds bound;

    public Vector3 up;
    public Vector3 right;
    public Vector3 forward;
    public float uniformScale;
    public Quaternion rotation = Quaternion.identity;

    public GenericLandMarksData(Vector3[] points) {
      this.points = points;
      CalculateBounds();
      CalculateRotation();
      CalculateScale();
    }
    
    public void CalculateBounds() {
      bound = GeometryUtility.CalculateBounds(points, Matrix4x4.identity);
    }

    public virtual void CalculateBasisVector() {
      throw new System.NotImplementedException("CalculateBasisVector not implemented");
    }

    public virtual void CalculateScale() {
      uniformScale = 1;
    }

    public void CalculateRotation() {
      CalculateBasisVector();
      if(up != default && right != default && forward != default) {
        rotation = Quaternion.LookRotation(forward, up);
        rotation = Quaternion.Euler(-rotation.eulerAngles.x, 180 + rotation.eulerAngles.y, -rotation.eulerAngles.z);
      }
    }
  }
}
