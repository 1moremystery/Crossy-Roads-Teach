# Lesson 3

## Prevent moving into trees on side
1. Move animate move to vector to its own
1. add check for if z is between 6 and -6
```
public void Move(InputAction.CallbackContext callbackContext)
{
    Vector2 v = callbackContext.ReadValue<Vector2>();
    if (!callbackContext.performed || v == Vector2.zero || isMoving) return;

    Vector3 wantedPos = new(Body.position.x + v.y, 0.7f, Body.position.z - v.x);
    if(wantedPos.z >= 6 || wantedPos.z <= -6) return;
    isMoving = true;
    StartCoroutine(AnimateMoveTo(wantedPos));
}
```

## Procedural terrain
- Create Grass script
- Attach grass script to the parent of the grass prefab
- Create empty gameobject "TerrainHolder" then a Transform for terrain holder
```
[SerializeField] private Transform TerrainHolder;
[SerializeField] private Grass grassPrefab;
private int spawnPos = 1;
[SerializeField] private int spawnDistance = 20;

private void NewLevel()
{
    spawnPos = 1;
    Body.position = new(0, 0.7f, 0);
    foreach(Transform child in TerrainHolder)
    {
        Destroy(child.gameObject);
    }

    for(int i = 0;  i < spawnDistance; i++)
    {
        SpawnObstacle();
    }
}

private void SpawnObstacle()
{
    Grass g = Instantiate(grassPrefab, TerrainHolder);
    g.Init(spawnPos);
    spawnPos++;
}
```
- Add `if (spawnPos - wantedPos.x < spawnDistance) SpawnObstacle();` to Move function
- Grass
```
public class Grass : MonoBehaviour
{
    public void Init(int position)
    {
        transform.position = new(position,0);
    }
}
```

## Road
- Create road parent and road materials, then road script
- Create car prefab out of a cube
    - scale x= 0.85, y=1, z=2
```
public class Road : MonoBehaviour
{
    [SerializeField] private Rigidbody vehiclePrefab;
    [SerializeField] private float speed;
    [SerializeField] private int direction;

    List<Rigidbody> Vehicles = new();

    public void Init(int pos)
    {
        transform.position = new(pos, 0);
        speed = Random.Range(1f, 5f);
        direction = 2 * Random.Range(0, 2) - 1;


        int vehicleCount = Random.Range(1, 5);
        float gap = Random.Range(2f, 6f);

        for (int i = 0; i < vehicleCount; i++)
        {
            Rigidbody vic = Instantiate(vehiclePrefab,
                new Vector3(pos, 0.7f, (i*gap) * direction),
                Quaternion.Euler(0, 180 * direction, 0),
                transform);
            Vehicles.Add(vic);
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody v in Vehicles)
        {
            Vector3 moveAmount = new(0, 0, speed * direction * Time.deltaTime);
            v.MovePosition(v.position + moveAmount);

            Vector3 pos = v.position;
            if(direction > 0 && pos.z > 11)
            {
                pos.z = -11;
                v.position = pos;
            }
            else if(pos.z < -11)
            {
                pos.z = 11;
                v.position = pos;
            }
        }
    }
}
```
- spawn probability
```
[SerializeField] private float roadProbability = 0.5f;
private void SpawnObstacle()
{
    if (Random.value < roadProbability)
    {
        Road r = Instantiate(roadPrefab, TerrainHolder);
        r.Init(spawnPos);
    }
    else
    {
        Grass g = Instantiate(grassPrefab, TerrainHolder);
        g.Init(spawnPos);
    }
    spawnPos++;
}
```