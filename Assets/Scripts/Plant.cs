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
    private float timeWateredGrown;
    private int countWatered;
    private ActionController actionController;
    private GameObject growingObject;

    void Awake(){
        plantAnim = plant.GetComponent<Animator>();
        seedAnim = seed.GetComponent<Animator>();
        timeWateredGrown = Time.fixedDeltaTime /(timeToGrowMins * 60f / timesToBeWatered );
        countWatered = 0;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Y)){
            timeWateredGrown = timeWateredGrown*6;
        }
    }

    public void WaterPlant(ActionController actCon, float delay){
        actionController = actCon;
        StartCoroutine(WateringWithDelay(delay));
    }

    IEnumerator WateringWithDelay(float delay){
        yield return new WaitForSeconds(delay);
        if(countWatered == 0){
            plantAnim.SetBool("isGrowing", true);
            seedAnim.SetBool("isShrinking", true);
        }
        growingObject = actionController.StartGrowingEffect(transform.position);
        countWatered += 1;
        StartCoroutine(PlayGrowingSmoothly(timeWateredGrown));
    }

    IEnumerator PlayGrowingSmoothly(float timeToGrow)
    {
        plantAnim.speed = 1;
        seedAnim.speed = 1;
        plantAnim.Play("Growing", 0, 0f);
        seedAnim.Play("Shrinking", 0, 0f);
        float startAnimTime = 1f/timesToBeWatered * (countWatered - 1);
        float endAnimTime = 1f/timesToBeWatered * countWatered;
        for (float t = startAnimTime; t <= endAnimTime; t += timeToGrow)
        {
            plantAnim.Play("Growing", 0, t);
            seedAnim.Play("Shrinking", 0, t);
            yield return new WaitForFixedUpdate();
        }
        
        if(countWatered == timesToBeWatered){
            plantAnim.SetBool("isGrowing", false);
            seedAnim.SetBool("isShrinking", false);
            transform.parent.GetComponent<Tile>().SetTileState(TileState.Grown);
        }else{
            transform.parent.GetComponent<Tile>().SetTileState(TileState.Seeded);
        }
        plantAnim.speed = 0;
        seedAnim.speed = 0;
        Debug.Log("Check");
        Destroy(growingObject);
    }
}
