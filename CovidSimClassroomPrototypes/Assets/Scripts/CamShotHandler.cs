using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShotHandler : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Camera[] covidCams = null;
    public string characterType;
    #endregion


    #region MONOBEHAVIOUR
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < covidCams.Length; i++) {
                covidCams[i].GetComponent<ScreenshotCameraScript>().TakeScreenshot(1920, 1080, i, characterType);
            }
        }
    }
    #endregion
}
