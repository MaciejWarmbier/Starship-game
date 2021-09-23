using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faller : MonoBehaviour
{

    [SerializeField] GameObject playerShip;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = FindDistance();
        Debug.Log(distance);
        if(distance < 10){
            rb.useGravity = true;
        }

    }

    private float FindDistance()
    {
        Vector3 playerShipVector = playerShip.transform.position;
        Vector3 objectVector = gameObject.transform.position;
        //Debug.Log("Ship " + playerShipVector + "   Object   " + objectVector);
        float distance = Vector3.Distance(playerShipVector, objectVector);

        return distance;
    }
}
