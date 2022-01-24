using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public float moveSpeed = 1; //이동 속도
    Transform tower; //타워 위치
    NavMeshAgent agent; //길 찾기를 수행할 내비게이션 메시 에이전트
    public float attackRange = 3; //공격 범위
    public float attackDelayTime = 2; //공격 지연 시간

    void Start()
    {
        //타워 찾기
        tower = GameObject.Find("Tower").transform;
        //NavMeshAgent 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        //agent의 속도 설정
        agent.speed = moveSpeed;
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
            //agent 활성화
            agent.enabled = true;
        }
    }
    private void Move() //타워를 향해 이동
    {
        //내비게이션할 목적지 설정
        agent.SetDestination(tower.position);
        //공격 범위 안에 들어오면 공격 상태로 전환
        if(Vector3.Distance(transform.position, tower.position) < attackRange)
        {
            state = DroneState.Attack;
            //agent의 동작 정지
            agent.enabled = false;
        }
    }
    private void Attack()
    {
        //1. 시간이 흐른다
        currentTime += Time.deltaTime;
        //2. 경과 시간이 공격 지연 시간을 초과하면
        if(currentTime > attackDelayTime)
        {
            //3. 공격
            //4. 경과 시간 초기화
            currentTime = 0;
        }
    }
    private void Damage()
    {

    }
    private void Die()
    {

    }
}
