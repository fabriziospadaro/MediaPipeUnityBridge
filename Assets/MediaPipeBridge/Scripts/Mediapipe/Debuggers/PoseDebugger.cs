using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Shapes;
namespace MediaPipe {
  public class PoseDebugger : MonoBehaviour {
    private LandMarkPointsSmoother landMarkPointsSmoother;

    private void Start() {
      landMarkPointsSmoother = new LandMarkPointsSmoother(80);
    }

    private void OnRenderObject() {
      if(Application.isPlaying) {
        PoseData[] results = MediaPipeBridge.GetResults<PoseData>(MediaPipeModule.Category.Pose.ToString());
        foreach(PoseData pose in results) {
          landMarkPointsSmoother.Step(pose.points, Time.deltaTime);

          foreach(int[] connections in PoseData.Constants.CONNECTIONS)
            for(int i = 0; i < connections.Length; i++)
              if(i + 1 < connections.Length)
                Draw.Line(landMarkPointsSmoother.points[connections[i]], landMarkPointsSmoother.points[connections[i + 1]], Color.yellow);

          DebugTorso(pose);
          DebugHead(pose);
        }
      }
    }

    void DebugTorso(PoseData pose) {
      Draw.Line(pose.torsoCenter, pose.torsoCenter + pose.torsoRotation.up, Color.green);
      Draw.Line(pose.torsoCenter, pose.torsoCenter + pose.torsoRotation.forward, Color.blue);
      Draw.Line(pose.torsoCenter, pose.torsoCenter + pose.torsoRotation.right, Color.red);

      Draw.Sphere(pose.hipsMidpoint, 0.05f, Color.blue);
      Draw.Sphere(pose.shouldersMidpoint, 0.05f, Color.blue);
      Draw.Sphere(pose.anklesMidpoint, 0.05f, Color.blue);
    }

    void DebugHead(PoseData pose) {
      Draw.Line(pose.points[PoseData.Constants.NOSE], pose.points[PoseData.Constants.NOSE] + pose.headRotation.up, Color.green);
      Draw.Line(pose.points[PoseData.Constants.NOSE], pose.points[PoseData.Constants.NOSE] + pose.headRotation.forward, Color.blue);
      Draw.Line(pose.points[PoseData.Constants.NOSE], pose.points[PoseData.Constants.NOSE] + pose.headRotation.right, Color.red);
    }

  }
}
