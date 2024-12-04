using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CustomScreenShot : MonoBehaviour
{
    public string gameName = "JellyFish Game";
    public RawImage showImg;
    private byte[] currentTexture;
    private string currentFilePath;

    public GameObject showImagePanel;
    public GameObject capturePanel;
    public GameObject saveImagePanel;

    public string ScreenShotName() 
    {
        return $"{gameName}_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
    }
    public void Capture() 
    {
        StartCoroutine(TakeScreenShot());
    }
    private IEnumerator TakeScreenShot() 
    {
        EnableCaptureUI(false);

        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);

        screenshot.ReadPixels(new Rect(0,0,width,height),0,0);
        screenshot.Apply();

        currentFilePath = Path.Combine(Application.temporaryCachePath, "temp_img.jpg");
        currentTexture = screenshot.EncodeToJPG();
        File.WriteAllBytes(currentFilePath,currentTexture);
        ShowImage();
        EnableCaptureUI(true);
        Object.Destroy(screenshot);
    }
    private void EnableCaptureUI(bool isActive) 
    {
        capturePanel.SetActive(isActive);
    }
    public void ShowImage() 
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.LoadImage(currentTexture);
        showImg.material.mainTexture = tex;
        showImagePanel.SetActive(true);
    }
    public void ShareImage() 
    {
        new NativeShare().AddFile(currentFilePath).SetSubject("Subject gone here").SetText("Share").SetUrl("https://github.com/yasirkula/UnityNativeShare")
            .SetCallback((result,shareTarget) => Debug.Log($"Share result: {result}, selected app: {shareTarget}")).Share();
    }

    public void SaveToGallery() 
    {
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(currentFilePath, gameName, ScreenShotName(),
            (isSuccess, path) =>
            {
                Debug.Log($"Media save result: {isSuccess} {path}");
                if (isSuccess)
                {
                    saveImagePanel.SetActive(true);
#if UNITY_EDITOR
                    string editorFilePath = Path.Combine(Application.persistentDataPath, ScreenShotName());
                    File.WriteAllBytes(editorFilePath, currentTexture);
#endif
                }
            }
            );
        Debug.Log("Permission result: " + permission);
    }
}
