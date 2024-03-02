using System.Collections;
using System.Collections.Generic;
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

    void Awake(){
        plantAnim = plant.GetComponent<Animator>();
        seedAnim = seed.GetComponent<Animator>();
    }

    public void WaterPlant(){
        plantAnim.SetBool("isWatered", true);
        seedAnim.SetBool("isWatered", true);
        StartCoroutine(PlayGrowingSmoothly(timeToGrowMins * 60f));
    }

    IEnumerator PlayGrowingSmoothly(float timeToGrowSecs)
    {
        float timeStep = Time.fixedDeltaTime / timeToGrowSecs;

        plantAnim.Play("Growing", 0, 0f);

        for (float t = 0f; t <= 1f; t += timeStep)
        {
            plantAnim.Play("Growing", 0, t);
            yield return new WaitForFixedUpdate();
        }

        plantAnim.Play("Growing", 0, 1f);

        plantAnim.SetBool("isWatered", false);
        seedAnim.SetBool("isWatered", false);
    }
}
