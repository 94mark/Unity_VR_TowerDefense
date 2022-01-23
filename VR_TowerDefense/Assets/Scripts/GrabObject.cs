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
            //���� ����� ��ź �ε���
            int closest = 0;
            //�հ� ���� ����� ��ü ����
            for(int i = 1; i < hitOjbects.Length; i++)
            {
                //�հ� ���� ����� ��ü���� �Ÿ�
                Vector3 closestPos = hitOjbects[closest].transform.position;
                float closestDistance = Vector3.Distance(closestPos, ARAVRInput.RHandPosition);
                //���� ��ü�� ���� �Ÿ�
                Vector3 nextPos = hitOjbects[i].transform.position;
                float nextDistance = Vector3.Distance(nextPos, ARAVRInput.RHandPosition);
                //���� ��ü���� �Ÿ��� �� �����ٸ�
                if(nextDistance < closestDistance)
                {
                    //���� ����� ��ü �ε��� ��ü
                    closest = i;
                }
            }
            //3. ��ź�� ��´�
            //����� ��ü�� ���� ���
            if(hitOjbects.Length > 0)
            {
                //���� ���·� ��ȯ
                isGrabbing = true;
                //���� ��ü�� ���� ���
                grabbedObject = hitOjbects[closest].gameObject;
                //���� ��ü�� ���� �ڽ����� ���
                grabbedObject.transform.parent = ARAVRInput.RHand;
            }
        }
    }
}
