# UnityExtendedButtons
Copyright (c) 2019 Paweł Ostrzołek

This plugin is provided 'as-is', without any express or implied warranty. In
no event will the authors be held liable for any damages arising from the use
of this plugin.

Permission is granted to anyone to use this plugin for any purpose,
including commercial applications, and to alter it and redistribute it freely,
subject to the following restrictions:

1. The origin of this plugin must not be misrepresented; you must not claim
that you wrote the original plugin. If you use this plugin in a product,
an acknowledgment in the product documentation would be appreciated but is not
required.

2. Altered source versions must be plainly marked as such, and must not be
misrepresented as being the original plugin.

3. This notice may not be removed or altered from any source distribution.

=============================================================================

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
