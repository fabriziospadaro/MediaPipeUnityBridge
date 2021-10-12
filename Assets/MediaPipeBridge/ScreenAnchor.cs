using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnchor : MonoBehaviour{

  public enum Anchor { TopLeft,TopRight,BottomLeft,BottomRight}
  public Anchor worldAnchor;
  Camera cam;
  private void Start() {
    cam = FindObjectOfType<Camera>();
  }

  void Update(){
    Vector3 worldPoint = Vector3.zero;
    switch(worldAnchor) {
      case Anchor.BottomLeft:
        worldPoint = cam.ScreenToWorldPoint(new Vector3(0, 0, 5));break;
      case Anchor.BottomRight:
        worldPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5)); break;
      case Anchor.TopLeft:
        worldPoint = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 5)); break;
      case Anchor.TopRight:
        worldPoint = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 5)); break;
    }

    transform.position = worldPoint;
  }

}
