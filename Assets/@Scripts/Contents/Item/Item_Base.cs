using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Base : InitBase
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
}
