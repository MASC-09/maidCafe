using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField] private float cookingTime = 3.0f;
    public Transform foodSpawnPoint;
    public GameObject Meal;


    public void StartCooking()
    {
        Debug.Log("Cooking meal!");
        StartCoroutine(CookMeal());
    }

    private IEnumerator CookMeal()
    {
        yield return new WaitForSeconds(cookingTime);
        Instantiate(Meal, foodSpawnPoint.position, foodSpawnPoint.rotation);
    }

}
