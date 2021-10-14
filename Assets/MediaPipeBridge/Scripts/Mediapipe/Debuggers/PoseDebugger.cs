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
        PoseData pose = (PoseData)MediaPipeBridge.GetData(MediaPipeModule.Category.Pose.ToString());
        landMarkPointsSmoother.Step(pose.points, Time.deltaTime);
        //DRAW CONNECTIONS
        foreach(int[] connections in PoseData.Constants.CONNECTIONS)
          for(int i = 0; i < connections.Length; i++)
            if(i+1 < connections.Length)
              Draw.Line(landMarkPointsSmoother.points[connections[i]], landMarkPointsSmoother.points[connections[i + 1]],Color.yellow);

        
        Draw.Line(pose.torsoCenter, pose.torsoCenter + pose.torsoUp, Color.green);
        Draw.Line(pose.torsoCenter, pose.torsoCenter + pose.torsoForward, Color.blue);
        Draw.Line(pose.torsoCenter, pose.torsoCenter  + pose.torsoRight, Color.red);



        Draw.Sphere(pose.hipsMidpoint, 0.05f, Color.blue);
        Draw.Sphere(pose.shouldersMidpoint, 0.05f, Color.blue);
        Draw.Sphere(pose.anklesMidpoint, 0.05f, Color.blue);
      }
    }
  }
}
