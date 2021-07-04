using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEditor : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public Canvas canvasPanel;
    public RectTransform panelImgEdit;

    [Header("ITEM SECTION")]
    public TextMeshProUGUI txtOldPrice;
    public TextMeshProUGUI txtNewPrice;
    public Image sprItem;
    public Text txtItemName;
    public Text txtItemDescription;
    public Text txtDirection;
    public TextMeshProUGUI txtDate;

    public ItemProductHandler curItem;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (curItem != null)
        {
            txtOldPrice.text = curItem.oldPrice.ToString() + "k";
            txtNewPrice.text = curItem.newPrice.ToString() + "k";
            txtItemName.text = curItem.inpName.text;
            txtItemDescription.text = curItem.inpDes.text;
            txtDirection.text = curItem.inpDirection.text;
            txtDate.text = curItem.inpDate.text;
        }
    }

    public void Load(ItemProductHandler handler)
    {
        sprItem.sprite = handler.sprite;
        curItem = handler;
        txtItemName.text = handler.inpName.text;
        txtItemDescription.text = handler.inpDes.text;
    }

    public void OnClickSave()
    {
        var rect = panelImgEdit.rect;
        var scaleX = rect.width / canvasScaler.referenceResolution.x;
        var scaleY = rect.height / canvasScaler.referenceResolution.y;
        ScreenshotHandler.TakeScreenshot_Static((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, scaleX, scaleY);
    }

    public void OnClickSaveAll()
    {
        StartCoroutine(RoutineSaveAll());
    }

    public IEnumerator RoutineSaveAll()
    {
        var itemLoader = FindObjectOfType<ItemLoader>();
        if (itemLoader)
        {
            var extensionList = new[] {
    new ExtensionFilter("PNEG", "png"),
    new ExtensionFilter("Text", "txt"),
            };
            var newPath = StandaloneFileBrowser.OpenFolderPanel("Choose Folder To Save", "png", false);
            foreach (Transform transform in itemLoader.contentLayout.transform)
            {
                var component = transform.GetComponent<ItemProductHandler>();
                if (component)
                {
                    Load(component);
                    yield return new WaitForEndOfFrame();
                    SaveScreenshotToPath(string.Format("{0}/{1}.png", newPath[0], component.itemName));
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public void SaveScreenshotToPath(string path)
    {
        var rect = panelImgEdit.rect;
        var scaleX = rect.width / canvasScaler.referenceResolution.x;
        var scaleY = rect.height / canvasScaler.referenceResolution.y;
        ScreenshotHandler.TakeScreenshot_Static((int)canvasScaler.referenceResolution.x, (int)canvasScaler.referenceResolution.y, scaleX, scaleY,
            true, path);

    }

    //    public void AnotherFunction()
    //    {
    //        var camera = Camera.main;
    //        RenderTexture activeRenderTexture = RenderTexture.active;
    //        RenderTexture.active = camera.targetTexture;

    //        camera.Render();

    //        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
    //        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
    //        image.Apply();
    //        RenderTexture.active = activeRenderTexture;

    //        byte[] bytes = image.EncodeToPNG();
    //        Destroy(image);

    //        ReceivePNGScreenShot(bytes);
    //    }

    //    public void Capture()
    //    {

    //    }

    //    public void Function()
    //    {
    //        //float x = rectT.localPosition.x + (Screen.width - rectT.rect.width) / 2;
    //        //float y = rectT.localPosition.y + (Screen.height - rectT.rect.height) / 2;

    //        var worldCorners = new Vector3[4];
    //        var screenCorners = new Vector3[4];

    //        panelImgEdit.GetWorldCorners(worldCorners);

    //        for (int i = 0; i < 4; i++)
    //        {
    //            screenCorners[i] = Camera.main.WorldToScreenPoint(worldCorners[i]);
    //            if (true)
    //            {
    //                screenCorners[i].x = (int)screenCorners[i].x;
    //                screenCorners[i].y = (int)screenCorners[i].y;
    //            }
    //        }


    //        var scaleFactor = canvasScaler.referenceResolution;
    //        var rect = Screen.safeArea;
    //        var sizeW = Screen.width;
    //        var sizeH = Screen.height;
    //        var scaleX = sizeW / scaleFactor.x;
    //        var scaleY = sizeH / scaleFactor.y;
    //        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, panelImgEdit.transform.position);
    //        //screenPos = Camera.main.WorldToViewportPoint(panelImgEdit.position);
    //        Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y);

    //        var width = (int)(panelImgEdit.rect.width * scaleX);
    //        var height = (int)(panelImgEdit.rect.height * scaleY);
    //        //var startX = screenPos2D.x - width / 2;
    //        //var startY = screenPos2D.y - height / 2;
    //        float startX = panelImgEdit.localPosition.x + (Screen.width - panelImgEdit.rect.width) / 2;
    //        float startY = panelImgEdit.localPosition.y + (Screen.height - panelImgEdit.rect.height) / 2;
    //        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

    //        Rect rex = new Rect(startX, startY, width, height);

    //        tex.ReadPixels(rex, 0, 0);
    //        tex.Apply();

    //        // Encode texture into PNG
    //        var bytes = tex.EncodeToPNG();

    //        var extensionList = new[] {
    //    new ExtensionFilter("PNEG", "png"),
    //    new ExtensionFilter("Text", "txt"),
    //};
    //        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList);
    //        File.WriteAllBytes(path, bytes);
    //        Destroy(tex);
    //    }

    //    void ReceivePNGScreenShot(byte[] pngArray)
    //    {
    //        Debug.Log("Picture taken");

    //        //Do Something With the Image (Save)
    //        //string path = Application.persistentDataPath + "/CanvasScreenShot.png";
    //        //System.IO.File.WriteAllBytes(path, pngArray);
    //        //Debug.Log(path);

    //        // Save file with filter
    //        var extensionList = new[] {
    //    new ExtensionFilter("Binary", "bin"),
    //    new ExtensionFilter("Text", "txt"),
    //};
    //        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList);
    //        File.WriteAllBytes(path, pngArray);
    //    }

    //private IEnumerator TakeScreenShot()
    //{
    //    var _camera = Camera.main;
    //    Texture2D _screenShot;
    //    yield return new WaitForEndOfFrame();

    //    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
    //    _camera.targetTexture = rt;
    //    _screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
    //    _camera.Render();
    //    RenderTexture.active = rt;
    //    _screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
    //    _camera.targetTexture = null;
    //    RenderTexture.active = null;
    //    Destroy(rt);

    //    string filename = ScreenShotName(resWidth, resHeight);

    //    //byte[] bytes = _screenShot.EncodeToPNG();
    //    //System.IO.File.WriteAllBytes(filename, bytes);

    //    Debug.Log(string.Format("Took screenshot to: {0}", filename));

    //    Sprite tempSprite = Sprite.Create(_screenShot, new Rect(0, 0, resWidth, resHeight), new Vector2(0, 0));
    //    GameObject.Find("SpriteObject").GetComponent<SpriteRenderer>().sprite = tempSprite;
    //}
}
