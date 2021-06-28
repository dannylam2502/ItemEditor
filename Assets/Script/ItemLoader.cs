using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemLoader : MonoBehaviour
{
    public ItemProductHandler prefab;
    public RectTransform contentLayout;
    public ItemEditor itemEditor;

    // Start is called before the first frame update
    void Start()
    {
        var path = "Images/Items";
        var images = Resources.LoadAll<Sprite>(path);
        foreach (var item in images)
        {
            var instance = Instantiate(prefab, contentLayout, false);
            var component = instance.GetComponent<ItemProductHandler>();
            if (component)
            {
                component.SetData(item.name, path + item.name, item);
                component.SetCallback(OnClickItem);
            }
        }

        foreach (Transform transform in contentLayout.transform)
        {
            var component = transform.gameObject.GetComponent<ItemProductHandler>();
            if (component)
            {
                OnClickItem(component);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickItem(ItemProductHandler itemProductHandler)
    {
        itemEditor.Load(itemProductHandler);
    }
}
