using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using SFB;
using System.IO;
using System.Linq;

public class ScreenCaptureIntoRenderTexture : MonoBehaviour
{
    private RenderTexture renderTexture;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        ScreenCapture.CaptureScreenshotIntoRenderTexture(renderTexture);
        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGBA32, ReadbackCompleted);
    }

    void ReadbackCompleted(AsyncGPUReadbackRequest request)
    {
        // Render texture no longer needed, it has been read back.
        //DestroyImmediate(renderTexture);
        Texture2D t = toTexture2D(renderTexture);
        var bytes = t.EncodeToPNG();

        //using (var imageBytes = request.GetData<byte>())
        //{

        //}
        var extensionList = new[] {
    new ExtensionFilter("PNEG", "png"),
    new ExtensionFilter("Text", "txt"),
    };
        //File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + FileCounter + ".png", Bytes);
        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", extensionList);
        File.WriteAllBytes(path, bytes);
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        Destroy(tex);//prevents memory leak
        return tex;
    }

}