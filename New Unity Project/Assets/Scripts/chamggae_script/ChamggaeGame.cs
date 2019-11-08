using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamggaeGame : MonoBehaviour {

    //부모객체 .. 고양이, 사자, 사과 등등..
    public GameObject[] parentsObjList;
    
    //자식객체 .. 고양이 손, 발, 사자 꼬리 등등..
    public GameObject[] childObjList;

    //현재 선택된 부모객체 .. 고양이
    public GameObject nowObj; 

    //답안
    public Text answer;

    //부모카운터
    public int pCount = 0;

    //자식카운터
    public int cCount = 0;

    private void Awake() {
        Screen.SetResolution(1280, 720, true);
    }

    private void Start() {
        //임시
        String type = "animalImages";

        //부모객체 할당 (동물, 사물, 식물 선택)
        parentsObjList = GameObject.FindGameObjectsWithTag(type);

        //부모객체 배열 셔플
        ShuffleArray(parentsObjList);
  
        //답안 텍스트 오브젝트 할당
        answer = GameObject.Find("answer_txt").GetComponent<Text>();

        //자식객체 배열에 할당
        childObjList = new GameObject[10];
        nowObj = parentsObjList[0];
        for (int i = 0; i < nowObj.transform.childCount; i++) {
            Array.Resize(ref childObjList, nowObj.transform.childCount);
            childObjList[i] = nowObj.transform.GetChild(i).gameObject;
        }

        //실루엣화
        SpriteRenderer sr = childObjList[0].GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 0, 0, 200);
    }

    //보여라 참깨
    public void EnableImage() {
        //자식객체 한 개 활성화
        if (cCount < childObjList.Length) {
            childObjList[cCount].SetActive(true);
            cCount++;
        }
    }

    //정답확인
    public void EnableAnswer() {
        //답안 할당
        answer.text = nowObj.GetComponent<Text>().text;

        //자식객체 전부 활성화
        for (; cCount < childObjList.Length; cCount++) {
            childObjList[cCount].SetActive(true);
        }
        cCount = 0;
    }

    //넘어가기
    public void NextImage() {
        print("parentsObjList : " + parentsObjList.Length);
        //자식객체 전부 비활성화
        for (int i = 0; i < childObjList.Length; i++) {
            childObjList[i].SetActive(false);
        }

        //자식객체 배열 초기화
        Array.Clear(childObjList, 0, childObjList.Length);

        //현재 선택된 부모 객체를 변경
        if (pCount < parentsObjList.Length - 1) {
            nowObj = parentsObjList[++pCount];
        }

        //자식객체 새로 할당
        Array.Resize(ref childObjList, nowObj.transform.childCount);
        for (int i = 0; i < nowObj.transform.childCount; i++) {
            childObjList[i] = nowObj.transform.GetChild(i).gameObject;
        }

        //실루엣화
        SpriteRenderer sr = childObjList[0].GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 0, 0, 200);

        //답안 초기화
        answer.text = "";

        //카운터 초기화
        cCount = 0;
    }

    //배열 셔플
    public static void ShuffleArray<T>(T[] array) {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < array.Length; ++index) {
            random1 = UnityEngine.Random.Range(0, array.Length);
            random2 = UnityEngine.Random.Range(0, array.Length);

            tmp = array[random1];
            array[random1] = array[random2];
            array[random2] = tmp;
        }
    }
}
