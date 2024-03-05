using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ActionType{
    None,
    PlantSeed,
    SpellThrow,
}

public enum SpellType{
    Water,
    Fire,
    Electrical,
    WindCut
}

public class Controller : MonoBehaviour
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
    public GameObject electricalEffect;
    public GameObject correctGrownEffect;
    public GameObject wrongGrownEffect;
    public Material frozenMat;
    public Material evilMat;
    public Material growingFrozenEffectMat;
    public Material growingEvilEffectMat;
    public TextMeshProUGUI textAid;
    public TextMeshProUGUI textAction;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textMoney;
    public float delayTextInSec;
    public float delayWateringInSec;
    public ProgressBar progressBar;
    private int score;
    private int money;

    void Awake(){
        textAction.text = "Action: None";
        score = 0;
        textScore.text = "0%";
        money = 2;
        textMoney.text = money.ToString();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.U)){
            money += 50;
            textMoney.text = money.ToString();
        }
    }

    public void DoAction(Tile tile){
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
                            case SpellType.Electrical:
                                textAid.text = "Stop that!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.WindCut:
                                textAid.text = "Nothing to cut here";
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
                            case SpellType.Electrical:
                                textAid.text = "No don't do that";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.WindCut:
                                textAid.text = "Let it grow first";
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
                            case SpellType.WindCut:
                                textAid.text = "Is growing be patient";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Electrical:
                                textAid.text = "Let it grow";
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
                            case SpellType.WindCut:
                                textAid.text = "Can't cut ice...";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Electrical:
                                textAid.text = "Nope not this one";
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
                            case SpellType.WindCut:
                                GameObject plantObject = tile.GetCurrentPlant();
                                tile.SetTileState(TileState.Normal);
                                PlantType plantType = plantObject.GetComponent<Plant>().GetPlantType();
                                if(plantType == tile.GetCorrectPlant()){
                                    DecrementScore();
                                }
                                switch(plantType){
                                    case PlantType.Tomato:
                                        money += (int)plantType + 5;
                                        break;
                                    case PlantType.CherryBlossom:
                                        money += (int)plantType + 15;
                                        break;
                                    case PlantType.Daisy:
                                        money += (int)plantType + 2;
                                        break;
                                }
                                textMoney.text = money.ToString();
                                Destroy(plantObject);
                                
                                break;
                            case SpellType.Electrical:
                                textAid.text = "No evil here!";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                        }
                        break;
                    case TileState.Evil:
                        switch (currSpellType){
                            case SpellType.Water:
                                textAid.text = "No don't let it grow";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Fire:
                                textAid.text = "Need stronger spell";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.WindCut:
                                textAid.text = "Don't take that";
                                StartCoroutine(EmptyTextAfterDelay());
                                break;
                            case SpellType.Electrical:
                                Vector3 tilePos = tile.transform.position;
                                Instantiate(electricalEffect, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                                StartCoroutine(UnEvil(tile.GetCurrentPlant()));
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
                    if(money >= (int)currPlantType){
                        Vector3 tilePos = tile.transform.position;
                        GameObject newPlantPrefab = null;
                        switch(currPlantType){
                            case PlantType.Tomato:
                                newPlantPrefab = tomatoPlant;
                                break;
                            case PlantType.CherryBlossom:
                                newPlantPrefab = cherryBlossom;
                                break;
                            case PlantType.Daisy:
                                newPlantPrefab = daisyFlower;
                                break;
                        }
                        newPlant = Instantiate(newPlantPrefab, new Vector3(tilePos.x, tilePos.y + 0.5f, tilePos.z), Quaternion.identity);
                        newPlant.transform.SetParent(tile.transform);
                        tile.SetCurrentPlant(newPlant);
                        tile.SetTileState(TileState.Seeded);  
                        money -= (int)currPlantType;
                        textMoney.text = money.ToString();
                    }else{
                        textAid.text = "No money sorry";
                        StartCoroutine(EmptyTextAfterDelay());
                    }
                    
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
            default:
                Debug.LogError("Error in UnFreezeAndDeleteFire");
                break;
        }
        UnFeatureWithComparison(prefabToCompare.transform, plantToUnfreeze.transform);
        yield return new WaitForSeconds(0.5f);
        Destroy(fire);
    }

    IEnumerator UnEvil(GameObject plantToUnevil)
    {
        Plant plant = plantToUnevil.GetComponent<Plant>();
        plant.UnEvilPlant();
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
            default:
                Debug.LogError("Error in UnFreezeAndDeleteFire");
                break;
        }
        UnFeatureWithComparison(prefabToCompare.transform, plantToUnevil.transform);
        yield return null;
    }

    void UnFeatureWithComparison(Transform prefabTransform, Transform plantTransform){
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
                UnFeatureWithComparison(childPrefab, child);
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
            case SpellType.Electrical:
                textAction.text = "Action: Lightning";
                break;
            case SpellType.WindCut:
                textAction.text = "Action: Cut";
                break;
        }
    }

    public GameObject StartGrowingEffect(Vector3 position){
        GameObject growingObject = Instantiate(growingEffect, new Vector3(position.x, position.y, position.z), Quaternion.identity);
        return growingObject;
    }

    public void FeaturePlant(Transform plantTransform, TileState tileState){
        Material changeMat = null;
        switch (tileState){
            case TileState.Frozen:
                changeMat =  frozenMat;
                break;
            case TileState.Evil:
                changeMat =  evilMat;
                break;
            default:
                Debug.LogError("Error In FeaturePlant");
                break;
        }
        for (int i = 0; i < plantTransform.childCount; i++)
        {
            Transform child = plantTransform.GetChild(i);
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null){
                meshRenderer.material = changeMat;
            }
            if (child.childCount > 0){
                FeaturePlant(child, tileState);
            }
        }
    }

    public Material GetGrowingFeatureMat(TileState tileState){
        switch (tileState){
            case TileState.Frozen:
                return growingFrozenEffectMat;
            case TileState.Evil:
                return growingEvilEffectMat;
            default:
                Debug.LogError("Error In GetFeatureMat");
                return null;
        }
        
    }

    public void CorrectFlowerGrownEffect(Vector3 position){
        GameObject grownInstance = Instantiate(correctGrownEffect, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
        Destroy(grownInstance, 3f);
    }

    public void WrongFlowerGrownEffect(Vector3 position){
        GameObject grownInstance = Instantiate(wrongGrownEffect, new Vector3(position.x, position.y + 0.5f, position.z), Quaternion.identity);
        Destroy(grownInstance, 3f);
    }

    public void IncrementScore(){
        score += 1;
        textScore.text = ((int)((float)score/64*100)).ToString() + "%";
        money += 2;
        textMoney.text = money.ToString();
        progressBar.SetProgress((int)((float)score/64*100));
    }

    public void DecrementScore(){
        score -= 1;
        if(score < 0){
            score = 0;
        }
        textScore.text = ((int)((float)score/64*100)).ToString() + "%";
        progressBar.SetProgress((int)((float)score/64*100));
    }

    public void ReduceMoney(){
        money -= 2;
        if(money < 0){
            money = 0;
        }
        textMoney.text = money.ToString();
    }
}
