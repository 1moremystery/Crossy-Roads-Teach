using System.Collections.Generic;
using UnityEngine;

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
