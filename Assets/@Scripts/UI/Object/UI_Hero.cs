using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hero : UI_Object
{
    enum Texts
    {
        ItemTake,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));
        ItemInteractActive(false);

        return true;
    }

    public void ItemInteractActive(bool isEnable)
    {
        GetText((int)Texts.ItemTake).gameObject.SetActive(isEnable);
    }
}
