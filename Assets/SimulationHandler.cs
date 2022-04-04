using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BinObjectPool))]
public class SimulationHandler : MonoBehaviour
{
    public bool visualize = true;
    BinObjectPool pool;

    ParameterizedBox box;

    List<Rigidbody> rigidbodies = new List<Rigidbody>();

    SimulationStatusHandler status;

    void Start()
    {
        pool = GetComponent<BinObjectPool>();
        box = GetComponentInChildren<ParameterizedBox>();
        status = new SimulationStatusHandler();

        PopulateBin();
        if(!visualize) Simulate();
    }

    void PopulateBin()
    {
        Rigidbody rb;
        while ((rb = pool.SpawnObject(box.RandomPositionWithin(), Random.rotation)) != null)
        {
            rigidbodies.Add(rb);
        }

        Debug.Log("Populating Bin");
        status.Value = SimulationStatus.Active;
    }

    void Simulate()
    {
        Debug.Log("Simulating...");
        Physics.autoSimulation = false;
        int maxIterations = 1000;
        for (int i = 0; i < maxIterations; i++)
        {
            Physics.Simulate(Time.fixedDeltaTime);
            if(CheckObjects()) break;
        }

        Physics.autoSimulation = true;
    }

    void CleanUpBin()
    {
        Debug.Log("Starting cleanup.");
        foreach (Rigidbody rb in rigidbodies)
        {
            pool.ReturnObject(rb);
        }
        rigidbodies.Clear();

        status.Value = SimulationStatus.Inactive;
    }

    IEnumerator CleanUpRoutine()
    {
        Debug.Log("Simulation Completed");
        yield return new WaitForSeconds(1);
        CleanUpBin();
        yield return new WaitForSeconds(1);
        //PopulateBin();
        //if(!visualize) Simulate();
    }

    bool CheckObjects()
    {
        bool allSleeping = true;

        for (int i = rigidbodies.Count - 1; i >= 0; i--)
        {
            Rigidbody rb = rigidbodies[i];
            if (rb.position.y < transform.position.y)
            {
                pool.ReturnObject(rb);
                rigidbodies.RemoveAt(i);
                Debug.Log("Removed object out of bounds.");
                continue;
            }

            allSleeping &= rb.IsSleeping();
        }
        return allSleeping;
    }

    void FixedUpdate()
    {
        if (status.Value == SimulationStatus.Active)
        {
            if (CheckObjects())
            {
                status.Value = SimulationStatus.Completed;
                StartCoroutine(CleanUpRoutine());
            }
        }
    }
}
