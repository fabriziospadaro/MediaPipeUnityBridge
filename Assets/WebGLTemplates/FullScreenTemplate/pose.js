function onResults(results) {
  if(typeof(unityEditor) != "undefined" && results.poseLandmarks && results.poseLandmarks.length > 0){
    let serializedPoints = serializeLandmarks(results.poseLandmarks);

    if(recordP)
      mediapipeTapeP += "Pose|"+serializedPoints + "/";
    
    unityEditor.SendMessage("MediaPipeBridge", "OnLandmarksCollected", "Pose|"+serializedPoints);
  }
}

const pose = new Pose({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/pose/${file}`;
}});

pose.setOptions({
  modelComplexity: 1,
  smoothLandmarks: true,
  enableSegmentation: true,
  smoothSegmentation: true,
  minDetectionConfidence: 0.5,
  minTrackingConfidence: 0.5
});
pose.onResults(onResults);
cameraListeners.push(pose);

let mediapipeTapeP = "";
let recordP = false;

function startPoseRecord(){
  recordP = true;
  document.getElementById("stopPoseRecord").style.visibility = "visible";
  document.getElementById("startPoseRecord").style.visibility = "hidden";
}

function stopPoseRecord(){
  recordP = false;
  document.getElementById("stopPoseRecord").style.visibility = "hidden";
  document.getElementById("startPoseRecord").style.visibility = "visible";
  mediapipeTapeP = mediapipeTapeP.slice(0, -1);
  console.log(mediapipeTapeP);
}

window.startPoseRecord = startPoseRecord;
window.stopPoseRecord = stopPoseRecord;
