let handsModel = new Hands({locateFile: (file) => {
  return `https://cdn.jsdelivr.net/npm/@mediapipe/hands/${file}`;
}});

let handsOptions = %OPTION%;

window.handsModule = new GenericTrackableModule("Hands",handsModel,handsOptions);