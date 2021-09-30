using System;
using UnityEngine;
using System.Reflection;
namespace MediaPipe {
  [CreateAssetMenu(fileName = "MediaPipeModule", menuName = "ScriptableObject/MediaPipe/Module", order = 100)]
  public class MediaPipeModule : ScriptableObject {
    public enum Category { FaceMesh = 420, Hands = 32, Pose = 80 }
    public Category category;

    public LandMarksDeserializer landMarksDeserializer;
    public LandMarksDeserializerWrapper deserializerWrapper;

    public LandMarksProcessor landMarkProcessor;
    public LandMarksProcessorWrapper processorWrapper;

    public Action<string> onLandmarkCollected;

    public void Initialize(Camera cam) {
      object landMarkProcessorInstance = Assembly.GetExecutingAssembly().CreateInstance("MediaPipe." + processorWrapper.name);
      landMarkProcessor = (LandMarksProcessor)landMarkProcessorInstance;

      object landMarkDeserializerInstance = Assembly.GetExecutingAssembly().CreateInstance("MediaPipe." + deserializerWrapper.name);
      landMarksDeserializer = (LandMarksDeserializer)landMarkDeserializerInstance;

      landMarksDeserializer.SetDeps(cam);
      landMarksDeserializer.SetVars((int)category, landMarkProcessor.OnPointsDeserialized);
      onLandmarkCollected += landMarksDeserializer.OnLandmarkCollected;
    }
  }
}
