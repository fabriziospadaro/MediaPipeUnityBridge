function onResults(results) {
  if(typeof(unityEditor) != "undefined" && !!results.objectDetections){
    console.log(results.objectDetections[0]);
    let serializedPoints = serializeLandmarks(results.objectDetections[0].keypoints.map(x => x.point3d));

    if(recordF)
      mediapipeTapeF += "Objectron|"+serializedPoints + "/";
    
    console.log(serializedPoints);
    unityEditor.SendMessage("MediaPipeBridge", "OnLandmarksCollected", "Objectron|"+serializedPoints);
    
  }
}

const objectron = new Objectron({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/objectron/${file}`;
}});
objectron.setOptions({
  modelName: 'Shoe',
  maxNumObjects: 3,
});
objectron.onResults(onResults);
cameraListeners.push(objectron);


let mediapipeTapeF = "";
let recordF = false;

function startObjectronRecord(){
  recordF = true;
  document.getElementById("stopObjectronRecord").style.visibility = "visible";
  document.getElementById("startObjectronRecord").style.visibility = "hidden";
}

function stopObjectronRecord(){
  recordF = false;
  document.getElementById("stopObjectronRecord").style.visibility = "hidden";
  document.getElementById("startObjectronRecord").style.visibility = "visible";
  mediapipeTapeF = mediapipeTapeF.slice(0, -1);
  console.log(mediapipeTapeF);
}

window.startObjectronRecord = startObjectronRecord;
window.stopObjectronRecord = stopObjectronRecord;
