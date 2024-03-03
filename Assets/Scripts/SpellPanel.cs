using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSpellToWater(){
        ActionController.ChangeCurrentAction(ActionType.SpellThrow);
        ActionController.ChangeCurrentSpell(SpellType.Water);
    }

    public void ChangeSpellToFire(){
        ActionController.ChangeCurrentAction(ActionType.SpellThrow);
        ActionController.ChangeCurrentSpell(SpellType.Fire);
    }
}
