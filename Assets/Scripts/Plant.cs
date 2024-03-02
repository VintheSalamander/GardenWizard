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
            plantAnim.SetBool("isGrowing", true);
            seedAnim.SetBool("isGrowing", true);
        }
        countWatered += 1;
        StartCoroutine(PlayGrowingSmoothly(timeWateredGrownSec));
        if(countWatered == timesToBeWatered){
            plantAnim.Play("Growing", 0, 1f);
            plantAnim.SetBool("isGrowing", false);
            seedAnim.SetBool("isGrowing", false);
        }
    }

    IEnumerator PlayGrowingSmoothly(float timeToGrowSecs)
    {
        plantAnim.speed = 1;
        plantAnim.Play("Growing", 0, 0f);
        //TESTTTTTTTTTT
        float startAnimTime = timeToGrowSecs * countWatered;
        for (float t = startAnimTime; t <= 1f; t += startAnimTime + timeToGrowSecs / timesToBeWatered)
        {
            plantAnim.Play("Growing", 0, t);
            yield return new WaitForFixedUpdate();
        }
        plantAnim.speed = 0;
    }
}
