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
    if(%DEBUG_MODE%){
      window.addEventListener('load', (event) => {
        this.tapeRecorder = new TapeRecorder(moduleName);
      });
    }
    this.resultKey = this.detectResultKey;
  }

  onResults(results) {
    let completeData = `${this.moduleName}|`;
    if(typeof(unityEditor) != "undefined" && results[this.resultKey] && results[this.resultKey].length > 0){
        console.log(results);
        for(let points of results[this.resultKey]){
          let serializedPoints = serializeLandmarks(points);
          if(
            this.faceState == GenericTrackableModule.EXIT_STATE ||
            this.faceState == GenericTrackableModule.DESTROY_STATE
          )
            this.faceState = GenericTrackableModule.ENTER_STATE;
          else
            this.faceState = GenericTrackableModule.STAY_STATE;
            //completeData += `${this.faceState}$${serializedPoints}^`;
            completeData += `2$${serializedPoints}^`;
      }
      completeData = completeData.slice(0, -1);
    }
    /*
    else{
      this.faceState = this.faceState !== GenericTrackableModule.EXIT_STATE ? GenericTrackableModule.EXIT_STATE : GenericTrackableModule.DESTROY_STATE;
      let completeData = `${this.moduleName}`;
    }
*/
    //todo remove last char
    
    this.tapeRecorder?.recordEpisode(completeData);
    unityEditor.SendMessage("MediaPipeBridge", "OnResult", completeData);
  }

  get detectResultKey(){
    switch(this.moduleName){
      case "FaceMesh": return "multiFaceLandmarks";
      case "Hands": return "multiHandLandmarks"
    }
  }
}

window.GenericTrackableModule = GenericTrackableModule;

