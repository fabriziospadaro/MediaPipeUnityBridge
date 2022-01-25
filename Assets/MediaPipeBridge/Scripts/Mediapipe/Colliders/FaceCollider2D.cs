using UnityEngine;
using MediaPipe;

namespace MediaPipe {
  public class FaceCollider2D : MonoBehaviour {
    PolygonCollider2D polyCollider;
    Camera cam;

    void Start() {
      polyCollider = GetComponent<PolygonCollider2D>();
      cam = FindObjectOfType<Camera>();
      transform.position = cam.transform.position += cam.transform.position + new Vector3(0, 0, cam.nearClipPlane);
    }

    void Update() {
      var results = MediaPipeBridge.GetResults(MediaPipeModule.Category.FaceMesh.ToString());
      foreach(GenericLandMarksData data in results) {
        Vector2[] points = new Vector2[FaceMeshData.Constants.SILHOUETTE.Length];
        Vector2 maxSize = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));

        for(int i = 0; i < FaceMeshData.Constants.SILHOUETTE.Length; i++) {
          Vector3 p = data.points[FaceMeshData.Constants.SILHOUETTE[(i + 1) % FaceMeshData.Constants.SILHOUETTE.Length]];
          points[i] = cam.WorldToViewportPoint(p) * (maxSize * 2) - maxSize;
        }

        polyCollider.points = points;
      }
    }
  }
}
