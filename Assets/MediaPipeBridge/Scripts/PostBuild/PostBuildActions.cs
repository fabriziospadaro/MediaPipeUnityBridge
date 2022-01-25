using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#endif

  public class PostBuildActions {
    #if UNITY_EDITOR

    const string modulePlaceholder = "--INJECTED_MODULES--";
    const string optionPlaceholder = "%OPTION%";
    const string debugPlaceholder = "%DEBUG_MODE%";
    const string dimensionPlaceholder = "%DIMENSION%";

  [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string targetPath){
      if(PlayerSettings.WebGL.template.Contains("FullScreenTemplate")) {
        replacePlaceholder(targetPath);
      }
    }

    static void replacePlaceholder(string targetPath){
      string html = "";
      //put script here
      var urlBase = "https://cdn.jsdelivr.net/npm/@mediapipe/";
      Dictionary<string, string> mapping = new Dictionary<string, string>() {
          { "FaceMesh", "face_mesh/face_mesh.js" },
          { "Pose", "pose/pose.js" },
          { "Hands", "hands/hands.js" },
          { "Objectron", "objectron/objectron.js" }
      };

      foreach(var module in MediaPipe.Settings.enabledModules) {
          html += $"<script src=\"{urlBase+ mapping[module]}\" crossorigin></script>\n";
          var snakeCaseModuleName = module.First().ToString().ToLower() + module.Substring(1);
          html += $"<script src=\"{module}.js\"></script>\n";

          ReplaceAt(Path.Combine(targetPath, $"{module}.js"), optionPlaceholder, MediaPipe.Settings.Obj2String(module));
      }

      ReplaceAt(Path.Combine(targetPath, "index.html"), modulePlaceholder, html);

      MediaPipe.GeneralSettings generalSettings = MediaPipe.Settings.getJson.generalSettings;
      ReplaceAt(Path.Combine(targetPath, "genericTrackableModule.js"), debugPlaceholder, generalSettings.debug.ToString().ToLower());
      ReplaceAt(Path.Combine(targetPath, "cameraManager.js"), dimensionPlaceholder, GetDimensionBySetting(generalSettings));
    }

    static void ReplaceAt(string path, string placeholder, string replace) {
        var text = File.ReadAllText(path);
        text = text.Replace(placeholder, replace);
        File.WriteAllText(path, text);
      }

    static string GetDimensionBySetting(MediaPipe.GeneralSettings generalSettings) {
      switch(generalSettings.sizingRule) {
        case MediaPipe.GeneralSettings.SizingRule.FullSize:
        return @"width: window.screen.width,height: window.screen.height";
        case MediaPipe.GeneralSettings.SizingRule.CustomSize:
        return @$"width: {generalSettings.dimension.x},height: {generalSettings.dimension.y}";
        case MediaPipe.GeneralSettings.SizingRule.Ratio:
        return @$"aspectRatio: {generalSettings.dimension.x}/{generalSettings.dimension.y}.0";
      }
      return "";
    }

#endif
}