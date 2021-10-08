window.cameraListeners = [];

const videoElement = document.getElementsByClassName('input_video')[0];

let cam = new Camera(videoElement, {
	onFrame: async () => {
	  for(listener of cameraListeners)
	  	await listener.send({image: videoElement});
	},
	width: window.screen.width,
	height: window.screen.height
 })

cam.start();

window.onresize = refreshCameraSize;

function refreshCameraSize(){
	cam.width = window.screen.width;
	cam.height = window.screen.height;
}