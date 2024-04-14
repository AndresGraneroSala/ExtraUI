using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorGradient : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isMouseTouching=false;
    private Canvas parentCanvas;

    [SerializeField] private float minX,maxX,minY,maxY;

    public float MinX => minX;
    public float MaxX => maxX;

    public float MinY => minY;
    public float MaxY => maxY;

    [SerializeField] private Image resultImg;

    [SerializeField] private Color resultColor;

    public Color ResultColor => resultColor;

    [SerializeField] private ColorBasic colorBasic;

    public ColorBasic GetColorBasic => colorBasic;

    [SerializeField] private RectTransform fakeSelector;
    private Image imgFakeSelector;
    
    private RectTransform thisRectTransform;

    private Image thisImage;


   
    
    private Vector2 positionInCanvas;
    
    


    private Vector2 sizes;

    
    
    // Start is called before the first frame update
    public void Init()
    {
        imgFakeSelector = fakeSelector.GetComponent<Image>();

        parentCanvas = GetComponentInParent<Canvas>();


        thisRectTransform = GetComponentInParent<RectTransform>();
        thisImage = GetComponentInParent<Image>();


        sizes = new Vector2();
        sizes.x = thisRectTransform.rect.width;
        sizes.y = thisRectTransform.rect.height;

        Vector2 paddigImg = thisImage.raycastPadding;

        positionInCanvas = thisRectTransform.anchoredPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            transform.position, parentCanvas.worldCamera,
            out positionInCanvas);



        minX = -(sizes.x / 2) + thisImage.raycastPadding.x;
        maxX = (sizes.x / 2) - thisImage.raycastPadding.x;
        minY = -(sizes.y / 2) + thisImage.raycastPadding.y;
        maxY = (sizes.y / 2) - thisImage.raycastPadding.y;






        //resultColor = Color.HSVToRGB(thisImage);

    }

    Vector2 movePos;

    // Update is called once per frame
    void Update()
    {
        if (isMouseTouching)
        {

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);


            movePos = new Vector2(
                Mathf.Clamp(movePos.x-positionInCanvas.x, minX, maxX),
                Mathf.Clamp(movePos.y-positionInCanvas.y, minY, maxY)
            );

            ColorWithGradient(movePos);
        }
        
        


        
    }


    public void ColorWithGradientUsingFakeSelector()
    {
        ColorWithGradient(fakeSelector.anchoredPosition);
    }

    public void ColorWithGradient(Vector2 position)
    {
        
            
        //print(movePos);

        fakeSelector.anchoredPosition = position;

            

        float saturation = (float)(position.x - minX) / (maxX - minX) * (1 - 0) - 0;
        float value = (float)(position.y - minY) / (maxY - minY) * (1 - 0) - 0;

        
            
        resultColor = Color.HSVToRGB(colorBasic.GetHue(),saturation,value);
            
        resultImg.color = resultColor;
            

            
        //check if change to black in white space
        if (position.x/2+50 > position.y)
        {
            imgFakeSelector.color= Color.white;
        }
        else
        {
            imgFakeSelector.color= Color.black;
        }
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
