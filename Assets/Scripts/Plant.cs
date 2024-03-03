using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlantType
{
    Tomato,
    CherryBlossom,
    Daisy
}

public class Plant : MonoBehaviour
{

    public GameObject plant;
    public GameObject seed;
    public float timeToGrowMins;
    public int timesToBeWatered;
    public PlantType plantType;

    private Animator plantAnim;
    private Animator seedAnim;
    private float timeWateredGrownSec;
    private int countWatered;
    private float timeStep;

    void Awake(){
        plantAnim = plant.GetComponent<Animator>();
        seedAnim = seed.GetComponent<Animator>();
        timeWateredGrownSec = Time.fixedDeltaTime /(timeToGrowMins / timesToBeWatered * 60f);
        countWatered = 0;
    }

    public void WaterPlant(){
        if(countWatered == 0){
            Debug.Log("Check");
            plantAnim.SetBool("isGrowing", true);
            seedAnim.SetBool("isGrowing", true);
        }
        countWatered += 1;
        StartCoroutine(PlayGrowingSmoothly(timeWateredGrownSec));
        Debug.Log(countWatered);
        Debug.Log(countWatered);
        Debug.Log(timesToBeWatered);
    }

    IEnumerator PlayGrowingSmoothly(float timeToGrowSecs)
    {
        plantAnim.speed = 1;
        plantAnim.Play("Growing", 0, 0f);
        //TESTTTTTTTTTT
        Debug.Log(timesToBeWatered);
        float startAnimTime = 1f/timesToBeWatered * (countWatered - 1);
        Debug.Log(1/timesToBeWatered);
        float endAnimTime = 1f/timesToBeWatered * countWatered;
        Debug.Log(startAnimTime);
        Debug.Log(endAnimTime);
        for (float t = startAnimTime; t <= endAnimTime; t += timeToGrowSecs)
        {
            plantAnim.Play("Growing", 0, t);
            yield return new WaitForFixedUpdate();
        }
        
        if(countWatered == timesToBeWatered){
            plantAnim.SetBool("isGrowing", false);
            seedAnim.SetBool("isGrowing", false);
        }
        plantAnim.speed = 0;
    }
}
