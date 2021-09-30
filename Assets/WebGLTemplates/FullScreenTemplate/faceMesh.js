window.addEventListener('load', (event) => {

	function onResults(results) {
		if(typeof(unityEditor) != "undefined" && results.multiFaceLandmarks[0] && results.multiFaceLandmarks[0].length > 0){
			let serializedPoints = serializeLandmarks(results.multiFaceLandmarks[0]);

			if(record)
				mediapipeTape += serializedPoints + "/";
			unityEditor.SendMessage("MediaPipeBridge", "OnFaceLandmarksCollected", serializedPoints);
		}
	}

	const faceMesh = new FaceMesh({locateFile: (file) => {
	  return `https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh/${file}`;
	}});

	faceMesh.setOptions({
	  maxNumFaces: 1,
	  minDetectionConfidence: 0.5,
	  minTrackingConfidence: 0.5
	});
	faceMesh.onResults(onResults);
	cameraListeners.push(faceMesh);
});

let mediapipeTape;
let record = false;
function startFaceRecord(){
	record = true;
	document.getElementById("stopFaceRecord").style.visibility = "visible";
	document.getElementById("startFaceRecord").style.visibility = "hidden";
}

function stopFaceRecord(){
	record = false;
	document.getElementById("stopFaceRecord").style.visibility = "hidden";
	document.getElementById("startFaceRecord").style.visibility = "visible";
	mediapipeTape = mediapipeTape.slice(0, -1);
	console.log(mediapipeTape);
}
