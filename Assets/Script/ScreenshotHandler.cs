using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    private string nextPictureName = "/CameraScreenshot.png";

    public float _scaleX;
    public float _scaleY;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            var width = renderTexture.width * _scaleX;
            var height = renderTexture.height * _scaleY;
            Texture2D renderResult = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, renderTexture.height * (1 - _scaleY), width, height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            //System.IO.File.WriteAllBytes(Application.dataPath + nextPictureName, byteArray);
            //System.IO.File.WriteAllBytes(nextPictureName, byteArray);
            ReceivePNGScreenShot(byteArray, _isAuto, _curPath);
            Debug.Log("Saved CameraScreenshot at " + nextPictureName);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    void ReceivePNGScreenShot(byte[] pngArray, bool isAuto = false, string path = "")
    {
        Debug.Log("Picture taken");

        //Do Something With the Image (Save)
        //string path = Application.persistentDataPath + "/CanvasScreenShot.png";
        //System.IO.File.WriteAllBytes(path, pngArray);
        //Debug.Log(path);

        // Save file with filter
        var extensionList = new[] {
    new ExtensionFilter("PNEG", "png"),
};
        if (isAuto)
        {
            File.WriteAllBytes(path, pngArray);
        }
        else
        {
            var newPath = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList);
            if (newPath.Length > 0)
            {
                File.WriteAllBytes(newPath, pngArray);
            }
        }
    }

    private void TakeScreenshot(int width, int height, string name)
    {
        nextPictureName = name;
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, string name)
    {
        instance.TakeScreenshot(width, height, name);
    }

    public static void TakeScreenshot_Static(int width, int height, float scaleX, float scaleY)
    {
        instance._scaleX = scaleX;
        instance._scaleY = scaleY;
        instance._isAuto = false;
        instance._curPath = "";
        instance.TakeScreenshot(width, height, "/CameraScreenshot.png");
    }

    bool _isAuto = false;
    string _curPath = "";
    public static void TakeScreenshot_Static(int width, int height, float scaleX, float scaleY, bool isAuto, string path)
    {
        instance._scaleX = scaleX;
        instance._scaleY = scaleY;
        instance._isAuto = isAuto;
        instance._curPath = path;
        instance.TakeScreenshot(width, height, "/CameraScreenshot.png");
    }
}