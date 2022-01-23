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
        }
    }
}
