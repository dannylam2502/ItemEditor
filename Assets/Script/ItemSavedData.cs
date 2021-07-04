using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSavedData
{
    public ItemSaveDetailData[] data;
}

[Serializable]
public class ItemSaveDetailData
{
    public string imagePath;
    public string itemName;
    public int oldPrice;
    public int newPrice;
    public string description;
    public string date;
    public string direction;
    public byte[] image;

    public void Set(ItemProductHandler info)
    {
        imagePath = info.path;
        itemName = info.itemName;
        oldPrice = info.oldPrice;
        newPrice = info.newPrice;
        description = info.inpDes.text;
        date = info.inpDate.text;
        direction = info.inpDirection.text;
        image = ExtensionMethod.DeCompress(info.sprite.texture).EncodeToPNG();
    }
}
