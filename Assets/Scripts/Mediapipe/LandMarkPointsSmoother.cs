using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MediaPipe {
  public class LandMarkPointsSmoother : MonoBehaviour {
    public Vector3[] points;
    float speed = 0;

    public LandMarkPointsSmoother(float speed) {
      this.speed = speed;
    }

    public void Step(Vector3[] points, float deltaTime) {
      float timeStep = deltaTime * speed;
      if(this.points != null) {
        for(int i = 0; i < points.Length; i++)
          this.points[i] = Vector3.Lerp(this.points[i], points[i], timeStep);
      }
      else {
        this.points = points;
      }
    }
  }

}