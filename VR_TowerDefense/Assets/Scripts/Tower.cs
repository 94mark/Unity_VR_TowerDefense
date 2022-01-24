using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    //�������� ǥ���� UI
    public Transform damageUI;
    public Image damageImage;

    public int initialHP = 10; //Ÿ���� ���� HP
    int _hp = 0; //���� hp ����
    //_hp�� get/set ������Ƽ
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            //hp�� 0 ���ϸ� ����
            if(_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        _hp = initialHP;
        //ī�޶��� nearClipPlane ���� ����صд�
        float z = Camera.main.nearClipPlane + 0.01f;
        //damageUI ��ü�� �θ� ī�޶�� ����
        damageUI.parent = Camera.main.transform;
        //damageUI�� ��ġ�� X,Y�� 0,Z ���� ī�޶��� near ������ ����
        damageUI.localPosition = new Vector3(0, 0, z);
        //damageImage�� ������ �ʵ��� �ʱ⿡ ��Ȱ��ȭ�� ���´�
        damageImage.enabled = false;
    }

    void Update()
    {
        
    }
}
