using UnityEngine;
using UnityEditor;
using System.IO;
public class FaceMeshOptions {
  public const string NAME = "FaceMesh";
  public bool enabled;
  public int maxNumFaces;
  public bool refineLandmarks;
  public float minDetectionConfidence;
  public float minTrackingConfidence;
}
public class HandsOptions {
  public const string NAME = "Hands";
  public bool enabled;
  public int maxNumHands;
  public int modelComplexity;
  public float minDetectionConfidence;
  public float minTrackingConfidence;
}
public class SettingsWindow: EditorWindow {
  FaceMeshOptions faceMeshOptions;
  HandsOptions handsOptions;
  bool faceFoldout = true;
  bool handsFoldout = true;

  [MenuItem("Window/MediaPipeBridge Settings")]
  public static void ShowWindow(){
    EditorWindow.GetWindow(typeof(SettingsWindow));
  }

  public void OnEnable(){
    LoadSettings();
  }
  void LoadSettings() {
    LoadFaceMeshSetting();
    LoadHandsOptions();
  }
  void OnGUI(){
    GUILayout.Label("Active Modules", EditorStyles.boldLabel);
    EditorGUILayout.Space();

    FaceMeshInspector();
    HandsInspector();
  }

  void FaceMeshInspector() {
    faceFoldout = EditorGUILayout.Foldout(faceFoldout, "FaceMesh");
    if(faceFoldout) {
      EditorGUI.indentLevel++;

      EditorGUI.BeginChangeCheck();
      faceMeshOptions.enabled = EditorGUILayout.BeginToggleGroup("Enabled", faceMeshOptions.enabled);
      faceMeshOptions.refineLandmarks = EditorGUILayout.Toggle("Refine Landmarks", faceMeshOptions.refineLandmarks);
      faceMeshOptions.maxNumFaces = EditorGUILayout.IntField("Max Number of faces", faceMeshOptions.maxNumFaces);
      faceMeshOptions.minDetectionConfidence = EditorGUILayout.Slider("Min Detection Confid", faceMeshOptions.minDetectionConfidence, 0, 1);
      faceMeshOptions.minTrackingConfidence = EditorGUILayout.Slider("Min Tracking Confid", faceMeshOptions.minTrackingConfidence, 0, 1);
      EditorGUILayout.EndToggleGroup();
      
      if(EditorGUI.EndChangeCheck())
        SaveSettings(MediaPipe.FaceMeshOptions.NAME, faceMeshOptions);
      EditorGUI.indentLevel--;
    }
  }
  void HandsInspector() {
    handsFoldout = EditorGUILayout.Foldout(handsFoldout, "Hands");
    if(handsFoldout) {
      EditorGUI.indentLevel++;
      EditorGUI.BeginChangeCheck();
      handsOptions.enabled = EditorGUILayout.BeginToggleGroup("Enabled", handsOptions.enabled);
      handsOptions.maxNumHands = EditorGUILayout.IntSlider("Max Number of hands", handsOptions.maxNumHands, 1, 6);
      handsOptions.modelComplexity = EditorGUILayout.IntSlider("Model Complexity", handsOptions.modelComplexity, 0, 1);
      handsOptions.minDetectionConfidence = EditorGUILayout.Slider("Min Detection Confid", handsOptions.minDetectionConfidence, 0, 1);
      handsOptions.minTrackingConfidence = EditorGUILayout.Slider("Min Tracking Confid", handsOptions.minTrackingConfidence, 0, 1);
      EditorGUILayout.EndToggleGroup();

      if(EditorGUI.EndChangeCheck())
        SaveSettings(HandsOptions.NAME, handsOptions);
      EditorGUI.indentLevel--;
    }
  }
  void SaveSettings(string key,object obj) {
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{key}_Settings.json";
    string settingsRaw = JsonUtility.ToJson(obj);
    File.WriteAllText(settingsPath, settingsRaw);
  }
  void LoadFaceMeshSetting(){
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{FaceMeshOptions.NAME}_Settings.json";
    try {
      faceMeshOptions = (FaceMeshOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(FaceMeshOptions));
    }
    
    catch { faceMeshOptions = new FaceMeshOptions(); }
  }
  void LoadHandsOptions(){
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{HandsOptions.NAME}_Settings.json";
    try {
      handsOptions = (HandsOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(HandsOptions));
    }
    catch {handsOptions = new HandsOptions();}
  }

}
 