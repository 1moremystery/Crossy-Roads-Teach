using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform Body;
    [SerializeField] private float moveDur = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = new(Body.position.x, 4, Body.position.z - 3);
        cameraPos.z = Mathf.Clamp(cameraPos.z, -5, 6);
        Camera.main.transform.position = cameraPos;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        Vector2 v = callbackContext.ReadValue<Vector2>();
        if (!callbackContext.performed || v == Vector2.zero) return;


        StartCoroutine(AnimateMoveTo(new(Body.position.x + v.y, 0.7f, Body.position.z - v.x)));
    }

    IEnumerator AnimateMoveTo(Vector3 v)
    {
        float elapsed = 0f;
        Vector3 startPos = Body.position;

        while (elapsed < moveDur)
        {
            float percent = elapsed / moveDur;
            Vector3 newPoint = Vector3.Lerp(startPos, v, percent);
            elapsed += Time.deltaTime;
            Body.position = newPoint;
            yield return null;
        }
        Body.position = v;
        Debug.Log(v);
    }
}
