window.addEventListener('load', (event) => {
	var buildUrl = "Build";
	var loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
	var config = {
	  dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
	  frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
	  codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
	#if MEMORY_FILENAME
	  memoryUrl: buildUrl + "/{{{ MEMORY_FILENAME }}}",
	#endif
	#if SYMBOLS_FILENAME
	  symbolsUrl: buildUrl + "/{{{ SYMBOLS_FILENAME }}}",
	#endif
	  streamingAssetsUrl: "StreamingAssets",
	  companyName: "{{{ COMPANY_NAME }}}",
	  productName: "{{{ PRODUCT_NAME }}}",
	  productVersion: "{{{ PRODUCT_VERSION }}}",
	  backgroundColor: "transparent"
	};
	var script = document.createElement("script");
	script.src = loaderUrl;
	let unityEditor = null;
	let glCtx = null;

	script.onload = () => {
	  createUnityInstance(document.getElementById("unity-canvas"),config,null).then(onSuccess);
	};

	document.body.appendChild(script);

	function onSuccess(u){
		unityEditor = u;
		glCtx = u.Module.ctx;
		window.glCtx = glCtx;
		window.unityEditor = unityEditor;
		refreshUnityRatio();
		window.onresize = refreshUnityRatio;
	}

	const videoElement = document.getElementsByClassName('input_video')[0];
	const faceMesh = new FaceMesh({locateFile: (file) => {
	  return `https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh/${file}`;
	}});

	faceMesh.setOptions({
	  maxNumFaces: 1,
	  minDetectionConfidence: 0.5,
	  minTrackingConfidence: 0.5
	});
	faceMesh.onResults(onResults);

	const camera = new Camera(videoElement, {
	  onFrame: async () => {
	    await faceMesh.send({image: videoElement});
	  },
	  width: window.screen.width,
	  height: window.screen.height
	});
	camera.start();

	function onResults(results) {
		if(results.multiFaceLandmarks[0] && results.multiFaceLandmarks[0].length > 0){
			let serializedPoints = "";
			
			for(let i = 0; i < results.multiFaceLandmarks[0].length;i++){
				let point = results.multiFaceLandmarks[0][i];
				serializedPoints += `${point.x}*${point.y}*${point.z}*`;
			}

			serializedPoints = serializedPoints.slice(0, -1);
			if(record){
				mediapipeTape += serializedPoints + "/";
			}
			if(unityEditor)
				unityEditor.SendMessage("MediaPipeBridge", "OnFacePointsCalculated", serializedPoints);
		}
	}

});

function refreshUnityRatio() {
	let ratio = document.querySelector(".input_video").videoHeight / document.querySelector(".input_video").videoWidth;
	document.getElementById("unity-canvas").style.width = window.innerWidth + "px";
	document.getElementById("unity-canvas").style.height = window.innerWidth * ratio + "px";
	console.log("RESIZE HAPPEND");
}



let mediapipeTape;
let record = false;
function startMediaPipeRecord(){
	record = true;
	document.getElementById("stop").style.visibility = "visible";
	document.getElementById("start").style.visibility = "hidden";
}

function stopMediaPipeRecord(){
	record = false;
	document.getElementById("stop").style.visibility = "hidden";
	document.getElementById("start").style.visibility = "visible";
	mediapipeTape = mediapipeTape.slice(0, -1);
	console.log(mediapipeTape);
}
