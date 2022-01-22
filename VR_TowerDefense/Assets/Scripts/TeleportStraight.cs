using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    public Transform teleportCircleUI; //텔레포트를 표시할 UI
    LineRenderer lr; //선을 그을 라인 렌더러

    void Start()
    {
        //시작할 때 비활성화
        teleportCircleUI.gameObject.SetActive(false);
        //라인 렌더러 컴포넌트 얻어오기
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //왼쪽 컨트롤러의 One 버튼을 누르고 있을 때
        if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //1. 왼쪽 컨트롤러를 기준으로 Ray 만든다
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain만 Ray 충돌 검출
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //부딪힌 지점에 텔레포트 UI 표시
            }
        }
    }
}
