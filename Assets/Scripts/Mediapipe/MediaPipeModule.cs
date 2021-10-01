using System;
using UnityEngine;
using System.Reflection;
namespace MediaPipe {
  [CreateAssetMenu(fileName = "MediaPipeModule", menuName = "ScriptableObject/MediaPipe/Module", order = 100)]
  public class MediaPipeModule : ScriptableObject {
    public enum Category { FaceMesh = 468, Hands = 21, Pose }
    public Category category;

    private LandMarksDeserializer landMarksDeserializer;
    [SerializeField]
    private LandMarksDeserializerWrapper deserializerWrapper;

    private LandMarksProcessor landMarkProcessor;
    [SerializeField]
    private LandMarksProcessorWrapper processorWrapper;

    public Action<string> onLandmarkCollected;

    public void Initialize(Camera cam) {
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
      landMarksDeserializer = (LandMarksDeserializer)landMarkDeserializerInstance;

      landMarksDeserializer.SetDeps(cam);
      landMarksDeserializer.SetVars(landMarkProcessor.OnPointsDeserialized,(int)category);
      onLandmarkCollected += landMarksDeserializer.OnLandmarkCollected;
    }

    public GenericLandMarksData ProcessorData {
      get { return landMarkProcessor.data; }
    }
  }
}
