using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : Interactive
{
    public static Hotspot currentHotspot;
    public Transform teleportPosition;
    public float lerping = 8;
    [Space]
    public GameObject content;
    public GameObject highlight;

    public void HighlightOnThisFrame()
    {
        CancelInvoke(nameof(HideHighlight));
        highlight.gameObject.SetActive(true);
        Invoke(nameof(HideHighlight), (Time.deltaTime + Time.fixedDeltaTime) * 2);
    }
    void HideHighlight()
    {
        highlight.gameObject.SetActive(false);
    }
    public void ShowVisualsOnThisFrame()
    {
        CancelInvoke(nameof(HideVisuals));
        content.gameObject.SetActive(true);
        Invoke(nameof(HideVisuals), (Time.deltaTime + Time.fixedDeltaTime) * 2);
    }
    void HideVisuals()
    {
        content.gameObject.SetActive(false);
    }

    public override void OnPlayerAim(XRController controller)
    {
        base.OnPlayerAim(controller);
        HighlightOnThisFrame();
        if (controller.Controller.GetButtonDown(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            currentHotspot = this;
        }
    }
    public void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, pedXRController.main.transform.position) <= interactionRange)
        {
            ShowVisualsOnThisFrame();
        }

        if (currentHotspot == this)
        {
            if (Vector3.Distance(transform.position, pedXRController.main.transform.position) >= 0.1f)
            {
                pedXRController.main.transform.position = Vector3.Lerp(pedXRController.main.transform.position, teleportPosition.position, lerping * Time.deltaTime);
                pedXRController.main.transform.rotation = Quaternion.Lerp(pedXRController.main.transform.rotation, teleportPosition.rotation, lerping * Time.deltaTime);
                pedXRController.main.thurnAngle = Mathf.LerpAngle(pedXRController.main.thurnAngle, teleportPosition.eulerAngles.y, lerping * Time.deltaTime);
            } else
            {
                pedXRController.main.transform.position = teleportPosition.position;
                pedXRController.main.transform.rotation = teleportPosition.rotation;
                pedXRController.main.thurnAngle = teleportPosition.eulerAngles.y;
                currentHotspot = null;
            }
        }
    }
    private void OnValidate()
    {
        if (!teleportPosition)
        {
            teleportPosition = this.transform;
        }
    }
}
