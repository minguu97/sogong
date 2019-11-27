﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonAdapter : MonoBehaviour
{
    GameObject btn;         // 버튼
    GameObject obj;         // 국가
    GameObject flag;        // 국기

    public SpriteRenderer svgSpriteRenderer;
    public string name;
    public string state;

    /// <summary>
    /// 국가 그림과 버튼 연결
    /// </summary>
    /// <param name="btn">버튼</param>
    /// <param name="obj">국가</param>
    public CountryButtonAdapter(GameObject btn, GameObject obj)
    {
        this.btn = btn;
        this.obj = obj;
        svgSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        name = obj.name;
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_green");

        state = "not clicked";
    }

    public void BtnOnClick()
    {
        // 국가 색 변화
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_red");

        // 버튼 색 변화
        btn.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);

        // 위치 계산
        Vector2 contentPos = GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition;
        
        Vector2 lb = contentPos - new Vector2(900, 400);

        Vector2 objPos_ = lb + obj.GetComponent<RectTransform>().anchoredPosition;

        Vector2 a = contentPos - objPos_;
        Vector2 b = new Vector2(384, 288) - objPos_;

        GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition = objPos_ + (a+b) ;

        ////////////////////// 국기 등장
        string resourcePath = "flags/" + name.Substring(0, name.Length - 7);
        Object flagPrefab = Resources.Load<Object>(resourcePath);

        flag = Instantiate(flagPrefab) as GameObject;
        var flagRect = flag.AddComponent<RectTransform>();
        var sriterenderer = flag.GetComponent<SpriteRenderer>();
        var flagButton = flag.AddComponent<Button>();

        // AnchorPresets을 bottom, left로 설정
        flagRect.anchorMin = new Vector2(0, 0);
        flagRect.anchorMax = new Vector2(0, 0);

        // Pivot 0, 0으로 설정
        flagRect.pivot = new Vector2(0, 0);

        GameObject viewport = GameObject.Find("Viewport");
        flag.transform.SetParent(viewport.transform);
        flagRect.anchoredPosition = new Vector3(155f, 470f, 1);

        flagRect.localScale = new Vector3(2000, 2000, 1);

        sriterenderer.sortingLayerName = "UI";

        state = "clicked";
    }

    public void AgainClick()
    {
        // 초록
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_green");
        btn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);

        Destroy(flag);

        state = "not clicked";
    }
}