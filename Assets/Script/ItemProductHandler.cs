using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemProductHandler : MonoBehaviour
{
    public InputField inpOldPrice;
    public InputField inpNewPrice;
    public int oldPrice;
    public int newPrice;
    public string itemName;
    public string path;

    public Text txtName;
    public Sprite sprite;


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

    public void SetData(string name, string path, Sprite sprite)
    {
        txtName.text = name;
        itemName = name;
        this.path = path;
        this.sprite = sprite;
    }

    public void SetCallback(OnClickItemCallback callback)
    {
        this.callback = callback;
    }

    public void OnClicked()
    {
        this.callback?.Invoke(this);
    }
}
