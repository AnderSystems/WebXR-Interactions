using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some interactive example
/// </summary>
public class InteractionExample : Interactive
{
    public override void OnPlayerAim(XRController controller)
    {
        base.OnPlayerAim(controller);
        if (controller.Controller.GetButtonDown(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
}
