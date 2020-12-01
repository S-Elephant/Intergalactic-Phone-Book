using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// What the hell am I doing?
/// Of course the customer wants it 100% dynamic but that does NOT mean I can't dragndrop the whole thing into
/// a prefab and then spawn the prefab dynamically... Good morning [Removed name of creator].
/// </summary>
public static class UGuiCreator
{
    //public static GameObject CreateText(Transform canvasTransform, string gameObjName, string text, float x, float y, int font_size=20)
    //{
    //    GameObject theGameObject = new GameObject(gameObjName);
    //    theGameObject.transform.SetParent(canvasTransform);

    //    RectTransform trans = theGameObject.AddComponent<RectTransform>();
    //    trans.anchoredPosition = new Vector2(x, y);

    //    Text textComp = theGameObject.AddComponent<Text>();
    //    textComp.font = FontMgr.Instance.ActiveFont;
    //    textComp.text = text;
    //    textComp.fontSize = font_size;
    //    textComp.horizontalOverflow = HorizontalWrapMode.Overflow;
    //    textComp.verticalOverflow = VerticalWrapMode.Overflow;

    //    return theGameObject;
    //}

    //public static GameObject CreateMenuPanel(Transform canvasTransform)
    //{
    //    GameObject theGameObject = new GameObject(gameObjName);
    //    theGameObject.transform.SetParent(canvasTransform);
    //}
}
