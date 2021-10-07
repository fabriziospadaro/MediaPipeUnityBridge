function onResults(results) {
  if(typeof(unityEditor) != "undefined" && results.multiHandLandmarks && results.multiHandLandmarks.length > 0){
    let serializedPoints = serializeLandmarks(results.multiHandLandmarks[0]);

    if(recordH)
      mediapipeTapeH += "Hands|"+serializedPoints + "/";
    unityEditor.SendMessage("MediaPipeBridge", "OnLandmarksCollected", "Hands|"+serializedPoints);
  }
}

const hands = new Hands({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`;
}});

hands.setOptions({
  maxNumHands: 2,
  minDetectionConfidence: 0.5,
  minTrackingConfidence: 0.5
});
hands.onResults(onResults);

cameraListeners.push(hands);


let mediapipeTapeH = "";
let recordH = false;

function startHandsRecord(){
  recordH = true;
  document.getElementById("stopHandsRecord").style.visibility = "visible";
  document.getElementById("startHandsRecord").style.visibility = "hidden";
}

function stopHandsRecord(){
  recordH = false;
  document.getElementById("stopHandsRecord").style.visibility = "hidden";
  document.getElementById("startHandsRecord").style.visibility = "visible";
  mediapipeTapeH = mediapipeTapeH.slice(0, -1);
  console.log(mediapipeTapeH);
}


window.startHandsRecord = startHandsRecord;
window.stopHandsRecord = stopHandsRecord;
