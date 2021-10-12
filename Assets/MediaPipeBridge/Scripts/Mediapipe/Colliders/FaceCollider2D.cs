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
      var data = MediaPipeBridge.GetData(MediaPipeModule.Category.FaceMesh.ToString());
      Vector2[] points = new Vector2[FaceMesh.OVAL_INDICES.Length];
      Vector2 maxSize = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));

      for(int i = 0; i < FaceMesh.OVAL_INDICES.Length; i++) {
        Vector3 p = data.points[FaceMesh.OVAL_INDICES[(i + 1) % FaceMesh.OVAL_INDICES.Length]];
        points[i] = cam.WorldToViewportPoint(p) * (maxSize * 2) - maxSize;
      }

      polyCollider.points = points;
    }
  }
}
