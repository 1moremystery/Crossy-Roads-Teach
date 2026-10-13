# Lesson 2

## Change to URP which I thought I did
1. Top bar > Window > Package Management > Package Manager
    - Make Sure Universal Render Pipeline is installed

2. Right click on folder > Create > Rendering > URP Asset (with Universal Renderer)

3. Top bar > Edit > Project Settings > Graphics 
    - Drag the asset into the place where it says "Default Render Pipeline"
    - Everything will turn pink

4. Project Panel > Packages > Universal Render Pipeline > Runtime > Materials
    - Drag Lit onto the cube and plane

## Organize Stuff into folders
- Settings
    - URP Settings and other stuff
- Scripts
- Scenes
- Models

## Scene Changes
Gonna flip things around so we'll be moving in x positive directions
- Reset cube position to 0, 0.5, 0
- Camera position looks good at 0, 4, -3 rotation 42, 53, 0
- Change camera to orthographic - size 3
- Add directional light

## Other Major changes 
1. Rename Movement script into GameManager (Both filename and class name)
2. Remove Movement script from cube
3. Remove Player input component from cube
4. Drag the GameManager into the hierarchy so it creates its own empty gameObject
5. You will have to re-setup the rigidBody and movement event in the cube

## Movement Script
1. Go into input actions asset
    - Delete any things already in movement
    - change movement to Action Type: Value - Control Type: Vector 2
    - Right click add up/right/left/down composite and set it up

2. Add InputAction.CallbackContext value in function

3. Add `Vector2 v = callbackContext.ReadValue<Vector2>()` then set the add force value to v

4. Go into cube's player input and set it to use the new Move function which should be at the top

5. If RigidBody gravity is off, notice the cube goes in the wrong direction

6. `Body.AddForce(new(v.y, 0, -v.x));` because w and s (up/down) are the y in this situation and left is considered negative for whatever reason


## Camera Movement
```
void Update()
{
    Vector3 cameraPos = new(Body.position.x,4,Body.position.z-3);
    cameraPos.z = Mathf.Clamp(cameraPos.z, -5, 6); 
    Camera.main.transform.position = cameraPos;
}
```
Tag the main camera as "MainCamera" or else there will be errors

Clamp values are arbitrary, and will probably need to be changed later