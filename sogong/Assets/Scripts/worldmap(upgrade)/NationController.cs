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
public class NationController : MonoBehaviour
{
    Color NATION_DEFAULT = new Color(0, 0.6603774f, 0.4322661f, 1);             // 국가 색 기본값
    Color NATION_CLICKED = new Color(1, 0.1745283f, 0.3235905f, 1);    // 국가 선택시 색

    // "앉아서 세계속으로"의 전반적인 기능을 맡음.
    // 1. 파일로부터 국가 리스트 만들기
    // 2. 국가 리스트로부터 svg 파일로 지도 만들기
    // 3. 지도 왼편에 리스트(Scroll View)에 들어가는 국가 별 버튼 만들기
    // 4. 검색 기능
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
            GameObject buttonPrefab = Resources.Load("Prefabs/NationButton_prefab") as GameObject; 
            GameObject button = GameObject.Instantiate(buttonPrefab);

            button.transform.GetChild(0).GetComponent<Text>().text = n.getKoreanName();
            button.name = "btn_" + n.getName();
            button.transform.SetParent(GameObject.Find("Content_btnCountry").transform);

            // 버튼 OnclickListner 설정
            button.GetComponent<Button>().onClick.AddListener(() => { taskOnClick(n, newCountry); });

            // 지도 맵 Collider, ClickEvent 설정
            newCountry.AddComponent<PolygonCollider2D>();
            newCountry.GetComponent<PolygonCollider2D>().isTrigger = true;
            newCountry.AddComponent<MapCallbackListener>();
            newCountry.GetComponent<MapCallbackListener>().setTarget(button);
            //newCountry.AddComponent<Button>();
            //newCountry.GetComponent<Button>().onClick.AddListener(() => { taskOnClick(n, newCountry); });

        }


        /************************* 국가 검색 버튼 리스너 설정 *************************/
        GameObject searchBtn = GameObject.Find("SearchButton");
        searchBtn.GetComponent<Button>().onClick.AddListener(() => { searchOnClick(list); });
    }


    // 국가 리스트 버튼 클릭시 실행되는 기능
    // 1. 선택된 국가 이미지 색깔 변화
    // 2. 카메라 이동
    // 3. 좌상단 국기 렌더링
    // 같은 버튼 두 번 클릭에 대한 처리를 추가함.
    GameObject clickedNation = null;    // 전에 클릭 중이던 국가
    bool againClick = false;

    //taskOnClickMap(Nation n, GameObject nation)

    // n : 선택된 국가 클래스
    // nation : n에 해당하는 unity GameObject
    void taskOnClick(Nation n, GameObject nation)
    {
        if (n == null || nation == null)
        {
            Debug.Log("Error: Something is null...");
            return;
        }

        Debug.Log(n.getKoreanName() + " is clicked");

        if (clickedNation && clickedNation.name == nation.name)  // 같은 버튼을 두번 클릭한 경우
        {
            againClick = true;
        }

        /******************* 선택된 국가 색깔 변화 *************************/
        if (againClick)
        {
            clickedNation.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;
            clickedNation = null;
        }
        else
        {
            // 전에 선택 중이던 버튼이 있으면 되돌려 놓고
            if (clickedNation != null)
                clickedNation.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;

            // 색깔 변화
            var sprite = nation.GetComponent<SpriteRenderer>();
            sprite.color = NATION_CLICKED;
            clickedNation = nation;
        }


        /******************* 선택된 국가 위로 카메라 위치 이동 *************************/
        Vector3 nationPos = nation.GetComponent<RectTransform>().anchoredPosition;
        nationPos.x -= 15;  // ocean 크기에 따라 달라짐.
        nationPos.y -= 7;   // ocean 크기에 따라 달라짐.
        nationPos.z = -10;
        GameObject.Find("Main Camera").transform.position = nationPos;


        /******************* 국기 렌더링 *************************/
        GameObject flag = GameObject.Find("Flag");
        var svgimage = flag.GetComponent<SVGImage>();
        if (againClick)
        {
            // 다시 클릭이면 국기 없애
            svgimage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            flag.transform.Find("NationNameText").GetComponent<Text>().text = "";
        }
        else
        {
            // 국기 sprite 설정
            svgimage.sprite = Resources.Load<Sprite>("flags/" + n.getName());
            flag.transform.Find("NationNameText").GetComponent<Text>().text = n.getKoreanName();
        }

        againClick = false;
    }

    void searchOnClick(List<Nation> list)
    {
        Text text = GameObject.Find("SearchWord").GetComponent<Text>();

        Debug.Log("Search about " + text.text);

        foreach (Nation n in list)
        {
            string comp = text.text;
            if (comp == n.getKoreanName())
            {
                Debug.Log("It's " + n.getKoreanName() + "!!!");

                taskOnClick(n, GameObject.Find(n.getName() + "(Clone)"));
                return;
            }
        }
    }
}
