using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPanel : MonoBehaviour
{
    public ActionController actionController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSeedToTomato(){
        actionController.ChangeCurrentAction(ActionType.PlantSeed);
        actionController.ChangeCurrentSeed(PlantType.Tomato);
    }

    public void ChangeSeedToCherryBlossom(){
        actionController.ChangeCurrentAction(ActionType.PlantSeed);
        actionController.ChangeCurrentSeed(PlantType.CherryBlossom);
    }

    public void ChangeSeedToDaisy(){
        actionController.ChangeCurrentAction(ActionType.PlantSeed);
        actionController.ChangeCurrentSeed(PlantType.Daisy);
    }
}
