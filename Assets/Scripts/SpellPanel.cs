using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPanel : MonoBehaviour
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

    public void ChangeSpellToWater(){
        actionController.ChangeCurrentAction(ActionType.SpellThrow);
        actionController.ChangeCurrentSpell(SpellType.Water);
    }

    public void ChangeSpellToFire(){
        actionController.ChangeCurrentAction(ActionType.SpellThrow);
        actionController.ChangeCurrentSpell(SpellType.Fire);
    }
}
