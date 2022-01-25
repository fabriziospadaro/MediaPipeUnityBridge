using System.Collections;
using UnityEngine;
namespace MediaPipe {
  public class PlaybackManager: MonoBehaviour {
    public MediaPipeModule.Category category;
    public TextAsset tape;
    private int elapsedFrames = 0;
    public int episodeId;
    public string[] episodes;
    public float episodeDuration = 0.01f;
    public static PlaybackManager Instance;
    private void Awake(){
#if UNITY_EDITOR
      Instance = this;
      if(tape) {
        episodes = tape.text.Split(new char[] { '/' });
        StartCoroutine(PlayTape());
      }
#endif
    }

    IEnumerator PlayTape(){
      while(true) {
        episodeId = elapsedFrames % episodes.Length;
        MediaPipeBridge.Instance.OnResult(episodes[episodeId]);
        elapsedFrames++;
        yield return new WaitForSeconds(episodeDuration);
      }
    }
  }
}