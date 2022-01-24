using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour
{
    //드론의 상태 상수 정의
    enum DroneState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }
    
    DroneState state = DroneState.Idle; //초기 시작 상태는 Idle로 설정
    public float idleDelayTime = 2; //대기 상태의 지속 시간
    float currentTime; //경과 시간

    void Start()
    {

    }

    void Update()
    {
        switch (state)
        {
            case DroneState.Idle:
                Idle();
                break;
            case DroneState.Move:
                Move();
                break;
            case DroneState.Attack:
                Attack();
                break;
            case DroneState.Damage:
                Damage();
                break;
            case DroneState.Die:
                Die();
                break;
        }
    }
    private void Idle() //일정 시간 동안 기다렸다가 상태를 공격으로 전환
    {
        //1. 시간이 흘러야 한다
        currentTime += Time.deltaTime;
        //2. 만약 경과 시간이 대기 시간을 초과했다면
        if(currentTime > idleDelayTime)
        {
            //3. 상태를 이동으로 전환
            state = DroneState.Move;
        }
    }
    private void Move()
    {

    }
    private void Attack()
    {

    }
    private void Damage()
    {

    }
    private void Die()
    {

    }
}
