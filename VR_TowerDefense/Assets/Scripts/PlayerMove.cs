using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�̵� �ӵ�
    public float speed = 5;
    //CharacaterController ������Ʈ
    CharacterController cc;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //������� �Է¿� ���� �����¿�� �̵�
        //1. ������� �Է��� �޴´�
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");
        //2. ������ �����
        Vector3 dir = new Vector3(h, 0, v);
        //3. �̵�
        cc.Move(dir * speed * Time.deltaTime);
    }
}
