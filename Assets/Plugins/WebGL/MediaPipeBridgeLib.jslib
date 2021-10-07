/*
mergeInto(LibraryManager.library, {
  LoadMPModule: function (moduleName) {
    var name = Pointer_stringify(moduleName);
    var urlBase = "https://cdn.jsdelivr.net/npm/@mediapipe/";
    
    mapping = {
      "FaceMesh": "face_mesh/face_mesh.js",
      "Pose": "pose/pose.js",
      "Hands": "hands/hands.js",
      "Objectron": "objectron/objectron.js"
    }
    
    var script = document.createElement('script');
    script.src = urlBase+mapping[name];
    script.setAttribute('crossorigin','anonymous');

    script.onload = function () {
      var moduleScript = document.createElement('script');
      moduleScript.src = name+".js";
      document.head.appendChild(moduleScript);
    };

    document.head.appendChild(script);
  },
});
*/