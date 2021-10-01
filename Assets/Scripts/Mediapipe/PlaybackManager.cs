using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MediaPipe {
  public class PlaybackManager : MonoBehaviour {
    public MediaPipeModule.Category category;
    public TextAsset tape;
    private int elapsedFrames = 0;
    public int episodeId;
    public string[] episodes;
    public float episodeDuration = 0.01f;

    private void Start() {
      if(tape) {
        episodes = tape.text.Split(new char[] { '/' });
        StartCoroutine(PlayTape());
      }
    }

    IEnumerator PlayTape() {
      while(true) {
        episodeId = elapsedFrames % episodes.Length;
        MediaPipeBridge.Instance.OnLandmarksCollected(episodes[episodeId], category.ToString());
        elapsedFrames++;
        yield return new WaitForSeconds(episodeDuration);
      }
    }

  }
}
