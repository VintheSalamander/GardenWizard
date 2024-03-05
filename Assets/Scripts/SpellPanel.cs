using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPanel : MonoBehaviour
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

    public void ChangeSpellToWater(){
        controller.ChangeCurrentAction(ActionType.SpellThrow);
        controller.ChangeCurrentSpell(SpellType.Water);
    }

    public void ChangeSpellToFire(){
        controller.ChangeCurrentAction(ActionType.SpellThrow);
        controller.ChangeCurrentSpell(SpellType.Fire);
    }

    public void ChangeSpellToWindCut(){
        controller.ChangeCurrentAction(ActionType.SpellThrow);
        controller.ChangeCurrentSpell(SpellType.WindCut);
    }

    public void ChangeSpellToElectrical(){
        controller.ChangeCurrentAction(ActionType.SpellThrow);
        controller.ChangeCurrentSpell(SpellType.Electrical);
    }
}
