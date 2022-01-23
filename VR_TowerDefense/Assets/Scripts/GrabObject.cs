using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    //�ʿ� �Ӽ� : ��ü�� ��� �ִ��� ����, ��� �ִ� ��ü, ���� ��ü�� ����, ���� �� �ִ� �Ÿ�
    bool isGrabbing = false; //��ü�� ��� �ִ����� ����
    GameObject grabbedObject; //��� �ִ� ��ü
    public LayerMask grabbedLayer; //���� ��ü�� ����
    public float grabRange = 0.2f; //���� �� �ִ� �Ÿ�
    
    void Start()
    {
        
    }

    void Update()
    {
        //��ü ���
        //1. ��ü�� ���� �ʰ� ���� ���
        if(isGrabbing == false)
        {
            //��� �õ�
            TryGrab();
        }
    }
    private void TryGrab()
    {
        //grab ��ư�� ������ ���� ���� �ȿ� �ִ� ��ź�� ��´�
        //1.grab ��ư�� �����ٸ�
        if(ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            //2. ���� ���� �ȿ� ��ź�� ���� ��
            //���� �ȿ� �ִ� ��� ��ź ����
            Collider[] hitOjbects = Physics.OverlapSphere(ARAVRInput.RHandPosition, grabRange, grabbedLayer);
            //3. ��ź�� ��´�
        }
    }
}
