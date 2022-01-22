using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //이동 속도
    public float speed = 5;
    //CharacaterController 컴포넌트
    CharacterController cc;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //사용자의 입력에 따라 전후좌우로 이동
        //1. 사용자의 입력을 받는다
        float h = ARAVRInput.GetAxis("Horizontal");
        float v = ARAVRInput.GetAxis("Vertical");
        //2. 방향을 만든다
        Vector3 dir = new Vector3(h, 0, v);
        //3. 이동
        cc.Move(dir * speed * Time.deltaTime);
    }
}
