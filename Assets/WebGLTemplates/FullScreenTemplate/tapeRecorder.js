class TapeRecorder{
  constructor(moduleName){
    this.mediapipeTape = "";
    this.record = false;
    this.moduleName = moduleName;
    this.buttonElement = this.createButton();
  }

  recordEpisode(data){
    this.mediapipeTape += data + "/";
  }

  createButton() {
    let recordBtn = document.createElement("button");
    recordBtn.innerHTML = this.buttonLabel;
    recordBtn.onclick = this.toggleRecord.bind(this);
    document.querySelector(".record-buttons-wrapper").appendChild(recordBtn);
    return recordBtn;
  }

  toggleRecord(){
    this.record = !this.record;
    this.buttonElement.innerHTML = this.buttonLabel;
    if(!this.record){
      this.mediapipeTape = this.mediapipeTape.slice(0, -1);
      this.copyTapeToClipboard();
      alert("Tape copied to clipboard");
      this.mediapipeTape = "";
    }
  }

  get buttonLabel(){
    return this.moduleName + " | " + (this.record ? "STOP" : "START") + " RECORD";
  }

  copyTapeToClipboard(){
      let el = document.createElement('textarea');
      el.value = this.mediapipeTape;
      document.body.appendChild(el);
      el.select();
      document.execCommand('copy');
      document.body.removeChild(el);
  }
}

window.TapeRecorder = TapeRecorder;