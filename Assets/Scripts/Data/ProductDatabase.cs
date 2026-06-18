using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ProductDatabase
{
    public List<ProductData> products;
}

[Serializable]
public class ProductData
{
    public string id;
    public string name;
    public string category;
    public string subCategory;
    public string description;
    public string thumbnailURL;
    public string modelCategory;
}