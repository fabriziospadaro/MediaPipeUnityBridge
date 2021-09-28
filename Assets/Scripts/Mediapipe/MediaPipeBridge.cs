using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MediaPipe;

public class MediaPipeBridge : MonoBehaviour{

  public System.Action<FaceData> onPointDeserialized = null;
  public static MediaPipeBridge Instance;
  Camera cam;
  public TextAsset mediaPipeVC;
  string[] mediaPipeEpisodes;
  int episodeIndex;
  public bool playEpisode = true;
  public float VCSpeed = 0.001f;

  private void Awake() {
    cam = FindObjectOfType<Camera>();
    Instance = this;
    mediaPipeEpisodes = mediaPipeVC.text.Split(new char[] { '/' });
#if UNITY_EDITOR
    StartCoroutine(PlayCassette());
#endif

  }

  public IEnumerator PlayCassette() {
    while(true) {
      if(playEpisode) {
        OnFacePointsCalculated(mediaPipeEpisodes[episodeIndex % mediaPipeEpisodes.Length]);
        episodeIndex++;
      }
      yield return new WaitForSeconds(VCSpeed);
    }
  }

  void OnFacePointsCalculated(string serializedPoints) {
    string[] dataChunk = serializedPoints.Split(new char[] { '*' });
    List<Vector3> facePoints = new List<Vector3>();

    for(int i = 0; i < dataChunk.Length; i += 3) {
      float.TryParse(dataChunk[i],out float x);
      float.TryParse(dataChunk[i+1],out float y);
      float.TryParse(dataChunk[i+2],out float z);
      //mirror x,y
      x = 1 - x;
      y = 1 - y;
      //convert normalized space to screen space
      x *= Screen.width;
      y *= Screen.height;
      //convert screen space to world space
      Vector3 sToW = cam.ScreenToWorldPoint(new Vector3(x, y, 6 + (z * 7.5f)));
      facePoints.Add(sToW);
    }

    if(onPointDeserialized != null)
      onPointDeserialized(new FaceData(facePoints.ToArray()));
  }

}
