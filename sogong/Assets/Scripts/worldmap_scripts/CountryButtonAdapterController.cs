using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CountryButtonAdapterController : MonoBehaviour
{
    public CountryButtonAdapter prevButton;
    List<Country> conutryList = CountryFileReader.GetInstance().GetList();
    public string s = "ho";

    void Start()
    {
        CreateButtons();
    }

    public void CreateButtons()
    {
       
        foreach (Country c in conutryList)
        {
            GameObject buttonPrefab = Resources.Load("Prefabs/btnInterface_country") as GameObject; ;

            // 리스트마다 버튼 생성. parent: Viewport_CountryList
            CreateButton(c, buttonPrefab, GameObject.Find("Content_btnCountry").transform);
        }
    }

    public void CreateButton(Country country, GameObject prefab, Transform panel)
    {
        GameObject button = GameObject.Instantiate(prefab);

        // 이름 설정하고 (현재는 영어이름 그대로 씀.)
        button.transform.GetChild(0).GetComponent<Text>().text = country.getKoreanName();
        // id 설정하고
        button.name = "btn_" + country.getName();
        // 리스너 설정
        setListner(button, country);
        // parent 정해서 위치 설정
        button.transform.SetParent(panel);
    }

    void setListner(GameObject newbutton, Country country)
    {
        GameObject worldmap_svg = GameObject.Find("worldmap_svg");
        string name = newbutton.name.Substring(4);
        var obj = worldmap_svg.transform.Find(name + "(Clone)").gameObject;
        CountryButtonAdapter countryButton = new CountryButtonAdapter(newbutton, obj);
        newbutton.GetComponent<Button>().onClick.AddListener(() => { TaskOnClick(countryButton); });

        country.setAdapter(countryButton);

        /* SVG image to collider start */
        obj.AddComponent<PolygonCollider2D>();
        var polygonCollider = obj.GetComponent<PolygonCollider2D>();
        var sprite = obj.GetComponent<SpriteRenderer>().sprite;

        /***** sprite vertices to physical shape part *****/
        Vector2[] originalvertices = sprite.vertices;
        int len = originalvertices.Length;

        int scaler = len > 10000 ? 1000 : (len > 1000 ? 100 : (len > 100 ? 10 : 1));

        int target = len / scaler;
        Vector2[] filtered = new Vector2[target];
        
        for (int i = 0; i < target; i++)
        {
            filtered[i] = originalvertices[i*scaler];
            filtered[i].x *= 100;
            filtered[i].y *= 100;
        }
        //sprite.OverridePhysicsShape(new List<Vector2[]> {filtered });
        
        /***** physical shape to collider2d part *****/
        List<Vector2> test = null;
        for (int i = 0; i > polygonCollider.pathCount; i++)
            polygonCollider.SetPath(i, test);
        polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());

        }

        obj.AddComponent<spriteOnClick>();
        obj.GetComponent<spriteOnClick>().setButton(countryButton);

    }

    public void TaskOnClick(CountryButtonAdapter country)
    {
        if (country.state == "clicked")
        {
            country.AgainClick();
            prevButton = null;
        } else
        {
            country.BtnOnClick();
            if(prevButton != null)
                prevButton.AgainClick();
            prevButton = country;
        }
        //Debug.Log("TEST button: " + country.objName + " " + country.state);
    }


}
