using UnityEngine;

namespace MediaPipe
{
  public abstract class LandMarksProcessor
  {
    public LandMarksProcessor() { }
    public GenericLandMarksData data;

    public virtual void OnPointsDeserialized(Vector3[] points){
      throw new System.NotImplementedException("OnPointsDeserialized need to be overriden");
    }
  }
}
