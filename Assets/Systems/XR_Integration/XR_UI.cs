using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Selectable))]
public class XR_UI : Interactive, iPlayerInteraction
{
    public static XR_UI lastSelection { get; set; }

    //Basic vars
    public RectTransform rectTransform { get { return (RectTransform)transform; } }
    public Selectable selectable;
    public Slider slider;
    BoxCollider col;

    public void SetCollider(BoxCollider collider, RectTransform rect)
    {
        Bounds b = RectTransformUtility.CalculateRelativeRectTransformBounds(rect);
        Vector3 s = b.size;
        s.z = (selectable.targetGraphic.depth) * 0.01f;

        collider.center = b.center;
        collider.size = s;
    }
    public void Setup()
    {
        selectable = GetComponent<Selectable>();
        col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        SetCollider(col, rectTransform);
    }

    //Callback
    public override void OnPlayerAim(XRController controller)
    {
        base.OnPlayerAim(controller);
        isPlayerAiming();

        //Player press interaction btn
        if (controller.Controller.GetButton(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            OnSlider(controller);
        }

        //Player start press interaction btn
        if (controller.Controller.GetButtonDown(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            selectable.OnPointerDown(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
            selectable.OnSelect(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
            lastSelection = this;
        }

        //Player release interaction btn
        if (controller.Controller.GetButtonUp(WebXR.WebXRController.ButtonTypes.Trigger))
        {
            selectable.OnPointerUp(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
        }
    }
    public override void OnPlayerTriggerDown(XRController controller)
    {
        base.OnPlayerTriggerDown(controller);
        if (controller.ray.hit.collider != null)
        {
            if (controller.ray.hit.collider.gameObject != this.gameObject)
            {
                selectable.OnPointerUp(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
                selectable.OnDeselect(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
            }
        } else
        {
            selectable.OnPointerUp(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
            selectable.OnDeselect(new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left });
        }
    }

    //Behaviours
    public void isPlayerAiming()
    {
        CancelInvoke(nameof(PlayerIsNotAiming));
        selectable.OnPointerEnter(new PointerEventData(EventSystem.current));
        Invoke(nameof(PlayerIsNotAiming), Time.deltaTime + Time.fixedDeltaTime);
    }
    void PlayerIsNotAiming()
    {
        selectable.OnPointerExit(new PointerEventData(EventSystem.current));
    }
    public void OnSlider(XRController controller)
    {
        slider = GetComponent<Slider>();
        col = GetComponent<BoxCollider>();
        //Slider
        if (slider)
        {
            Vector3 p = transform.InverseTransformPoint(controller.ray.hit.point);
            switch (slider.direction)
            {
                case Slider.Direction.LeftToRight:
                    p.x = ((p.x / col.size.x) + 0.5f); //Range between 0 and 1
                    p.x *= slider.maxValue; //Range between 0 and MaxSliderValue
                    slider.value = p.x;
                    break;
                case Slider.Direction.RightToLeft:
                    p.x = ((-p.x / col.size.x) + 0.5f); //Range between 0 and 1
                    p.x *= slider.maxValue; //Range between 0 and MaxSliderValue
                    slider.value = p.x;
                    break;
                case Slider.Direction.BottomToTop:
                    p.y = ((p.y / col.size.y) + 0.5f); //Range between 0 and 1
                    p.y *= slider.maxValue; //Range between 0 and MaxSliderValue
                    slider.value = p.y;
                    break;
                case Slider.Direction.TopToBottom:
                    p.y = ((-p.y / col.size.y) + 0.5f); //Range between 0 and 1
                    p.y *= slider.maxValue; //Range between 0 and MaxSliderValue
                    slider.value = p.y;
                    break;
                default:
                    break;
            }
        }
    }

    //Mono
    public void OnValidate()
    {
        Setup();
    }
    public override void OnDrawGizmosSelected()
    {
        Setup();
        base.OnDrawGizmosSelected();
    }
}
