using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ActionType{
    None,
    PlantSeed,
    SpellThrow
    
}

public enum SpellType{
    Water,
    Fire
}

public class ActionController : MonoBehaviour
{
    public static ActionType currentAction;
    public static PlantType currPlantType;
    public static SpellType currSpellType;
    public GameObject tomatoPlant;
    public GameObject cherryBlossom;
    public GameObject daisyFlower;
    public GameObject waterEffect;
    public GameObject growingEffect;
    public GameObject fireEffect;
    public Material frozenMat;
    public Material growingFrozenEffectMat;
    public TextMeshProUGUI textAid;
    public TextMeshProUGUI textAction;
    public float delayTextInSec;
    public float delayWateringInSec;

    void Awake(){
        textAction.text = "Action: None";
    }
    public void DoAction(GameObject tileObject){
        Tile tile = tileObject.GetComponent<Tile>();
        TileState tileState = tile.GetCurrentState();
        switch (currentAction){
            case ActionType.None:
                textAid.text = "Choose your action first";
                StartCoroutine(EmptyTextAfterDelay());
                break;
            case ActionType.SpellThrow:
                switch (tileState){
                    case TileState.Normal:
                        switch (currSpellType){
                            case SpellType.Water:
                                textAid.text = "Plant some seed";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Fire:
                                textAid.text = "Careful with that!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                        }
                        break;
                    case TileState.Seeded:
                        switch (currSpellType){
                            case SpellType.Water:
                                Vector3 tilePos = tile.transform.position;
                                tile.GetCurrentPlant().GetComponent<Plant>().WaterPlant(this, delayWateringInSec);
                                tile.SetTileState(TileState.Watered);
                                Instantiate(waterEffect, new Vector3(tilePos.x, tilePos.y + 0.5001f, tilePos.z), Quaternion.identity);
                                break;
                            case SpellType.Fire:
                                textAid.text = "Dont burn here!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                        }
                        break;
                    case TileState.Watered:
                        switch (currSpellType){
                            case SpellType.Water:
                                textAid.text = "No more water!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Fire:
                                textAid.text = "Stop unwatering!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                        }
                        break;
                    case TileState.Frozen:
                        switch (currSpellType){
                            case SpellType.Water:
                                textAid.text = "More ice?";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Fire:
                                Vector3 tilePos = tile.transform.position;
                                GameObject fireInstance = Instantiate(fireEffect, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                                StartCoroutine(UnFreezeAndDeleteFire(fireInstance, tile.GetCurrentPlant()));
                                break;
                        }
                        break;
                    case TileState.Grown:
                        switch (currSpellType){
                            case SpellType.Water:
                                textAid.text = "Already grown!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Fire:
                                textAid.text = "That is healthy!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case ActionType.PlantSeed:
                GameObject newPlant = tile.GetCurrentPlant();
                if(newPlant == null && tileState == TileState.Normal){
                    Vector3 tilePos = tile.transform.position;
                    switch(currPlantType){
                        case PlantType.Tomato:
                            newPlant = Instantiate(tomatoPlant, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                        case PlantType.CherryBlossom:
                            newPlant = Instantiate(cherryBlossom, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                        case PlantType.Daisy:
                            newPlant = Instantiate(daisyFlower, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                    }
                    newPlant.transform.SetParent(tile.transform);
                    tile.SetCurrentPlant(newPlant);
                    tile.SetTileState(TileState.Seeded);
                }
                break;
            default:
                Debug.LogError("Action Error");
                break;
        }
    }

    IEnumerator EmptyTextAfterDelay()
    {
        yield return new WaitForSeconds(delayTextInSec);
        textAid.text = "";
    }

    public void ChangeCurrentAction(ActionType newAction){
        currentAction = newAction;
    }

    IEnumerator UnFreezeAndDeleteFire(GameObject fire, GameObject plantToUnfreeze)
    {
        yield return new WaitForSeconds(0.5f);
        Plant plant = plantToUnfreeze.GetComponent<Plant>();
        plant.UnFreezePlant();
        PlantType plantType = plant.GetPlantType();
        GameObject prefabToCompare = null;
        switch(plantType){
            case PlantType.Tomato:
                prefabToCompare = tomatoPlant;
                break;
            case PlantType.CherryBlossom:
                prefabToCompare = cherryBlossom;
                break;
            case PlantType.Daisy:
                prefabToCompare = daisyFlower;
                break;
        }
        UnFreezeWithComparison(prefabToCompare.transform, plantToUnfreeze.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(fire);
    }

    void UnFreezeWithComparison(Transform prefabTransform, Transform plantTransform){
        for (int i = 0; i < plantTransform.childCount; i++)
        {
            Transform child = plantTransform.GetChild(i);
            Transform childPrefab = prefabTransform.GetChild(i);
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            MeshRenderer meshRendererPrefab = childPrefab.GetComponent<MeshRenderer>();
            if (meshRenderer != null){
                meshRenderer.material = meshRendererPrefab.sharedMaterial;
            }
            if (child.childCount > 0){
                UnFreezeWithComparison(childPrefab, child);
            }
        }
    }

    public void ChangeCurrentSeed(PlantType newSeed){
        currPlantType = newSeed;
        switch(currPlantType){
            case PlantType.Tomato:
                textAction.text = "Action: Tomato";
                break;
            case PlantType.CherryBlossom:
                textAction.text = "Action: Cherry Blossom";
                break;
            case PlantType.Daisy:
                textAction.text = "Action: Daisy";
                break;
        }
    }

    public void ChangeCurrentSpell(SpellType newSpell){
        currSpellType = newSpell;
        switch(currSpellType){
            case SpellType.Water:
                textAction.text = "Action: Water";
                break;
            case SpellType.Fire:
                textAction.text = "Action: Fire";
                break;
        }
    }

    public GameObject StartGrowingEffect(Vector3 position){
        GameObject growingObject = Instantiate(growingEffect, new Vector3(position.x, position.y, position.z), Quaternion.identity);
        return growingObject;
    }

    public void FreezePlant(Transform plantTransform){
        for (int i = 0; i < plantTransform.childCount; i++)
        {
            Transform child = plantTransform.GetChild(i);
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null){
                meshRenderer.material = frozenMat;
            }
            if (child.childCount > 0){
                FreezePlant(child);
            }
        }
    }

    public Material GetGrowinFrozenMat(){
        return growingFrozenEffectMat;
    }
}
