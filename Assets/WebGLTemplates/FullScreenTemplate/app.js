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
	let ratio = document.querySelector(".input_video").videoHeight / document.querySelector(".input_video").videoWidth;
	document.getElementById("unity-canvas").style.width = window.innerWidth + "px";
	document.getElementById("unity-canvas").style.height = window.innerWidth * ratio + "px";
}
