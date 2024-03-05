using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlantType
{
    Tomato = 10,
    CherryBlossom = 30,
    Daisy = 2
}

public class Plant : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject plant;
    public GameObject seed;
    public float timeToGrowMins;
    public int timesToBeWatered;
    public PlantType plantType;
    private bool isFreezed;
    private bool isEvil;

    private Animator plantAnim;
    private Animator seedAnim;
    private float timeWateredGrown;
    private int countWatered;
    private Controller controller;
    private GameObject growingObject;

    void Awake(){
        plantAnim = plant.GetComponent<Animator>();
        seedAnim = seed.GetComponent<Animator>();
        timeWateredGrown = Time.fixedDeltaTime /(timeToGrowMins * 60f / timesToBeWatered );
        countWatered = 0;
        isFreezed = false;
        isEvil = false;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Y)){
            timeWateredGrown = timeWateredGrown*6;
        }
    }

    public void WaterPlant(Controller con, float delay){
        controller = con;
        StartCoroutine(WateringWithDelay(delay));
    }

    IEnumerator WateringWithDelay(float delay){
        yield return new WaitForSeconds(delay);
        if(countWatered == 0){
            plantAnim.SetBool("isGrowing", true);
            seedAnim.SetBool("isShrinking", true);
        }
        growingObject = controller.StartGrowingEffect(transform.position);
        countWatered += 1;
        StartCoroutine(PlayGrowingSmoothly(timeWateredGrown));
    }

    IEnumerator PlayGrowingSmoothly(float timeToGrow)
    {
        plantAnim.speed = 0;
        seedAnim.speed = 0;
        plantAnim.Play("Growing", 0, 0f);
        seedAnim.Play("Shrinking", 0, 0f);
        float startAnimTime = 1f/timesToBeWatered * (countWatered - 1);
        float endAnimTime = 1f/timesToBeWatered * countWatered;

        float randSec = Random.value;
        int minute = System.DateTime.Now.Minute;
        if(minute % 2 == 0){
            randSec = 1-randSec;
        }
        if(randSec < (float)System.DateTime.Now.Millisecond/1000){
            isFreezed = true;
        }else if(Random.Range(0, 5) == minute % 5){
            isEvil = true;
        }
        for (float t = startAnimTime; t <= endAnimTime; t += timeToGrow)
        {
            if(isFreezed && t > startAnimTime + (endAnimTime-startAnimTime)/2){
                controller.FeaturePlant(transform, TileState.Frozen);
                Tile tile = transform.parent.GetComponent<Tile>();
                tile.SetTileState(TileState.Frozen);
                plantAnim.Play("Growing", 0, t);
                seedAnim.Play("Shrinking", 0, t);

                Renderer growEffectRend = growingObject.GetComponentInChildren<Renderer>();
                Material normalGrowingMat = growEffectRend.material;
                growEffectRend.material = controller.GetGrowingFeatureMat(TileState.Frozen);

                yield return new WaitUntil(() => !isFreezed);

                growEffectRend.material = normalGrowingMat;

                tile.SetTileState(TileState.Watered);
            }else if(isEvil && t>endAnimTime/2){
                controller.FeaturePlant(transform, TileState.Evil);
                Tile tile = transform.parent.GetComponent<Tile>();
                tile.SetTileState(TileState.Evil);
                plantAnim.Play("Growing", 0, t);
                seedAnim.Play("Shrinking", 0, t);

                Renderer growEffectRend = growingObject.GetComponentInChildren<Renderer>();
                Material normalGrowingMat = growEffectRend.material;
                growEffectRend.material = controller.GetGrowingFeatureMat(TileState.Evil);

                yield return new WaitUntil(() => !isEvil);
                controller.ReduceMoney();
                growEffectRend.material = normalGrowingMat;

                tile.SetTileState(TileState.Watered);
            }else{
                plantAnim.Play("Growing", 0, t);
                seedAnim.Play("Shrinking", 0, t);
            }
            yield return new WaitForFixedUpdate();
        }
        
        if(countWatered == timesToBeWatered){
            plantAnim.SetBool("isGrowing", false);
            seedAnim.SetBool("isShrinking", false);
            transform.parent.GetComponent<Tile>().SetTileState(TileState.Grown);
        }else{
            transform.parent.GetComponent<Tile>().SetTileState(TileState.Seeded);
        }
        Destroy(growingObject);
    }

    public void UnFreezePlant(){
        isFreezed = false;
    }

    public void UnEvilPlant(){
        isEvil = false;
    }

    public PlantType GetPlantType(){
        return plantType;
    }
}
