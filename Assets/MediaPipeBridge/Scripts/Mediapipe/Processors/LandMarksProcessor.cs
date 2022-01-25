using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe{
  public abstract class LandMarksProcessor{
    public LandMarksProcessor() { }
    public GenericLandMarksData[] results;

    public virtual void OnPointsDeserialized(Vector3[] points, int i){
      throw new System.NotImplementedException("OnPointsDeserialized need to be overriden");
    }

    public virtual void PostProcess() {}

    public void InitializeResults(int size) {
      results = new GenericLandMarksData[size];
    }

    public void Set(int i, GenericLandMarksData d) {
        results[i] = d;
    }
  }
}
