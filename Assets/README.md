# AR-Renderer-Unityhttp(s)

Unity 2021.x or later is required.
Developed on Unity 2021.2.7f1.

##Image recognition
1. In hierarchy press RMB -> AR-Target-Tracking-Scene
2. In hierarch select "AR-Renderer-Target-Track-Controller/" and set whatever you need to display as child of "AR-Renderer-Scene".
3. Copy the target image into the project "Assets/Targets" folder. (!IMPORTANT Only .png images are supported)
4. In hierarchy select "AR-Renderer-Target-Track-Controller/AR-Renderer-Target-Preview". Drag and drop your target image into "Traget Preview (Script)" "Image" field.
5. Change target platform to WebGL.
6. In "Edit/Player Settings/Player/Resolution and Presentation" select "AR-Renderer" WebGL template.
7. In "Edit/Player Settings/Player/Publishing Settings" change compression to Disabled.
8. If necessary set base href (or leave it empty. By default address is "http(s)://{yourip} or {localhost}:3000/") (For example, if you set href to "exampleHref/", all links will point to "http(s)://{yourip} or {localhost}:3000/exampleHref/" by default. It will be useful for Github Pages for example);
9. Done.

##Surface recognition preparation
1. In hierarchy press RMB -> AR-Surface-Tracking-Scene
2. In hierarch select "AR-Renderer-Surface-Track-Controller/" and set whatever you need to display as child of "AR-Renderer-Scene".
3. Create ui element for "AR-Renderer-Surface-Track-Controller/SurfaceTrackController (Script)/ScanSurface" function. (This function should be called manually to start scanning the surface). 
4. Change target platform to WebGL.
5. In "Edit/Player Settings/Player/Resolution and Presentation" select "AR-Renderer" WebGL template.
6. In "Edit/Player Settings/Player/Publishing Settings" change compression to Disabled.
7. If necessary set base href (or leave it empty for default value) (by default address is "http(s)://{yourip} or {localhost}:3000/") (For example, if you set href to "exampleHref/", all links will point to "http(s)://{yourip} or {localhost}:3000/exampleHref/" by default. It will be useful for Github Pages for example);
8. Done.

##Surface recognition process
1. Place your phone parallel to the surface. It is preferable that it be some object (bussines card, copybook, etc.) and not a clean surface.
2. Press on "ScanSurface" ui element ("Surface recognition preparation" block, point 4).
3. Done.

##Face recognition
1. In hierarchy press RMB -> AR-Face-Tracking-Scene
2. In hierarch select "AR-Renderer-Face-Track-Controller/" and set whatever you need to display as child of "AR-Renderer-Scene" (For correct custom model tracking and animation it should be located as close to default facemesh as possible).
3. Change target platform to WebGL.
4. In "Edit/Player Settings/Player/Resolution and Presentation" select "AR-Renderer" WebGL template.
5. In "Edit/Player Settings/Player/Publishing Settings" change compression to Disabled.
6. If necessary set base href (or leave it empty for default value) (by default address is "http(s)://{yourip} or {localhost}:3000/") (For example, if you set href to "exampleHref/", all links will point to "http(s)://{yourip} or {localhost}:3000/exampleHref/" by default. It will be useful for Github Pages for example);
7. Done.

Additional information about animation of custom facemesh models you can find in "Assets/AR-Renderer/FaceMesh/Readme.md"


## Additional information
To track certain events, you can subscribe to the desired event using a static class EventBus ("Assets/AR-Renderer/Scripts/EventBus"). (For example: EventBus.onTargetFound += "Your function");
These events are invoked automatically.
For more information about events check "Assets/AR-Renderer/Scripts/EventBus.cs".


