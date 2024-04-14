using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ColorBasic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject imgCircleSelector;

    private bool isMouseTouching=false;

    [SerializeField]
    private Image imgSquare;

    private float _hue;


    [SerializeField] private ColorGradient colorGradient;
    
    public float GetHue()
    {
        return _hue;
    }

    public void SetHue(float h)
    {
        _hue = h;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isMouseTouching)
        {
            // LookAt 2D

            Vector3 DirMouse = Input.mousePosition-transform.position;
            
            // get the angle
            Vector3 norTar = DirMouse.normalized;
            float angle = Mathf.Atan2(norTar.y,norTar.x)*Mathf.Rad2Deg;


            RotateToAangle(angle);
            
            

            
        }
    }

    public void RotateToAangle(float angle)
    {
        // rotate to angle
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        imgCircleSelector.transform.rotation = rotation;
        AngleZToColor();

    }

    public void AngleZToColor()
    {
        float colorBasic = imgCircleSelector.transform.rotation.eulerAngles.z;
        
        //print(colorBasic);
        
        imgSquare.color = Color.HSVToRGB(colorBasic/360, 1, 1);

        _hue = colorBasic/360;
            
        colorGradient.ColorWithGradientUsingFakeSelector();
        //TODO: fix bug move wheel and it changes
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isMouseTouching = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMouseTouching = false;
    }
}
