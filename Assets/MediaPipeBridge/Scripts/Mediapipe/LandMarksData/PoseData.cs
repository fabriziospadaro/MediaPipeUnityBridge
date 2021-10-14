using System.Collections.Generic;
using UnityEngine;
//https://github.com/tensorflow/tfjs-models/blob/master/facemesh/mesh_map.jpg
namespace MediaPipe {
  [System.Serializable]
  public class PoseData : GenericLandMarksData {
    //https://google.github.io/mediapipe/images/mobile/pose_tracking_full_body_landmarks.png
    public sealed class Constants {

      public static readonly List<int[]> CONNECTIONS = new List<int[]> {
        new int[]{ 12,14,16,18,20,16,22},
        new int[]{ 11,13,15,17,19,15,21},
        new int[]{ 12,11,23,24,12 },
        new int[]{ 24,26,28,30,32,28 },
        new int[]{ 23,25,27,29,31,27 },
        new int[]{ 10,9},
        new int[]{ 8,6,5,4,0,1,2,3,7},
      };

      public const int NOSE = 0;
      public const int LEFT_EYE_INNER = 1;
      public const int LEFT_EYE = 2;
      public const int LEFT_EYE_OUTER = 3;
      public const int RIGHT_EYE_INNER = 4;
      public const int RIGHT_EYE = 5;
      public const int RIGHT_EYE_OUTER = 6;
      public const int LEFT_EAR = 7;
      public const int RIGHT_EAR = 8;
      public const int MOUTH_LEFT = 9;
      public const int MOUTH_RIGHT = 10;
      public const int LEFT_SHOULDER = 11;
      public const int RIGHT_SHOULDER = 12;
      public const int LEFT_ELBOW = 13;
      public const int RIGHT_ELBOW = 14;
      public const int LEFT_WRIST = 15;
      public const int RIGHT_WRIST = 16;
      public const int LEFT_PINKY = 17;
      public const int RIGHT_PINKY = 18;
      public const int LEFT_INDEX = 19;
      public const int RIGHT_INDEX = 20;
      public const int LEFT_THUMB = 21;
      public const int RIGHT_THUMB = 22;
      public const int LEFT_HIP = 23;
      public const int RIGHT_HIP = 24;
      public const int LEFT_KNEE = 25;
      public const int RIGHT_KNEE = 26;
      public const int LEFT_ANKLE = 27;
      public const int RIGHT_ANKLE = 28;
      public const int LEFT_HEEL = 29;
      public const int RIGHT_HEEL = 30;
      public const int LEFT_FOOT_INDEX = 31;
      public const int RIGHT_FOOT_INDEX = 32;
    }

    public Vector3 torsoUp;
    public Vector3 torsoRight;
    public Vector3 torsoForward;
    public Quaternion torsoRotation;

    public PoseData(Vector3[] points) : base(points) { CalculateBodyRotations(); }

    public override void CalculateBasisVector() {
      up = -(anklesMidpoint - points[Constants.NOSE]).normalized;
      right = Vector3.Lerp((points[Constants.RIGHT_SHOULDER] - points[Constants.LEFT_SHOULDER]).normalized, (points[Constants.RIGHT_HIP] - points[Constants.LEFT_HIP]).normalized,0.5f);
      right = Vector3.Lerp(right, (points[Constants.RIGHT_KNEE] - points[Constants.LEFT_KNEE]).normalized, 0.45f);
      right = Vector3.Lerp(right, (points[Constants.RIGHT_ANKLE] - points[Constants.LEFT_ANKLE]).normalized, 0.45f);
      forward = Vector3.Cross(up, right);
    }

    public void CalculateBodyRotations() {
      torsoUp = (shouldersMidpoint - hipsMidpoint).normalized;
      torsoRight = Vector3.Lerp((points[Constants.RIGHT_HIP] - points[Constants.LEFT_HIP]).normalized, (points[Constants.RIGHT_SHOULDER] - points[Constants.LEFT_SHOULDER]).normalized,0.5f);
      torsoForward = Vector3.Cross(torsoUp, torsoRight);
      torsoRotation = Quaternion.LookRotation(torsoForward, torsoUp);
      torsoRotation = Quaternion.Euler(-torsoRotation.eulerAngles.x, 180 + torsoRotation.eulerAngles.y, -torsoRotation.eulerAngles.z);
    }

    public Vector3 anklesMidpoint { get { return Vector3.Lerp(points[Constants.LEFT_ANKLE], points[Constants.RIGHT_ANKLE], 0.5f); } }
    public Vector3 hipsMidpoint { get { return Vector3.Lerp(points[Constants.LEFT_HIP], points[Constants.RIGHT_HIP], 0.5f); } }
    public Vector3 shouldersMidpoint { get { return Vector3.Lerp(points[Constants.LEFT_SHOULDER], points[Constants.RIGHT_SHOULDER], 0.5f); } }

    public Vector3 torsoCenter { get { return Vector3.Lerp(shouldersMidpoint, hipsMidpoint, 0.5f); } }
  }
}
