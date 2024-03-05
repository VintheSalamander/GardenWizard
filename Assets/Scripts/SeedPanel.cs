using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPanel : MonoBehaviour
{
    public Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSeedToTomato(){
        controller.ChangeCurrentAction(ActionType.PlantSeed);
        controller.ChangeCurrentSeed(PlantType.Tomato);
    }

    public void ChangeSeedToCherryBlossom(){
        controller.ChangeCurrentAction(ActionType.PlantSeed);
        controller.ChangeCurrentSeed(PlantType.CherryBlossom);
    }

    public void ChangeSeedToDaisy(){
        controller.ChangeCurrentAction(ActionType.PlantSeed);
        controller.ChangeCurrentSeed(PlantType.Daisy);
    }
}
