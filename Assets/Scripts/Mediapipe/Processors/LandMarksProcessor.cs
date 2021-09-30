using UnityEngine;

namespace MediaPipe
{
  public abstract class LandMarksProcessor
  {
    public LandMarksProcessor() { }
    public virtual void OnPointsDeserialized(Vector3[] points)
    {
    }
  }
}
