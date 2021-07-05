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
    // UI position
    public SVector2 posImage;
    public SVector2 posItemName;
    public SVector2 posPriceTag;
    public SVector2 posDescription;
    public SVector2 posDate;
    public SVector2 posDirection;

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
        posImage = new SVector2(info.posImage);
        posItemName = new SVector2(info.posItemName);
        posPriceTag = new SVector2(info.posPriceTag);
        posDescription = new SVector2(info.posDescription);
        posDate = new SVector2(info.posDate);
        posDirection = new SVector2(info.posDirection);
    }
}

/// <summary> Serializable version of UnityEngine.Vector3. </summary>
[Serializable]
public struct SVector2
{
    public float x;
    public float y;

    public SVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public SVector2(Vector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }

    public override string ToString()
        => $"[x, y, z]";

    public static explicit operator SVector2(Vector2 s) => new SVector2(s);
    public static implicit operator Vector2(SVector2 s)
        => new Vector2(s.x, s.y);

    public static implicit operator SVector2(Vector3 v)
        => new SVector2(v.x, v.y);


    public static SVector2 operator +(SVector2 a, SVector2 b)
        => new SVector2(a.x + b.x, a.y + b.y);

    public static SVector2 operator -(SVector2 a, SVector2 b)
        => new SVector2(a.x - b.x, a.y - b.y);

    public static SVector2 operator -(SVector2 a)
        => new SVector2(-a.x, -a.y);

    public static SVector2 operator *(SVector2 a, float m)
        => new SVector2(a.x * m, a.y * m);

    public static SVector2 operator *(float m, SVector2 a)
        => new SVector2(a.x * m, a.y * m);

    public static SVector2 operator /(SVector2 a, float d)
        => new SVector2(a.x / d, a.y / d);
}
