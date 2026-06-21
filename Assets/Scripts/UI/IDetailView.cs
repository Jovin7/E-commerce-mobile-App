using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetailView 
{
    event Action OnBackButtonRequested;

    event Action OnView3DRequested;
    void SetDetailActive(bool isactive);

    void SetProductInfo(ProductData data);

    void SetProductSprite(Sprite sprite);

}
