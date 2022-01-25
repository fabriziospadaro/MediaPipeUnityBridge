using System.Collections.Generic;
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
  [System.Serializable]
  public struct HandsOptions {
    public const string NAME = "Hands";
    public bool enabled;
    public int maxNumHands;
    public int modelComplexity;
    public float minDetectionConfidence;
    public float minTrackingConfidence;
  }
  [System.Serializable]
  public struct GeneralSettings {
    public const string NAME = "General";
    public bool debug;
    public enum SizingRule { CustomSize, Ratio, FullSize };
    public SizingRule sizingRule;
    public Vector2Int dimension;
  }
  public class Settings {
    public const string scriptableSettingsName = "scriptableSettings";

    public static string[] enabledModules {
      get {
        List<string> enabledModules = new List<string>();
        var data = getJson;
        if(data.faceMeshOptions.enabled)
          enabledModules.Add(FaceMeshOptions.NAME);
        if(data.handsOptions.enabled)
          enabledModules.Add(HandsOptions.NAME);
        return enabledModules.ToArray();
      }
    }

    public static ScriptableSettings getJson {
      get {
        return Resources.Load<ScriptableSettings>($"Settings/{scriptableSettingsName}");
      }
    }
    public static string Obj2String(string name) {
      switch(name) {
        case FaceMeshOptions.NAME: return JsonUtility.ToJson(getJson.faceMeshOptions);
        case HandsOptions.NAME: return JsonUtility.ToJson(getJson.handsOptions);
        case GeneralSettings.NAME: return JsonUtility.ToJson(getJson.generalSettings);
      }
      return "";
    }
  }

}