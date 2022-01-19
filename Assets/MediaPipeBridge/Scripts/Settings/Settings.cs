using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MediaPipe {
  [System.Serializable]
  public struct FaceMeshOptions {
    public const string NAME = "FaceMesh";
    public bool enabled;
    public int maxNumFaces;
    public bool refineLandmarks;
    public float minDetectionConfidence;
    public float minTrackingConfidence;
  }
  public struct HandsOptions {
    public const string NAME = "Hands";
    public bool enabled;
    public int maxNumHands;
    public int modelComplexity;
    public float minDetectionConfidence;
    public float minTrackingConfidence;
  }
  public struct GeneralSettings {
    public const string NAME = "General";
    public bool debug;
    public enum SizingRule { Size, Ratio };
    public SizingRule sizingRule;
    public Vector2Int dimension;
  }
  class Settings {
    public static string[] enabledModules {
      get {
        List<string> enabledModules = new List<string>();

        string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{FaceMeshOptions.NAME}_Settings.json";
        if(((FaceMeshOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(FaceMeshOptions))).enabled)
          enabledModules.Add(FaceMeshOptions.NAME);

        settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{HandsOptions.NAME}_Settings.json";
        if(((HandsOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(HandsOptions))).enabled)
          enabledModules.Add(HandsOptions.NAME);

        return enabledModules.ToArray();
      }
    }
  }

}