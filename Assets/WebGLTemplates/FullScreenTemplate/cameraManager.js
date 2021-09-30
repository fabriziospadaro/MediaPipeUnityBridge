window.cameraListeners = [];

const videoElement = document.getElementsByClassName('input_video')[0];

new Camera(videoElement, {
	onFrame: async () => {
	  for(listener of cameraListeners)
	  	await listener.send({image: videoElement});
	},
	width: window.screen.width,
	height: window.screen.height
 }).start();