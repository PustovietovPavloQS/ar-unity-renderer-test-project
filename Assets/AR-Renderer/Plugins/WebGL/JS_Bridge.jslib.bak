mergeInto(LibraryManager.library, {
	LoadScene: function (scene, index, controller, targetImg) {
		let s = UTF8ToString(scene);
		let i = UTF8ToString(index);
		let c = UTF8ToString(controller);
		let t = UTF8ToString(targetImg);
		window.SCENE_LOADER.LoadScene(s, i, c, t);
	},
	
	ScanSurfaceCmd: function(index, controller,){
		let i = UTF8ToString(index);
		let c = UTF8ToString(controller);
		window.SCENE_LOADER.ScanSurface(i, c);
	}
});