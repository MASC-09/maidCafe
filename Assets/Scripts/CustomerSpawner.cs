using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject Remy;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private int spawnLimit = 6;
    private int customerSpawned = 0;
    [SerializeField] private bool isOn = true;

    private Coroutine spawnCoroutine;

    void Start()
    {
        if(isOn)
        {
            StartSpawnCoroutine();
        }
    }

    IEnumerator SpawnRemys()
    {
        while (customerSpawned < spawnLimit)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(Remy, transform.position, transform.rotation);
            customerSpawned++;
        }
        
    }

    public void setIsOn(bool state)
    {
        isOn = state;
        if(isOn)
        {
            StartSpawnCoroutine();
        }
        else 
        {
            StopSpawnCoroutine();
        }
    }

    private void StartSpawnCoroutine()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnRemys());
        }
    }


    private void StopSpawnCoroutine()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine =  null;
        }
    }
}

