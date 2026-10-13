using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = new(Body.position.x,4,Body.position.z-3);
        cameraPos.z = Mathf.Clamp(cameraPos.z, -5, 6);
        Camera.main.transform.position = cameraPos;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        Vector2 v = callbackContext.ReadValue<Vector2>();
        Body.AddForce(new(v.y, 0, -v.x));
    }

    public void StopMovement()
    {
        Body.linearVelocity = Vector3.zero;
    }
}
