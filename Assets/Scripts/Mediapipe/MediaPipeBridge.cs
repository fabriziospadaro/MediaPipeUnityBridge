using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MediaPipe;
public class MediaPipeBridge : MonoBehaviour{

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

  public static MediaPipeModule GetModule(string category) {
    if(Instance.moduleDictionary.ContainsKey(category))
      return Instance.moduleDictionary[category];
    else
      Debug.LogError($"No such module \"{category}\"");
    return null;
  }

  public void OnLandmarksCollected(string serializedPoints, string moduleName) {
    GetModule(moduleName).onLandmarkCollected(serializedPoints);
  }

}
