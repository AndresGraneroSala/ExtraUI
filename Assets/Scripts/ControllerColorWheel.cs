using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerColorWheel : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    private ColorBasic _colorBasic;
    private ColorGradient _colorGradient;

    [SerializeField] private UnityEvent <Color> changedColor,colorDefault;
    
    // Start is called before the first frame update
    void Awake()
    {
        float H, S, V;
        Color.RGBToHSV(defaultColor, out H, out S, out V);
        
        _colorBasic = GetComponentInChildren<ColorBasic>();
        _colorGradient = GetComponentInChildren<ColorGradient>();
        
        _colorGradient.Init();

        _colorBasic.RotateToAangle(H * 360 + 90);
        
        
        float saturation = S * (_colorGradient.MaxX-_colorGradient.MinX) + _colorGradient.MinX;
        float value = V * (_colorGradient.MaxY-_colorGradient.MinY) + _colorGradient.MinY;

        _colorGradient.Init();
        _colorGradient.ColorWithGradient(new Vector2(saturation,value));
        


    }

    private void Start()
    {
        colorDefault.Invoke(_colorGradient.ResultColor);

    }

    private Color colorResult;
    
    // Update is called once per frame
    void Update()
    {
        if (colorResult!= _colorGradient.ResultColor)
        {
            changedColor.Invoke(_colorGradient.ResultColor);
        }
    }
}

