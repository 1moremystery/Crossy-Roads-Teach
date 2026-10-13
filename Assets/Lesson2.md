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
- Prefabs
- Materials

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
Make sure to Tag the main camera as "MainCamera" or else there will be errors

Clamp values are arbitrary, and will probably need to be changed later

## Grass Prefab
1. Create Empty object, name it grass
1. Reset grass transform
1. Create cube under the grass object
1. Reset transforms
1. Scale to 1, 0.2, 11
1. Position to 0, 0.1, 0
1. Under materials folder, right click > create > create material
1. Make material green then put on grass cube
1. Duplicate the grass material, make it a darker green, name it "Grass OOB" for grass out of bounds
1. Duplicate the grass cube and move it z = +/- 11
1. Add the grass oob material to duplicated grass cubes
1. Make cubes to act as a tree and move them to the start of the oob area (y=+/-6)