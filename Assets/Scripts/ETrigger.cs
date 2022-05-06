using Unity;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ETrigger : EventTrigger 
{
    private bool pressed = false;
    public bool Pressed { get => pressed; set => pressed = value; }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter: "+gameObject.name);
        Color sColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new(sColor.r, sColor.g, sColor.b, 0.5f);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        print("Exit: "+gameObject.name);
        Color sColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new(sColor.r, sColor.g, sColor.b, 1f);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown: -> " + eventData.hovered[0].name);
        return;
        throw new System.NotImplementedException();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {

    }
}
