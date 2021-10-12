using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour{

  public Transform handleA;
  public Transform handleB;

  public Transform cube;

  private void OnDrawGizmos() {
    Vector3 dir = (handleB.position - handleA.position).normalized;
    dir.x = 0;
    var angle = Vector3.Angle(dir, Vector3.up) * Mathf.Sign(dir.z);
    cube.transform.position = Vector3.Lerp(handleA.position, handleB.position, 0.5f);
    cube.rotation = Quaternion.Euler(angle, 0,0);
  }


}
