## Installation

There is several options to install this package:
- Asset Store
- UPM
- directly in manifest

### Asset Store

Go to asset store and 'Add to My Assets' this package `https://assetstore.unity.com/packages/tools/integration/extended-buttons-152732`

### Unity Package Manager

Open Unity Package Manager and go to **Add package from git URL...** and paste `https://github.com/ostrzolekpawel/UnityExtendedButtons.git?path=Assets/ExtendedButtons/Assets`

### Manifest
Add link to package from repository directly to manifest.json

**Example**
```json
{
    "dependencies": {
        // other packages
        // ...
        "com.osirisgames.unityextendedbuttons": "https://github.com/ostrzolekpawel/UnityExtendedButtons.git?path=Assets/ExtendedButtons/Assets"
    }
}
```

## Infomration
With this plugin you can extend UI buttons and simply add to 3D objects button like behaviour.
Simply plugin to speed up work with UI and gameObject interactions. With this solution you can rid off OnMouse... methods and
raycasting to objects;

Button3D require Collider component and provide events:
- onEnter
- onDown
- onUp
- onClick
- onExit
- onBeginDrag
- onDrag
- onEndDrag

Button2D require Canvas Element component (ICanvasElement) and provide events:
- onEnter
- onDown
- onUp
- onClick
- onExit

Button2DExtended require Canvas Element component (ICanvasElement) and provide events:
- onEnter
- onDown
- onUp
- onClick
- onExit
- onBeginDrag
- onDrag
- onEndDrag

3D buttons require prefab with listener which manage all logic.
Package contains logic and example scripts, sample scenes also one input system for mouse and touch

HOW TO USE:
To manage 3D buttons place prefab with listener on scene ("InputSystem" or "ButtonsListenerBasic") then add to 3D Object script "Button3D".
Now Listener for 3D buttons adds automatically.
To extend UI buttons replace button component with "Button2D"
