using System.Collections.Generic;
using UnityEngine;
using MediaPipe;
public class MediaPipeBridge: MonoBehaviour {

  public static MediaPipeBridge Instance;
  public MediaPipeModule[] modules;
  private Dictionary<string, MediaPipeModule> moduleDictionary = new Dictionary<string, MediaPipeModule>();
  public static MediaPipeModule[] activeModules;

  private void Awake(){
    Camera cam = FindObjectOfType<Camera>();
    foreach(MediaPipeModule l in modules) {
      l.Initialize(cam);
      moduleDictionary.Add(l.category.ToString(), l);
    }
    Instance = this;
  }

  public static MediaPipeModule GetModule(string category){
    if(Instance.moduleDictionary.ContainsKey(category))
      return Instance.moduleDictionary[category];
    else
      throw new System.NotImplementedException($"No such module \"{category}\"");
  }

  public static GenericLandMarksData GetData(string category){
    return GetModule(category).ProcessorData;
  }

  public static T GetData<T>(string category) where T : GenericLandMarksData{
    return (T)GetModule(category).ProcessorData;
  }

  public void OnResult(string args){
    string[] dataArgs = args.Split(new char[] { '|' });
    string moduleName = dataArgs[0];
    string state = dataArgs[1];
    /*STATE MAPPING:
    DESTROY_STATE = "-1";
    EXIT_STATE = "0";
    ENTER_STATE = "1";
    STAY_STATE = "2";
    */
    if(state == "0")
      OnLandmarksLost(moduleName);
    else if(state == "-1")
      DestroyLandmarks(moduleName);
    else
      OnLandmarksCollected(moduleName, dataArgs[2], state);
  }


  public void OnLandmarksCollected(string moduleName, string serializedPoints, string state){
    GetModule(moduleName).onLandmarkCollected(serializedPoints);
    SetProcessorDataState(moduleName, GenericLandMarksData.ParseState(state));
  }
  public void DestroyLandmarks(string moduleName){
    GetModule(moduleName).ProcessorData = null;
  }

  public void OnLandmarksLost(string moduleName){
    SetProcessorDataState(moduleName, GenericLandMarksData.State.Exit);
  }

  private void SetProcessorDataState(string moduleName, GenericLandMarksData.State state){
    var data = GetModule(moduleName).ProcessorData;
    if(data != null)
      GetModule(moduleName).ProcessorData.state = state;
  }
  private void OnValidate(){
    activeModules = modules;
  }
}