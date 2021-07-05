using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemLoader : MonoBehaviour
{
    public ItemProductHandler prefab;
    public RectTransform contentLayout;
    public ItemEditor itemEditor;
    public ScrollRect scrollRectController;

    public const string SAVE_FILE_NAME = "VinacineItemData.dat";

    // Start is called before the first frame update
    void Start()
    {
        var path = "Images/Items";
        var images = Resources.LoadAll<Sprite>(path);
        foreach (var item in images)
        {
            var instance = Instantiate(prefab.gameObject, contentLayout, false);
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
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand)
            || Input.GetKey(KeyCode.RightCommand))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnClickBtnSaveProject();
            }
        }
    }

    public void OnClickItem(ItemProductHandler itemProductHandler)
    {
        itemEditor.Load(itemProductHandler);
    }

    public void OnClickBtnSaveProject()
    {
        var itemLoader = FindObjectOfType<ItemLoader>();
        if (itemLoader)
        {
            var extensionList = new[] {
                new ExtensionFilter("Vinacine Data File", "dat"),
            };
            var newPath = StandaloneFileBrowser.SaveFilePanel("Choose Folder To Save", "", SAVE_FILE_NAME, extensionList);
            if (newPath.Length > 0)
            {
                ItemSavedData dataObject = new ItemSavedData();
                dataObject.data = new ItemSaveDetailData[itemLoader.contentLayout.childCount];
                //List<ItemSaveDetailData> data = new List<ItemSaveDetailData>();
                for (int i = 0; i < itemLoader.contentLayout.transform.childCount; i++)
                {
                    var item = itemLoader.contentLayout.GetChild(i);
                    var component = item.GetComponent<ItemProductHandler>();
                    if (component)
                    {
                        ItemSaveDetailData detail = new ItemSaveDetailData();
                        detail.Set(component);
                        //data.Add(detail);
                        dataObject.data[i] = detail;
                    }
                }
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(newPath);
                bf.Serialize(file, dataObject);
                file.Close();
            }
        }
    }

    public void OnClickBtnImportProject()
    {
        var extensionList = new[] {
        new ExtensionFilter("Vinacine Data File", "dat"),
            };
        var filePath = StandaloneFileBrowser.OpenFilePanel("Choose Vinacine data file", "", extensionList, false);
        if (filePath.Length > 0)
        {
            var finalPath = filePath[0];
            if (File.Exists(finalPath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file =
                           File.Open(finalPath, FileMode.Open);
                ItemSavedData dataObject = (ItemSavedData)bf.Deserialize(file);
                file.Close();
                Debug.Log("Loaded Data Successfully");
                // Start Load
                StartCoroutine(RoutineLoadData(dataObject));
            }
            else
                Debug.LogError("There is no save data!");
        }
        
    }

    public IEnumerator RoutineLoadData(ItemSavedData dataObject)
    {
        foreach (Transform child in contentLayout.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in dataObject.data)
        {
            var instance = Instantiate(prefab.gameObject, contentLayout, false);
            var component = instance.GetComponent<ItemProductHandler>();
            if (component)
            {
                component.LoadData(item);
                //component.SetData(item.name, path + item.name, item);
                component.SetCallback(OnClickItem);
                yield return new WaitForEndOfFrame();
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

    public void OnClickImportImage()
    {
        var extensionList = new[] {
        new ExtensionFilter("PNEG", "png"),
        new ExtensionFilter("JPEG", "jpg")
            };
        var filePaths = StandaloneFileBrowser.OpenFilePanel("Choose Image", "", extensionList, true);
        if (filePaths.Length > 0)
        {
            foreach (var filePath in filePaths)
            {
                Sprite spr = ItemProductHandler.LoadSprite(filePath);
                if (spr)
                {
                    var instance = Instantiate(prefab.gameObject, contentLayout, false);
                    instance.transform.SetAsFirstSibling();
                    var component = instance.GetComponent<ItemProductHandler>();
                    if (component)
                    {
                        var substrings = filePath.Split('\\');
                        component.SetData(substrings.Last(), filePath, spr);
                        component.SetCallback(OnClickItem);
                        OnClickItem(component);
                    }
                }
            }
        }
    }
}
