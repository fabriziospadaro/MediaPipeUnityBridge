using System;
using UnityEngine;
using System.Reflection;
namespace MediaPipe {
  [CreateAssetMenu(fileName = "MediaPipeModule", menuName = "ScriptableObject/MediaPipe/Module", order = 100)]
  public class MediaPipeModule: ScriptableObject {
    public enum Category { FaceMesh = 468, Hands = 21, Pose = 33, Objectron = 9 }
    public Category category;

    private LandMarksDeserializer landMarksDeserializer;
    [SerializeField]
    private LandMarksDeserializerWrapper deserializerWrapper;

    private LandMarksProcessor landMarkProcessor;
    [SerializeField]
    private LandMarksProcessorWrapper processorWrapper;

    public Action<string, int> onLandmarkCollected;
    public Action onPostProcess;
    public int maxTrackedObjects = 0;

    public void Initialize(Camera cam,ScriptableSettings settings){
      object landMarkProcessorInstance = Assembly.GetExecutingAssembly().CreateInstance("MediaPipe." + processorWrapper.name);
      landMarkProcessor = (LandMarksProcessor)landMarkProcessorInstance;
      if(landMarkProcessorInstance == null) {
        Debug.LogError($"No such class \"{processorWrapper.name}\" found inside the MediaPipe napespace");
        return;
      }

      object landMarkDeserializerInstance = Assembly.GetExecutingAssembly().CreateInstance("MediaPipe." + deserializerWrapper.name);
      if(landMarkDeserializerInstance == null) {
        Debug.LogError($"No such class \"{deserializerWrapper.name}\" found inside the MediaPipe napespace");
        return;
      }
      maxTrackedObjects = MaxTrackedObjectsByCategory(category, settings);
      landMarksDeserializer = (LandMarksDeserializer)landMarkDeserializerInstance;
      landMarkProcessor.InitializeResults(maxTrackedObjects);
      landMarksDeserializer.SetDeps(cam);
      landMarksDeserializer.SetVars(landMarkProcessor.OnPointsDeserialized, (int)category);
      onLandmarkCollected += landMarksDeserializer.OnLandmarkCollected;
      onPostProcess = landMarkProcessor.PostProcess;
    }
    public GenericLandMarksData GetProcessorData(int i) {
      return landMarkProcessor.results[i];
    }

    public GenericLandMarksData SetProcessorData(int i, GenericLandMarksData value){
      return landMarkProcessor.results[i] = value;
    }

    public GenericLandMarksData[] processorResults{
      get { return landMarkProcessor.results; }
    }

    public static int MaxTrackedObjectsByCategory(Category category, ScriptableSettings settings) {
      switch(category) {
        case Category.FaceMesh:
        return settings.faceMeshOptions.maxNumFaces;
        case Category.Hands:
        return settings.handsOptions.maxNumHands;
      }
      throw new Exception($"{category} is not mapped with his related max tracked mobject setting");
    }
  }
}