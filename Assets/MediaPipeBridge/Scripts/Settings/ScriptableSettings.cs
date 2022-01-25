using UnityEngine;

namespace MediaPipe {
  [CreateAssetMenu(fileName = "Setting", menuName = "ScriptableObject/MediaPipe/Settings", order = 1)]
  public class ScriptableSettings: ScriptableObject {
    public GeneralSettings generalSettings;
    public FaceMeshOptions faceMeshOptions;
    public HandsOptions handsOptions;
  }
}