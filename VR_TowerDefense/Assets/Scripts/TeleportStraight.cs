using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TeleportStraight : MonoBehaviour
{
    public Transform teleportCircleUI; //텔레포트를 표시할 UI
    LineRenderer lr; //선을 그을 라인 렌더러
    Vector3 originScale = Vector3.one * 0.02f;
    public bool isWarp = false; //워프 사용 여부
    public float warpTime = 0.1f; //워프에 걸리는 시간
    public PostProcessVolume post; //사용하고 있는 포스트 프로세싱 볼륨 컴포넌트

    void Start()
    {
        //시작할 때 비활성화
        teleportCircleUI.gameObject.SetActive(false);
        //라인 렌더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //왼족 컨트롤러의 One 버튼을 누르면
        if(ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //라인 렌더러 컴포넌트 활성화
            lr.enabled = true;
        }
        //왼쪽 컨트롤러의 One 버튼에서 손을 떼면
        else if(ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //라인 렌더러 비활성화
            lr.enabled = false;
            if(teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                //텔레포트 UI 위치로 순간 이동
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            //텔레포트 UI 비활성화
            teleportCircleUI.gameObject.SetActive(false);
        }
        //왼쪽 컨트롤러의 One 버튼을 누르고 있을 때
        else if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //1. 왼쪽 컨트롤러를 기준으로 Ray 만든다
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain만 Ray 충돌 검출
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //3. Ray가 부딪힌 지점에 라인 그리기
                lr.SetPosition(0, ray.origin); //시작 지점
                lr.SetPosition(1, hitInfo.point); //목표 지점
                //4. Ray가 부딪힌 지점에 텔레포트 UI 표시
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point; //hitInfo.point : 충돌한 곳의 좌표 반환
                //텔레포트 UI가 위로 누워 있도록 방향 설정
                teleportCircleUI.forward = hitInfo.normal; //hitInfo.normal : 충돌한 객체의 표면 반환
                //텔레포트 UI의 크기가 거리에 따라 보정되도록 설정
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                //Ray 충돌이 발생하지 않으면 선이 Ray 방향으로 그려지도록 처리
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                //텔레포트 UI는 화면에서 비활성화
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
