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
    Vector3 prevPos; //���� ��ġ
    float throwPower = 10; //���� ��
    Quaternion preRot; //���� ȸ��
    public float rotPower = 5; //ȸ����

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
        else
        {
            //��ü ����
            TryUngrab();
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
                //���� ��� ����
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                //�ʱ� ��ġ�� ����
                prevPos = ARAVRInput.RHandPosition;
                //�ʱ� ȸ�� �� ����
                preRot = ARAVRInput.RHand.rotation;
            }
        }
    }
    private void TryUngrab()
    {
        //���� ����
        Vector3 throwDirection = (ARAVRInput.RHandPosition - prevPos);
        //��ġ ���
        prevPos = ARAVRInput.RHandPosition;
        //���ʹϾ� ����
        //angle1 = Q1, angle2 = Q2
        //angle1 + angle2 = Q1 * Q2
        //-angle2 = Quaternion.Inverse(Q2)
        //angle2 - angle1 = Quternion.FromToRotation(Q1.Q2) = Q2 * Quaternion.Inverse(Q1)
        //ȸ�� ���� = current - previous�� ���� ����. -previous�� Inverse�� ����
        Quaternion deltaRotation = ARAVRInput.RHand.rotation * Quaternion.Inverse(preRot);
        //���� ȸ�� ����
        preRot = ARAVRInput.RHand.rotation;
        //��ư�� ���Ҵٸ�
        if(ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            //���� ���� ���·� ��ȯ
            isGrabbing = false;
            //���� ��� Ȱ��ȭ
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //�տ��� ��ź �����
            grabbedObject.transform.parent = null;
            //������
            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower;
            //���� ��ü�� ������ ����
            grabbedObject = null;
        }
    }
}
