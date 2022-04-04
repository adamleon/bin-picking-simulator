using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BinObject : MonoBehaviour
{
    new Rigidbody rigidbody;

    public bool IsSleeping {get { return rigidbody.IsSleeping();}}

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.sleepThreshold = 0.1f;
    }

    public void SetTransform(Vector3 position, Quaternion rotation) {
        rigidbody.MovePosition(position);
        rigidbody.MoveRotation(rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < transform.parent.position.y) {
            Debug.Log("Moving object out of bounds.");
            ParameterizedBox box = GetComponentInChildren<ParameterizedBox>();
            rigidbody.MovePosition(box.RandomPositionWithin()); 
        }
    }
}
