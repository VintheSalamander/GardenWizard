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
    public TextMeshProUGUI textAid;
    public float delayTextInSec;


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
                                tile.GetCurrentPlant().GetComponent<Plant>().WaterPlant();
                                tile.SetTileState(TileState.Watered);
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

    public static void ChangeCurrentAction(ActionType newAction){
        currentAction = newAction;
    }

    public static void ChangeCurrentSeed(PlantType newSeed){
        currPlantType = newSeed;
    }

    public static void ChangeCurrentSpell(SpellType newSpell){
        currSpellType = newSpell;
    }
}
