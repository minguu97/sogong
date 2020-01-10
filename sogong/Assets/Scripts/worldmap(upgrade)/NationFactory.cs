using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class Constants
{
    public const float SCALE = 9f;                   // 국기 크기
    public const float RATIO_DISTANCE = 0.0238f;      // 거리 비율. 클수록 멀어져.
}

/**
 * 국가 GameObject 생성
 */
public class NationFactory : MonoBehaviour
{
    Color NATION_DEFAULT = new Color(0, 1, 0.7342434f, 1);             // 국가 색 기본값
    Color NATION_CLICKED = new Color(1, 0.1745283f, 0.3235905f, 1);    // 국가 선택시 색

    // Start is called before the first frame update
    void Start()
    {
        // 국가 리스트 가져와
        FileReader reader = new FileReader();
        List<Nation> list = reader.parse();

        GameObject oceanObj = GameObject.Find("ocean");

        foreach (Nation n in list)
        {
            /************************* 지도 만들기 ********************************/
            string objName = n.getName();
            // 새로운 GameObject 생성
            Object countryPrefab = Resources.Load<Object>("country_svg/" + objName);
            GameObject newCountry = Instantiate(countryPrefab) as GameObject;

            var sprite = newCountry.GetComponent<SpriteRenderer>();
            // 기본색 설정. 청록색? 00FFBB
            sprite.color = NATION_DEFAULT;

            // 객체의 모양 및 좌표 설정
            var rect = newCountry.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);     // AnchorPresets을 bottom, left로 설정
            rect.anchorMax = new Vector2(0, 0);
            rect.pivot = new Vector2(0, 0);         // Pivot 0, 0으로 설정
            rect.localScale = new Vector3(Constants.SCALE, Constants.SCALE, 1f); // Scale 설정
            rect.localPosition = n.getLocation();   // location을 x, y좌표 설정

            // 현재 객체 부모로 설정.
            newCountry.transform.SetParent(this.transform); 

            /*********************** 국가 GameObject 간 비율 조정 ******************************/
            string countryName = n.getName();
            RectTransform childRect;

            if (oceanObj)
            {
                childRect = oceanObj.transform.Find(countryName + "(Clone)").GetComponent<RectTransform>();
            }
            else
            {
                Debug.Log("Game Object 'ocean'을 찾을 수 없습니다.");
                break;
            }

            childRect.anchoredPosition = new Vector3(
                childRect.anchoredPosition.x * Constants.RATIO_DISTANCE,
                childRect.anchoredPosition.y * Constants.RATIO_DISTANCE,
                0);

            /************************* 국가의 버튼 생성 ********************************/
            GameObject buttonPrefab = Resources.Load("Prefabs/NationButton_prefab") as GameObject; ;
            GameObject button = GameObject.Instantiate(buttonPrefab);

            button.transform.GetChild(0).GetComponent<Text>().text = n.getKoreanName();
            button.name = "btn_" + n.getName();
            button.transform.SetParent(GameObject.Find("Content_btnCountry").transform);

            // 버튼 OnclickListner 설정
            button.GetComponent<Button>().onClick.AddListener(() => { taskOnClick(n, newCountry); });
        }
    }

    GameObject clickedButton = null;    // 전에 클릭 중이던 버튼
    /**
     * 버튼 클릭시 실행
     */
    void taskOnClick(Nation n, GameObject nation)
    {
        Debug.Log(n.getKoreanName() + " is clicked");

        // 같은 버튼을 두번 클릭한 경우
        if (clickedButton && clickedButton.name == nation.name)
        {
            clickedButton.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;
            clickedButton = null;
        }
        else
        {
            // 전에 선택 중이던 버튼이 있으면 되돌려 놓고
            if (clickedButton != null)
                clickedButton.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;

            // 색깔 변화
            var sprite = nation.GetComponent<SpriteRenderer>();
            sprite.color = NATION_CLICKED;
            clickedButton = nation;
        }

        // 카메라 위치 이동
        Vector3 nationPos = nation.GetComponent<RectTransform>().anchoredPosition;
        nationPos.x -= 15; 
        nationPos.y -= 7;
        nationPos.z = -10;
        GameObject.Find("Main Camera").transform.position = nationPos;
    }
}
