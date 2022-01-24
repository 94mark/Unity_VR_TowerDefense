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
            if(_hp <= 0)
            {
                Destroy(gameObject);
            }
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
