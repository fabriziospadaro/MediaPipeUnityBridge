using UnityEngine;
using UnityEditor;
using System.IO;
using MediaPipe;
public class SettingsWindow: EditorWindow {
  FaceMeshOptions faceMeshOptions;
  HandsOptions handsOptions;
  GeneralSettings generalSettings;
  bool faceFoldout = true;
  bool handsFoldout = true;

  [MenuItem("MediaPipeBridge/Settings")]
  public static void ShowWindow(){
    EditorWindow.GetWindow(typeof(SettingsWindow));
  }

  public void OnEnable(){
    LoadSettings();
  }
  void LoadSettings() {
    LoadFaceMeshSetting();
    LoadHandsOptions();
    LoadGeneralSettings();
  }
  void OnGUI(){
    GeneralSettingsInspector();
    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    ModuleSettingsInspector();
  }
  void GeneralSettingsInspector() {
    GUILayout.Label("General Settings", EditorStyles.centeredGreyMiniLabel);
    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
    EditorGUILayout.Space();
    EditorGUI.BeginChangeCheck();
    generalSettings.debug = EditorGUILayout.Toggle("Debug mode", generalSettings.debug);
    EditorGUILayout.Space();
    generalSettings.sizingRule = (GeneralSettings.SizingRule)EditorGUILayout.EnumPopup("Sizing rule",generalSettings.sizingRule);
    EditorGUILayout.Space();
    EditorGUI.indentLevel++;
    switch(generalSettings.sizingRule) {
      case GeneralSettings.SizingRule.Ratio:
      generalSettings.dimension = EditorGUILayout.Vector2IntField("", generalSettings.dimension);
      generalSettings.dimension = new Vector2Int(Mathf.Clamp(generalSettings.dimension.x,0,20), Mathf.Clamp(generalSettings.dimension.y, 0, 20));
      break;
      case GeneralSettings.SizingRule.Size:
      generalSettings.dimension = EditorGUILayout.Vector2IntField("", generalSettings.dimension);
      //clamp it to 4k
      generalSettings.dimension = new Vector2Int(Mathf.Clamp(generalSettings.dimension.x, 0, 3840), Mathf.Clamp(generalSettings.dimension.y, 0, 3840));
      break;
    }
    EditorGUI.indentLevel--;
    if(EditorGUI.EndChangeCheck())
      SaveSettings(GeneralSettings.NAME,generalSettings);
    EditorGUILayout.EndHorizontal();
  }

  void ModuleSettingsInspector() {
    GUILayout.Label("Active Modules", EditorStyles.centeredGreyMiniLabel);
    EditorGUILayout.Space();
    FaceMeshInspector();
    HandsInspector();
  }
  void FaceMeshInspector() {
    GUILayout.BeginVertical("", EditorStyles.helpBox);
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
        SaveSettings(FaceMeshOptions.NAME, faceMeshOptions);
      EditorGUI.indentLevel--;
    }
    GUILayout.EndVertical();
  }
  void HandsInspector() {
    GUILayout.BeginVertical("", EditorStyles.helpBox);
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
    GUILayout.EndVertical();
  }
  void SaveSettings(string key,object obj) {
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{key}_Settings.json";
    string settingsRaw = JsonUtility.ToJson(obj);
    File.WriteAllText(settingsPath, settingsRaw);
    AssetDatabase.Refresh();
  }
  void LoadFaceMeshSetting(){
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{FaceMeshOptions.NAME}_Settings.json";
    try {
      faceMeshOptions = (FaceMeshOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(FaceMeshOptions));
    }catch {}
  }
  void LoadHandsOptions(){
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{HandsOptions.NAME}_Settings.json";
    try {
      handsOptions = (HandsOptions)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(HandsOptions));
    }catch {}
  }
  void LoadGeneralSettings(){
    string settingsPath = Application.dataPath + $"/MediaPipeBridge/Scripts/Settings/{GeneralSettings.NAME}_Settings.json";
    try {
      generalSettings = (GeneralSettings)JsonUtility.FromJson(File.ReadAllText(settingsPath), typeof(GeneralSettings));
    }
    catch { }
  }
}
 