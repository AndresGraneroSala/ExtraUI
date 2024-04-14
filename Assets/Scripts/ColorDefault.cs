using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ColorDefault : MonoBehaviour
{
    [SerializeField] private Color defaultColor;
    [SerializeField] private GameObject imgCircleSelector;
    [SerializeField] private Image colorBaseSquare;
    [SerializeField] private ColorBasic colorBasic;
    
    [SerializeField] private ColorGradient gradient;
    [SerializeField] private RectTransform squareFakeSelector;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        float H, S, V;

        
        Color.RGBToHSV(defaultColor, out H, out S, out V);
        
        imgCircleSelector.transform.eulerAngles = new Vector3(0 ,0,H*360);

        colorBasic.SetHue(H);
        
        colorBaseSquare.color = Color.HSVToRGB(H, 1, 1);
        
        // 0,1 -> minx maxx
        
        float saturation = S * (gradient.MaxX-gradient.MinX) + gradient.MinX;
        float value = V * (gradient.MaxY-gradient.MinY) + gradient.MinY;

        squareFakeSelector.anchoredPosition = new Vector2(saturation, value);



    }
    
}
