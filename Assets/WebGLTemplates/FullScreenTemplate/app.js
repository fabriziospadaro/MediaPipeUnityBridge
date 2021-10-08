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

});

function refreshUnityRatio() {
	let data = videoDimensions(document.querySelector(".input_video"));
	document.getElementById("unity-canvas").style.width = data["width"]+ "px";
	document.getElementById("unity-canvas").style.height = data["height"] + "px";
}

//10x https://stackoverflow.com/questions/17056654/getting-the-real-html5-video-width-and-height
function videoDimensions(video) {
  // Ratio of the video's intrisic dimensions
  var videoRatio = video.videoWidth / video.videoHeight;
  // The width and height of the video element
  var width = video.offsetWidth, height = video.offsetHeight;
  // The ratio of the element's width to its height
  var elementRatio = width/height;
  // If the video element is short and wide
  if(elementRatio > videoRatio) width = height * videoRatio;
  // It must be tall and thin, or exactly equal to the original ratio
  else height = width / videoRatio;
  return {
    width: width,
    height: height
  };
}
