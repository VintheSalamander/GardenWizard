using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ActionType{
    PlantSeed,
    None,
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
                        GameObject plant = tile.GetCurrentPlant();
                        break;
                    case TileState.Watered:
                        break;
                    case TileState.Grown:
                        break;
                    default:
                        break;
                }
                break;
            case ActionType.PlantSeed:
                GameObject newPlant = null;
                if(tileState == TileState.Normal){
                    Vector3 tilePos = tile.transform.position;
                    switch(currPlantType){
                        case PlantType.Tomato:
                            newPlant = Instantiate(tomatoPlant, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                        case PlantType.CherryBlossom:
                            newPlant = Instantiate(tomatoPlant, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                        case PlantType.Daisy:
                            newPlant = Instantiate(tomatoPlant, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                            break;
                    }
                    newPlant.transform.SetParent(tile.transform);
                    tile.SetCurrentPlant(newPlant);
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
}