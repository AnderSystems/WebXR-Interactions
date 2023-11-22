using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using WebXR;

public class WebXR_Manager : MonoBehaviour
{
    public static WebXRManager manager;
    public static bool VR_Supported;
    public static bool AR_Supported;

    [DllImport("__Internal")]
    private static extern void CheckXRSupport();

    private void Start()
    {
        manager = FindFirstObjectByType<WebXRManager>();
        CheckXRSupport();
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleToVR()
    {
        manager.ToggleVR();
    }

    void ARSupport()
    {
        AR_Supported = true;
        skin.main().AR_BUTTON.gameObject.SetActive(AR_Supported);
    }
    void VRSupport()
    {
        VR_Supported = true;
        skin.main().VR_BUTTON.gameObject.SetActive(VR_Supported);
    }
}
