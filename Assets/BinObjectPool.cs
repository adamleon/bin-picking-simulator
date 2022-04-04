using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BinObjectPool : MonoBehaviour
{
    [Range(1,200)]
    public int numberOfObjects = 50;

    public Rigidbody objectType;

    private Stack<GameObject> rigidbodies = new Stack<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < numberOfObjects; i++) {
            GameObject obj = Instantiate(objectType.gameObject, transform);
            obj.SetActive(false);
            rigidbodies.Push(obj);

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.sleepThreshold = 0.1f;
        }
    }

    public Rigidbody SpawnObject(Vector3 position, Quaternion rotation) {
        if(rigidbodies.Count <= 0) return null;
        
        GameObject obj = rigidbodies.Pop();
        obj.SetActive(true);
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        rigidbody.MovePosition(position);
        rigidbody.MoveRotation(rotation);

        return rigidbody;
    }

    public void ReturnObject(Rigidbody rigidbody) {
        if(rigidbody != null) {
            GameObject obj = rigidbody.gameObject;
            obj.SetActive(false);
            rigidbodies.Push(obj);
        }
    }
}
