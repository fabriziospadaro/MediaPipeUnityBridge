'use strict';

class GenericTrackableModule {
  static DESTROY_STATE = "-1";
  static EXIT_STATE = "0";
  static ENTER_STATE = "1";
  static STAY_STATE = "2";

  constructor(moduleName, model, options){
    this.state = GenericTrackableModule.EXIT_STATE;
    this.moduleName = moduleName;
    this.model = model;
    this.model.setOptions(options);
    this.model.onResults(this.onResults.bind(this));
    cameraListeners.push(this.model);
    this.mediapipeTape = "";
    this.record = false;
    this.resultKey = this.detectResultKey;
  }

  onResults(results) {
    let serializedData;
    if(typeof(unityEditor) != "undefined" && results[this.resultKey] && results[this.resultKey].length > 0){
      let serializedPoints = serializeLandmarks(results[this.resultKey][0]);
      if(
        this.faceState == GenericTrackableModule.EXIT_STATE ||
        this.faceState == GenericTrackableModule.DESTROY_STATE
      )
        this.faceState = GenericTrackableModule.ENTER_STATE;
      else
        this.faceState = GenericTrackableModule.STAY_STATE;
      serializedData = `${this.moduleName}|${this.faceState}|${serializedPoints}`;
    }
    else{
      this.faceState = this.faceState !== GenericTrackableModule.EXIT_STATE ? GenericTrackableModule.EXIT_STATE : GenericTrackableModule.DESTROY_STATE;
      serializedData = `${this.moduleName}|${this.faceState}`;
    }
  
    if(this.record)
      this.mediapipeTape += serializedData + "/";
    unityEditor.SendMessage("MediaPipeBridge", "OnResult", serializedData);
  }

  recordTape(){
    record = true;
    document.getElementById(`stop${this.moduleName}Tape`).style.visibility = "visible";
    document.getElementById(`start${this.moduleName}Tape`).style.visibility = "hidden";
  }
  
  stopTape(){
    record = false;
    document.getElementById(`stop${this.moduleName}Tape`).style.visibility = "hidden";
    document.getElementById(`start${this.moduleName}Tape`).style.visibility = "visible";
    mediapipeTape = mediapipeTape.slice(0, -1);
    console.log(mediapipeTape);
  }

  get detectResultKey(){
    switch(this.moduleName){
      case "FaceMesh": return "multiFaceLandmarks";
      case "Hands": return "multiHandLandmarks"
    }
  }
}

window.GenericTrackableModule = GenericTrackableModule;

