using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCameraScript : MonoBehaviour
{
    #region VARIABLES
    private static ScreenshotCameraScript instance;

    public Camera currCamera = null;
    private bool takeScreenshotOnNextFrame;

    public string sectionName = null;
    private string characterName = null;

    private int camNum = 0;
    #endregion


    #region MONOBEHAVIOUR
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        currCamera = gameObject.GetComponent<Camera>();
    }


    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame) {
            takeScreenshotOnNextFrame = false;
            RenderTexture rendText = currCamera.targetTexture;

            Texture2D rendResult = new Texture2D(rendText.width, rendText.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, rendText.width, rendText.height);
            rendResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = rendResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CamShot_" + characterName + "_" + sectionName + "_" + camNum + ".png", byteArray);
            Debug.Log("Saved CamShot_" + camNum + ".png");

            RenderTexture.ReleaseTemporary(rendText);
            currCamera.targetTexture = null;
        }
    }
    #endregion


    #region SPECIFIC/SCREENSHOT
    /// <summary>
    /// Takes a screenshot from the current camera.
    /// </summary>
    /// <param name="currCam">The current camera.</param>
    public void TakeScreenshot(int width, int height, int imgNum, string characterType)
    {
        camNum = imgNum;
        characterName = characterType;
        currCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height, int imgNum, string characterType)
    {
        instance.TakeScreenshot(width, height, imgNum, characterType);
    }
    #endregion
}
