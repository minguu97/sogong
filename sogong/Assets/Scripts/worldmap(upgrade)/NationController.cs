using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
            newCountry.name = objName;

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

            // 현재 객체(Unity GameObject ocean)를 부모로 설정.
            newCountry.transform.SetParent(this.transform); 


            /*********************** 국가 GameObject 간 비율 조정 ******************************/
            string countryName = n.getName();
            RectTransform childRect;

            if (oceanObj)
            {
                childRect = oceanObj.transform.Find(countryName).GetComponent<RectTransform>();
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
    // 2. 좌상단 국기 렌더링
    // 3. 카메라 이동

    // 같은 버튼 두 번 클릭에 대한 처리를 추가함.
    GameObject clickedNation = null;    // 전에 클릭 중이던 국가
    bool againClick = false;

    //taskOnClickMap(Nation n, GameObject nation)

    // n : 선택된 국가 클래스
    // nation : n에 해당하는 unity GameObject
    void taskOnClick(Nation n, GameObject nation)
    { 
        Debug.Log(n.getKoreanName() + " is clicked");

        // 같은 버튼을 두번 클릭한 경우
        if (clickedNation && clickedNation.name == nation.name)  
        {
            againClick = true;
        }

        GameObject flag = GameObject.Find("Flag");
        GameObject nationInfo = GameObject.Find("NationInfo");
        if (againClick)
        {
            clickedNation.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;
            clickedNation = null;
            flag.GetComponent<SVGImage>().sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            flag.transform.Find("NationNameText").GetComponent<Text>().text = "";
            flag.GetComponent<Button>().onClick.RemoveAllListeners();
            Destroy(nationInfo);
        }
        else
        {
            // 전에 선택 중이던 버튼이 있으면 되돌려 놓고
            if (clickedNation != null)
            {
                clickedNation.GetComponent<SpriteRenderer>().color = NATION_DEFAULT;
                flag.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(nationInfo);
            }

            // 1. 색깔 변화
            var sprite = nation.GetComponent<SpriteRenderer>();
            sprite.color = NATION_CLICKED;

            // 2. 국기 렌더링
            flag.GetComponent<SVGImage>().sprite = Resources.Load<Sprite>("flags/" + n.getName());
            flag.transform.Find("NationNameText").GetComponent<Text>().text = n.getKoreanName();
            flag.GetComponent<Button>().onClick.AddListener(() => { flagOnClick(n.getName()); });

            clickedNation = nation;
        }

        // 3. 카메라 이동

        Vector3 nationPos = nation.GetComponent<RectTransform>().anchoredPosition;
        nationPos.x -= 15;  // ocean 크기에 따라 달라짐.
        nationPos.y -= 7;   // ocean 크기에 따라 달라짐.
        nationPos.z = -10;

        // Camera로부터 cameramapmove를 획득한 뒤에 목표 target과 목표 zoom을 변경한다.
        CameraMapMove target = GameObject.Find("Main Camera").GetComponent<CameraMapMove>();
        target.setTarget(nationPos);

        againClick = false;
    }

    // list : 검색범위인 국가 리스트
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

                taskOnClick(n, GameObject.Find(n.getName()));
                return;
            }
        }
    }

    string clickedFlag = null;
    // n : 클릭된 국기(국가)의 이름
    void flagOnClick(string n)
    {
        // 국기 두 번 클릭시 처리
        if (clickedFlag != null && clickedFlag == n)
        {
            Destroy(GameObject.Find("NationInfo"));
            clickedFlag = null;
            return;
        }

        string capital;
        string language;
        string[] features;
        string attraction1;
        string attraction2;

        // 국가 정보를 txt 파일로부터 읽어내서 저장.
        Debug.Log("Reading the information of [" + n + "] from txt file...");
        clickedFlag = n;

        // n = "guatemala";    // TEST

        // 상세정보가 없는 국가들 처리
        try
        {
            TextAsset nation_info = Resources.Load<TextAsset>("nation_info/" + n + "/info");
            StringReader buffer = new StringReader(nation_info.text);

            capital = buffer.ReadLine();
            language = buffer.ReadLine();
            int fCount = System.Convert.ToInt32(buffer.ReadLine());
            features = new string[fCount];
            for (int i = 0; i < fCount; i++)
            {
                features[i] = buffer.ReadLine();
            }
            attraction1 = buffer.ReadLine();
            attraction2 = buffer.ReadLine();

            // 국가 정보 panel을 만들어
            GameObject infoPrefab = Resources.Load<GameObject>("Prefabs/NationInfo");
            GameObject infoPanel = Instantiate<GameObject>(infoPrefab);
            infoPanel.name = "NationInfo";
            infoPanel.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // 읽은 데이터를 Unity UI에 할당해줘
            infoPanel.transform.Find("capital").Find("Capital").GetComponent<Text>().text = capital;
            infoPanel.transform.Find("language").Find("Language").GetComponent<Text>().text = language;
            infoPanel.transform.Find("feature").Find("Panel").Find("Feature").GetComponent<Text>().text = features[0];     // 일단은
            infoPanel.transform.Find("Attraction1").Find("Description1").GetComponent<Text>().text = attraction1;
            infoPanel.transform.Find("Attraction2").Find("Description2").GetComponent<Text>().text = attraction2;
            infoPanel.transform.Find("Attraction1").GetComponent<Image>().sprite = Resources.Load<Sprite>("nation_info/" + n + "/attraction1");
            infoPanel.transform.Find("Attraction2").GetComponent<Image>().sprite = Resources.Load<Sprite>("nation_info/" + n + "/attraction2");
        }
        catch (System.NullReferenceException e)
        {
            // TODO 정보가 없는 국가들 다른 정보들로 채워넣어야 대
            Debug.Log("No information about [" + n + "]");
        }

    }
}
