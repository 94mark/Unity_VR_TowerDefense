using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
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
    public bool isRemoteGrab = true; //���Ÿ����� ��ü�� ��� ��� Ȱ��ȭ ����
    public float remoteGrabDistance = 20; //���Ÿ����� ��ü�� ���� �� �ִ� �Ÿ�

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
            //���Ÿ� ��ü ��⸦ ����Ѵٸ�
            if(isRemoteGrab)
            {
                //�� �������� Ray ����
                Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
                RaycastHit hitInfo;
                //SphereCast�� �̿��� ��ü �浹�� üũ
                if(Physics.SphereCast(ray, 0.5f, out hitInfo, remoteGrabDistance, grabbedLayer))
                {
                    //���� ���·� ��ȯ
                    isGrabbing = true;
                    //���� ��ü�� ���� ���
                    grabbedObject = hitInfo.transform.gameObject;
                    //��ü�� �������� ��� ����
                    StartCoroutine(GrabbingAnimation());
                }
                return;
            }
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
            //���ӵ� = (1/dt) * dtheta(Ư�� �� ���� ���� ����)
            float angle;
            Vector3 axis;
            deltaRotation.ToAngleAxis(out angle, out axis);
            Vector3 angularVelocity = (1.0f / Time.deltaTime) * angle * axis;
            //���� ��ü�� ������ ����
            grabbedObject = null;
        }
    }
    IEnumerator GrabbingAnimation()
    {
        //���� ��� ����
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        //�ʱ� ��ġ �� ����
        prevPos = ARAVRInput.RHandPosition;
        //�ʱ� ȸ�� �� ����
        preRot = ARAVRInput.RHand.rotation;
        Vector3 startLocation = grabbedObject.transform.position;
        Vector3 targetLocation = ARAVRInput.RHandPosition + ARAVRInput.RHandPosition * 0.1f;
        float currentTime = 0;
        float finishTime = 0.2f;
        //�����
        float elapseRate = currentTime / finishTime;
        while(elapseRate < 1)
        {
            currentTime += Time.deltaTime;
            elapseRate = currentTime / finishTime;
            grabbedObject.transform.position = Vector3.Lerp(startLocation, targetLocation, elapseRate);
            yield return null;
        }
        //���� ��ü�� ���� �ڽ����� ���
        grabbedObject.transform.position = targetLocation;
        grabbedObject.transform.parent = ARAVRInput.RHand;
    }
}
