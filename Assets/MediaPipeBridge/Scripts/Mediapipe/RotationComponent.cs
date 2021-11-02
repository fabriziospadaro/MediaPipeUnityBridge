using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RotationComponent{
  public Vector3 up;
  public Vector3 right;
  public Vector3 forward;
  public Quaternion value;

  public RotationComponent(Vector3 up = default, Vector3 right = default, Vector3 forward = default) {
    this.up = this.forward = this.right = default;
    this.value = default;

    if(up != default) this.up = up;
    if(right != default) this.right = right;
    if(forward != default) this.forward = forward;

    CalculateUnassignedVector();
  }

  public void CalculateUnassignedVector() {
    if(up != default && right != default)
      forward = Vector3.Cross(up, right);
    else if(forward != default && up != default)
      right = Vector3.Cross(up, forward);
    else if(forward != default && right != default)
      up = Vector3.Cross(forward, right);
  }

  public void ComputeRotation() {
    value = Quaternion.LookRotation(forward, up);
    value = Quaternion.Euler(-value.eulerAngles.x, 180 + value.eulerAngles.y, -value.eulerAngles.z);
  }
}
