using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ItemProductHandler : MonoBehaviour
{
    public Image background;
    public InputField inpOldPrice;
    public InputField inpNewPrice;
    public InputField inpName;
    public InputField inpDes;
    public InputField inpDate;
    public InputField inpDirection;
    public int oldPrice;
    public int newPrice;
    public string itemName;
    public string path;

    public Text txtName;
    public Sprite sprite;

    // UI position
    public Vector2 posImage;
    public Vector2 posItemName;
    public Vector2 posPriceTag;
    public Vector2 posDescription;
    public Vector2 posDate;
    public Vector2 posDirection;

    public delegate void OnClickItemCallback(ItemProductHandler handler);

    public OnClickItemCallback callback;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (int.TryParse(inpOldPrice.text, out oldPrice))
        {
            
        }
        if (int.TryParse(inpNewPrice.text, out newPrice))
        {

        }
    }

    public void LoadData(ItemSaveDetailData info)
    {
        txtName.text = info.itemName;
        itemName = info.itemName;
        this.path = info.imagePath;
        this.sprite = LoadPNG(info.image);
        inpName.text = info.itemName;
        inpDes.text = info.description;
        inpDate.text = info.date;
        inpDirection.text = info.direction;
        oldPrice = info.oldPrice;
        newPrice = info.newPrice;
        inpOldPrice.text = oldPrice.ToString();
        inpNewPrice.text = newPrice.ToString();

        posDate = info.posDate;
        posDescription = info.posDescription;
        posDirection = info.posDirection;
        posImage = info.posImage;
        posItemName = info.posItemName;
        posPriceTag = info.posPriceTag;
    }

    public void SetData(string name, string path, Sprite sprite)
    {
        txtName.text = name;
        itemName = name;
        this.path = path;
        this.sprite = sprite;
        inpName.text = name;
    }

    public void SetCallback(OnClickItemCallback callback)
    {
        this.callback = callback;
    }

    public void OnClicked()
    {
        this.callback?.Invoke(this);
    }

    public void OnSelected()
    {
        background.color = Color.yellow;
    }

    public void OnDeselected()
    {
        background.color = Color.white;
    }

    public void OnClickBtnReset()
    {
        var itemEditor = FindObjectOfType<ItemEditor>();
        if (itemEditor)
        {
            itemEditor.ResetItemLayout(this);
        }
    }

    public static Sprite LoadSprite(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;
        Sprite result = null;
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            // to sprite
            Rect rec = new Rect(0, 0, tex.width, tex.height);
            result = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
        }
        return result;
    }

    public static Sprite LoadPNG(byte[] imageData)
    {
        Texture2D tex = null;
        Sprite result = null;
        tex = new Texture2D(2, 2);
        tex.LoadImage(imageData); //..this will auto-resize the texture dimensions.
        // to sprite
        Rect rec = new Rect(0, 0, tex.width, tex.height);
        result = Sprite.Create(tex, rec, new Vector2(0.5f, 0.5f), 100);
        return result;
    }
}
