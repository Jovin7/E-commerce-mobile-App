using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHomeView 
{
    Transform GridParent { get; }

    void SetHomeActive(bool isactive);
}
