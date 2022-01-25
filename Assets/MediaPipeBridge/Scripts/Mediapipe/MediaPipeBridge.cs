using System.Collections.Generic;
using UnityEngine;
using MediaPipe;
public class MediaPipeBridge: MonoBehaviour {

  public static MediaPipeBridge Instance;
  public MediaPipeModule[] modules;
  public ScriptableSettings settings;
  private Dictionary<string, MediaPipeModule> moduleDictionary = new Dictionary<string, MediaPipeModule>();
  private Dictionary<string, int> maxTrackedObjByModule = new Dictionary<string, int>();
  public static MediaPipeModule[] activeModules;

  private void Awake(){
    Instance = this;
    Camera cam = FindObjectOfType<Camera>();
    foreach(MediaPipeModule l in modules) {
      l.Initialize(cam,settings);
      moduleDictionary.Add(l.category.ToString(), l);
      maxTrackedObjByModule.Add(l.category.ToString(), l.maxTrackedObjects);
    }
  }

  public static MediaPipeModule GetModule(string category){
    if(Instance.moduleDictionary.ContainsKey(category))
      return Instance.moduleDictionary[category];
    else
      throw new System.NotImplementedException($"No such module \"{category}\"");
  }

  public static GenericLandMarksData[] GetResults(string category){
    return GetModule(category).processorResults;
  }

  public static T[] GetResults<T>(string category) where T : GenericLandMarksData{
    return (T[])GetModule(category).processorResults;
  }

  public void OnResult(string args){
    string[] dataArgs = args.Split(new char[] { '|' });
    string moduleName = dataArgs[0];
    string[] resultsData = dataArgs[1].Split(new char[] { '^' });
    for(int i = 0; i < maxTrackedObjByModule[moduleName]; i++) {
      if(i < resultsData.Length && dataArgs[1] != "") {
        string[] pointsDataSplit = resultsData[i].Split(new char[] { '$' });
        string state = pointsDataSplit[0];
        string rawPoints = pointsDataSplit[1];
        OnLandmarksCollected(moduleName, rawPoints, state, i);
      }
      else {
        OnLandmarksLost(moduleName, i);
      }
    }
    /*
    string state = dataArgs[1];
    /*STATE MAPPING:
    DESTROY_STATE = "-1";
    EXIT_STATE = "0";
    ENTER_STATE = "1";
    STAY_STATE = "2";
    if(state == "0")
      OnLandmarksLost(moduleName);
    else if(state == "-1")
      DestroyLandmarks(moduleName);
    else
      OnLandmarksCollected(moduleName, dataArgs[2], state);
    */
  }


  public void OnLandmarksCollected(string moduleName, string serializedPoints, string state, int index){
    GetModule(moduleName).onLandmarkCollected(serializedPoints,index);
    SetProcessorDataState(moduleName, GenericLandMarksData.ParseState(state),index);
  }
  public void DestroyLandmarks(string moduleName){
    //GetModule(moduleName).ProcessorData = null;
  }

  public void OnLandmarksLost(string moduleName, int index){
    //SetProcessorDataState(moduleName, GenericLandMarksData.State.Exit, index);
    GetModule(moduleName).SetProcessorData(index, null);
  }

  private void SetProcessorDataState(string moduleName, GenericLandMarksData.State state,int index){
    var data = GetModule(moduleName).processorResults;
    if(data != null)
      GetModule(moduleName).GetProcessorData(index).state = state;
  }
  private void OnValidate(){
    activeModules = modules;
  }
}