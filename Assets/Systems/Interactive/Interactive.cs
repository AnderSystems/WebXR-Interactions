using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The master interactive module
/// </summary>
public class Interactive : MonoBehaviour, iPlayerInteraction
{
    public float interactionRange = 10;
    public Button.ButtonClickedEvent OnTriggerEvent;
    public virtual void OnPlayerAim(XRController controller)
    {
        if (Vector3.Distance(transform.position, controller.transform.position) > interactionRange)
            return;

        controller.SetRayColourAtThisFrame(new Color(0,1,1,1));
        if (controller.Controller.GetButtonDown(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            OnTriggerEvent.Invoke();
        }
    }


    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0,1,1,0.3f);
        Gizmos.DrawSphere(transform.position, -interactionRange);
    }

    public virtual void OnPlayerTriggerDown(XRController controller){}
}
