using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    //필요 속성 : 물체를 잡고 있는지 여부, 잡고 있는 물체, 잡을 물체의 종류, 잡을 수 있는 거리
    bool isGrabbing = false; //물체를 잡고 있는지의 여부
    GameObject grabbedObject; //잡고 있는 물체
    public LayerMask grabbedLayer; //잡을 물체의 종류
    public float grabRange = 0.2f; //잡을 수 있는 거리
    Vector3 prevPos; //이전 위치
    float throwPower = 10; //던질 힘
    Quaternion preRot; //이전 회전
    public float rotPower = 5; //회전력

    void Start()
    {
        
    }

    void Update()
    {
        //물체 잡기
        //1. 물체를 잡지 않고 있을 경우
        if(isGrabbing == false)
        {
            //잡기 시도
            TryGrab();
        }
        else
        {
            //물체 놓기
            TryUngrab();
        }
    }
    private void TryGrab()
    {
        //grab 버튼을 누르면 일정 영역 안에 있는 폭탄을 잡는다
        //1.grab 버튼을 눌렀다면
        if(ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            //2. 일정 영역 안에 폭탄이 있을 때
            //영역 안에 있는 모든 폭탄 검출
            Collider[] hitOjbects = Physics.OverlapSphere(ARAVRInput.RHandPosition, grabRange, grabbedLayer);
            //가장 가까운 폭탄 인덱스
            int closest = 0;
            //손과 가장 가까운 물체 선택
            for(int i = 1; i < hitOjbects.Length; i++)
            {
                //손과 가장 가까운 물체와의 거리
                Vector3 closestPos = hitOjbects[closest].transform.position;
                float closestDistance = Vector3.Distance(closestPos, ARAVRInput.RHandPosition);
                //다음 물체와 손의 거리
                Vector3 nextPos = hitOjbects[i].transform.position;
                float nextDistance = Vector3.Distance(nextPos, ARAVRInput.RHandPosition);
                //다음 물체와의 거리가 더 가깝다면
                if(nextDistance < closestDistance)
                {
                    //가장 가까운 물체 인덱스 교체
                    closest = i;
                }
            }
            //3. 폭탄을 잡는다
            //검출된 물체가 있을 경우
            if(hitOjbects.Length > 0)
            {
                //잡은 상태로 전환
                isGrabbing = true;
                //잡은 물체에 대한 기억
                grabbedObject = hitOjbects[closest].gameObject;
                //잡은 물체를 손의 자식으로 등록
                grabbedObject.transform.parent = ARAVRInput.RHand;
                //물리 기능 정지
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                //초기 위치값 지정
                prevPos = ARAVRInput.RHandPosition;
                //초기 회전 값 지정
                preRot = ARAVRInput.RHand.rotation;
            }
        }
    }
    private void TryUngrab()
    {
        //던질 방향
        Vector3 throwDirection = (ARAVRInput.RHandPosition - prevPos);
        //위치 기억
        prevPos = ARAVRInput.RHandPosition;
        //쿼터니언 공식
        //angle1 = Q1, angle2 = Q2
        //angle1 + angle2 = Q1 * Q2
        //-angle2 = Quaternion.Inverse(Q2)
        //angle2 - angle1 = Quternion.FromToRotation(Q1.Q2) = Q2 * Quaternion.Inverse(Q1)
        //회전 방향 = current - previous의 차로 구함. -previous는 Inverse로 구함
        Quaternion deltaRotation = ARAVRInput.RHand.rotation * Quaternion.Inverse(preRot);
        //이전 회전 저장
        preRot = ARAVRInput.RHand.rotation;
        //버튼을 놓았다면
        if(ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.RTouch))
        {
            //잡지 않은 상태로 전환
            isGrabbing = false;
            //물리 기능 활성화
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //손에서 폭탄 떼어내기
            grabbedObject.transform.parent = null;
            //던지기
            grabbedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwPower;
            //잡은 물체가 없도록 설정
            grabbedObject = null;
        }
    }
}
