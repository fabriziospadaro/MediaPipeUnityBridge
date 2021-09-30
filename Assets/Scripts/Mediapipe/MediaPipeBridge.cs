using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MediaPipe;
public class MediaPipeBridge : MonoBehaviour{

  public System.Action<FaceMeshData> onPointDeserialized = null;
  public static MediaPipeBridge Instance;
  public MediaPipeModule[] modules;
  private Dictionary<string, MediaPipeModule> moduleDictionary = new Dictionary<string, MediaPipeModule>();

  private void Awake() {
    Camera cam = FindObjectOfType<Camera>();
    foreach(MediaPipeModule l in modules) {
      l.Initialize(cam);
      moduleDictionary.Add(l.category.ToString(), l);
    }
    Instance = this;
  }

  void OnLandmarksCollected(string serializedPoints,string moduleName) {
    moduleDictionary[moduleName].onLandmarkCollected(serializedPoints);
  }

}
