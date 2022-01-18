let faceMeshModel = new FaceMesh({locateFile: (file) => {
	return `https://cdn.jsdelivr.net/npm/@mediapipe/face_mesh/${file}`;
}});

let faceOptions = %OPTION%;

window.faceMeshModule = new GenericTrackableModule("FaceMesh", faceMeshModel, faceOptions);