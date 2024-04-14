using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UseTheColor : MonoBehaviour
{
    private Image img;
    
    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
    }


    public void ChangeColor(Color color)
    {
        img.color = color;
    }

    public void Test()
    {
        print("a");
    }
}
