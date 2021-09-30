window.serializeLandmarks = (landMarks) => {
	let serializedPoints = "";
	for(let i = 0; i < landMarks.length;i++){
		let point = landMarks[i];
		serializedPoints += `${point.x}*${point.y}*${point.z}*`;
	}
	serializedPoints = serializedPoints.slice(0, -1);
	return serializedPoints;
};