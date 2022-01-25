using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILandMark{
  public void OnLandmarkCollected(string serializedPoints, int i);
}
