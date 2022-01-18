using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shapes;
using UnityEngine;
namespace MediaPipe {
  public class GenericLandMarksDebugger : MonoBehaviour {
    LandMarkPointsSmoother[] landMarkPointsSmoother;
    List<MediaPipeModule.Category> categories = new List<MediaPipeModule.Category>();

    private void Start() {
      if(Application.isEditor)
        categories.Add(PlaybackManager.Instance.category);
      else
        categories = MediaPipeBridge.Instance.modules.Select(f => f.category).ToList();

      landMarkPointsSmoother = new LandMarkPointsSmoother[categories.Count];
      for(int i = 0;i<categories.Count;i++)
        landMarkPointsSmoother[i] = new LandMarkPointsSmoother(10);
    }

    private void OnRenderObject(){
      if(Application.isPlaying) {
        Draw.BlendMode = ShapesBlendMode.Opaque;
        Draw.Cube(transform.position, 10, Color.red);
        
        int i = 0;
        foreach(MediaPipeModule.Category category in categories) {
          var data = MediaPipeBridge.GetModule(category.ToString()).ProcessorData;
          if(data.state == GenericLandMarksData.State.Stay) {
            landMarkPointsSmoother[i].Step(data.points, Time.deltaTime);

            Draw.BlendMode = ShapesBlendMode.Transparent;
            DrawWireCube(data.bound.center, data.rotation, data.bound.size / 2);
            Draw.BlendMode = ShapesBlendMode.Opaque;

            foreach(Vector3 v in landMarkPointsSmoother[i].points)
              Draw.Sphere(v, 0.03f, Color.red);

            Draw.Thickness = 0.02f;
            Draw.Line(data.bound.center, data.bound.center + data.up, Color.green);
            Draw.Line(data.bound.center, data.bound.center + data.forward, Color.blue);
            Draw.Line(data.bound.center, data.bound.center + data.right, Color.red);
          }
          i++;
        }

      }
    }


    // Draws a wireframe cube using Draw.Line
    static void DrawWireCube(Vector3 position, Quaternion rotation, Vector3 scale) {
      using(Draw.MatrixScope) {
        Draw.Matrix *= Matrix4x4.TRS(position, rotation, scale);
        foreach((Vector3 a, Vector3 b) in cubeEdges)
          Draw.Line(a, b, LineEndCap.Round,color: Color.cyan);
      }
    }

    // an array of all vertex pairs of a cube's edges
    static readonly (Vector3 a, Vector3 b)[] cubeEdges = GetCubeEdges().ToArray();

    // generates all vertex pairs of cube edges
    static IEnumerable<(Vector3, Vector3)> GetCubeEdges() {
      Vector3 Scoot(Vector3 v, int n) => new Vector3(v[n], v[(n + 1) % 3], v[(n + 2) % 3]);
      for(int a = 0; a < 3; a++)
        for(int ix = -1; ix < 2; ix += 2)
          for(int iy = -1; iy < 2; iy += 2)
            yield return (
               Scoot(new Vector3(-1, ix, iy), a),
               Scoot(new Vector3(1, ix, iy), a)
            );
    }


  }
}
