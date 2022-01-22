using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCurve : MonoBehaviour
{
    public Transform teleportCircleUI; //텔레포트를 표시할 UI
    LineRenderer lr; //선을 그릴 라인 렌더러
    Vector3 originScale = Vector3.one * 0.02f;
    public int lineSmooth = 40; //커브의 부드러운 정도
    public float curveLength = 50; //커브의 길이
    public float gravity = -60; //커브의 중력
    public float simulateTime = 0.02f; //곡선 시뮬레이션의 간격 및 시간
    List<Vector3> lines = new List<Vector3>(); //곡선을 이루는 점들을 기억할 리스트
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
