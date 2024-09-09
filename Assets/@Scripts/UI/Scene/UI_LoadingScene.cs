using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingScene : UI_Scene
{
    enum Images
    {
        Image,
    }

    public override bool Init()
    { 
        if (base.Init() == false)
            return false;

        BindImages(typeof(Images));
        GetImage((int)Images.Image).fillAmount = 0f;

        return true;
    }
}
