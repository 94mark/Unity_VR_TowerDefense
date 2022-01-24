using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    //데미지를 표현할 UI
    public Transform damageUI;
    public Image damageImage;

    public int initialHP = 10; //타워의 최초 HP
    int _hp = 0; //내부 hp 변수

    public static Tower Instance; //Tower의 싱글턴 객체
    public float damageTime = 0.1f; //깜빡거리는 시간

    //_hp의 get/set 프로퍼티
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            //hp가 0 이하면 제거
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    //데미지 처리를 위한 코루틴 함수
    IEnumerator DamageEvent()
    {
        //damageImage 컴포넌트를 활성화
        damageImage.enabled = true;
        //damageTime만큼 기다린다
        yield return new WaitForSeconds(damageTime);
        //다시 원래대로 비활성화
        damageImage.enabled = false;
    }

    void Awake()
    {
        //싱글턴 객체 값 할당
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        _hp = initialHP;
        //카메라의 nearClipPlane 값을 기억해둔다
        float z = Camera.main.nearClipPlane + 0.01f;
        //damageUI 객체의 부모를 카메라로 설정
        damageUI.parent = Camera.main.transform;
        //damageUI의 위치를 X,Y는 0,Z 값은 카메라의 near 값으로 설정
        damageUI.localPosition = new Vector3(0, 0, z);
        //damageImage는 보이지 않도록 초기에 비활성화해 놓는다
        damageImage.enabled = false;
    }

    void Update()
    {
        
    }
}
