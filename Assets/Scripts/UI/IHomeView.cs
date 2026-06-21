using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHomeView 
{
    void SetHomeActive(bool isactive);
    Transform GridParent { get; }
    ScrollRect ScrollRect { get; }

    GridLayoutSettings LayoutSettings { get; }

    void SetFilterScreenActive(bool isactive);
    event Action<bool> OnFilterButtonClicked;
    IFilterView FilterView { get; }


}
