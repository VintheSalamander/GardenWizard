using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSeedToTomato(){
        ActionController.ChangeCurrentAction(ActionType.PlantSeed);
        ActionController.ChangeCurrentSeed(PlantType.Tomato);
    }

    public void ChangeSeedToCherryBlossom(){
        ActionController.ChangeCurrentAction(ActionType.PlantSeed);
        ActionController.ChangeCurrentSeed(PlantType.CherryBlossom);
    }

    public void ChangeSeedToDaisy(){
        ActionController.ChangeCurrentAction(ActionType.PlantSeed);
        ActionController.ChangeCurrentSeed(PlantType.Daisy);
    }
}
