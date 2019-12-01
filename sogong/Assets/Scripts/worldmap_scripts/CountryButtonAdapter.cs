using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountryButtonAdapter
{
    GameObject btn;         // 버튼
    GameObject obj;         // 국가
    GameObject flag;        // 국기

    public SpriteRenderer svgSpriteRenderer;
    public string objName;
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
        objName = obj.name;
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_green");

        state = "not clicked";
    }

    public void BtnOnClick()
    {
        // 국가 색 변화
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_red");

        // 버튼 색 변화
        btn.GetComponent<Image>().color = new Color32(255, 30, 0, 255);
        btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);

        ///////////////////////// view 위치 계산 (국가를 중심으로 옮기기)
        Vector2 contentPos = GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition;
        
                                    // Content 중심
        Vector2 lb = contentPos - new Vector2(1814, 626);

        Vector2 objPos_ = lb + obj.GetComponent<RectTransform>().anchoredPosition;

        Vector2 a = contentPos - objPos_;
                        // Canvas 중심
        Vector2 b = new Vector2(512, 384) - objPos_;
                                                                                                        // 실제 중심보다 살짝 오른쪽 위로
        GameObject.Find("Content").GetComponent<RectTransform>().anchoredPosition = objPos_ + (a + b) + new Vector2(200, 100) ;


        ////////////////////// 국기 등장
        string resourcePath = "flags/" + objName.Substring(0, objName.Length - 7);
        Object flagPrefab = Resources.Load<Object>(resourcePath);

        flag = Object.Instantiate(flagPrefab) as GameObject;
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
        flagRect.anchoredPosition = new Vector3(80f, 800f, 1);

        flagRect.localScale = new Vector3(2000, 2000, 1);

        sriterenderer.sortingLayerName = "UI";

        /////// 국기 밑에 이름 출력
        GameObject textPrefab = Resources.Load("Prefabs/countryNameText") as GameObject; ;
        GameObject textObj = GameObject.Instantiate(textPrefab);
        textObj.transform.SetParent(flag.transform);
        textObj.GetComponent<Text>().text = btn.transform.GetChild(0).GetComponent<Text>().text;
        textObj.GetComponent<RectTransform>().localScale = new Vector3(0.0005f, 0.0005f, 1);
        textObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -0.04f, 1);

        state = "clicked";
    }

    public void AgainClick()
    {
        // 초록
        svgSpriteRenderer.material = Resources.Load<Material>("Materials/Surface_green");

        btn.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        btn.transform.GetChild(0).GetComponent<Text>().color = new Color32(50, 50, 50, 255);

        Object.Destroy(flag);

        state = "not clicked";
    }
}
