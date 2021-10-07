using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MediaPipe;
using System.Runtime.InteropServices;

public class MediaPipeBridge : MonoBehaviour{
  
  [DllImport("__Internal")]
  private static extern void LoadMPModule(string name);
  
  public static MediaPipeBridge Instance;
  public MediaPipeModule[] modules;
  private Dictionary<string, MediaPipeModule> moduleDictionary = new Dictionary<string, MediaPipeModule>();

  private void Awake() {
    Camera cam = FindObjectOfType<Camera>();
    foreach(MediaPipeModule l in modules) {
      l.Initialize(cam);
      moduleDictionary.Add(l.category.ToString(), l);
      if(!Application.isEditor)
        LoadMPModule(l.category.ToString());
    }
    Instance = this;
  }

  public static MediaPipeModule GetModule(string category) {
    if(Instance.moduleDictionary.ContainsKey(category))
      return Instance.moduleDictionary[category];
    else
      throw new System.NotImplementedException($"No such module \"{category}\"");
  }

  public static GenericLandMarksData GetData(string category) {
    return GetModule(category).ProcessorData;
  }


  public void OnLandmarksCollected(string args) {
    string[] dataArgs = args.Split(new char[] { '|' });
    GetModule(dataArgs[0]).onLandmarkCollected(dataArgs[1]);
  }

}
