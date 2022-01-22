using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5; //�̵� �ӵ�
    CharacterController cc; //CharacaterController ������Ʈ
    public float gravity = -20; //�߷� ���ӵ��� ũ��
    float yVelocity = 0; //���� �ӵ�
    public float jumpPower = 5; //���� ũ��

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
        //2.0 ����ڰ� �ٶ󺸴� �������� �Է� �� ��ȭ��Ű��
        dir = Camera.main.transform.TransformDirection(dir);
        //2-1. �߷��� ������ ���� ���� �߰� v = v0 + at
        yVelocity += gravity * Time.deltaTime;
        //2-2. �ٴڿ� ���� ���, ���� �׷��� ó���ϱ� ���� �ӵ��� 0���� �Ѵ�
        if(cc.isGrounded)
        {
            yVelocity = 0;
        }
        //2-3. ����ڰ� ���� ��ư�� ������ �ӵ��� ���� ũ�⸦ �Ҵ��Ѵ�
        if(ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
        {
            yVelocity = jumpPower;
        }
        
        dir.y = yVelocity;
        //3. �̵�
        cc.Move(dir * speed * Time.deltaTime);
    }
}
