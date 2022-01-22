using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5; //�̵� �ӵ�
    CharacterController cc; //CharacaterController ������Ʈ
    public float gravity = -20; //�߷� ���ӵ��� ũ��
    float yVelocity = 0; //���� �ӵ�

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
        //2-1. �߷��� ������ ���� ���� �߰� v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        //2-2. �ٴڿ� ���� ���, ���� �׷��� ó���ϱ� ���� �ӵ��� 0���� �Ѵ�
        if(cc.isGrounded)
        {
            yVelocity = 0;
        }
        
        dir.y = yVelocity;
        //3. �̵�
        cc.Move(dir * speed * Time.deltaTime);
    }
}
