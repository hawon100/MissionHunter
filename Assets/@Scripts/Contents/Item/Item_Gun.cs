using UnityEngine;

public class Item_Gun : Item_Base
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    private void Update()
    {

    }
}