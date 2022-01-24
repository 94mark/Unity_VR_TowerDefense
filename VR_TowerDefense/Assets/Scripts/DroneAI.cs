using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneAI : MonoBehaviour
{
    //����� ���� ��� ����
    enum DroneState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }
    
    DroneState state = DroneState.Idle; //�ʱ� ���� ���´� Idle�� ����
    public float idleDelayTime = 2; //��� ������ ���� �ð�
    float currentTime; //��� �ð�
    public float moveSpeed = 1; //�̵� �ӵ�
    Transform tower; //Ÿ�� ��ġ
    NavMeshAgent agent; //�� ã�⸦ ������ ������̼� �޽� ������Ʈ
    public float attackRange = 3; //���� ����
    public float attackDelayTime = 2; //���� ���� �ð�

    void Start()
    {
        //Ÿ�� ã��
        tower = GameObject.Find("Tower").transform;
        //NavMeshAgent ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        //agent�� �ӵ� ����
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
    private void Idle() //���� �ð� ���� ��ٷȴٰ� ���¸� �������� ��ȯ
    {
        //1. �ð��� �귯�� �Ѵ�
        currentTime += Time.deltaTime;
        //2. ���� ��� �ð��� ��� �ð��� �ʰ��ߴٸ�
        if(currentTime > idleDelayTime)
        {
            //3. ���¸� �̵����� ��ȯ
            state = DroneState.Move;
            //agent Ȱ��ȭ
            agent.enabled = true;
        }
    }
    private void Move() //Ÿ���� ���� �̵�
    {
        //������̼��� ������ ����
        agent.SetDestination(tower.position);
        //���� ���� �ȿ� ������ ���� ���·� ��ȯ
        if(Vector3.Distance(transform.position, tower.position) < attackRange)
        {
            state = DroneState.Attack;
            //agent�� ���� ����
            agent.enabled = false;
        }
    }
    private void Attack()
    {
        //1. �ð��� �帥��
        currentTime += Time.deltaTime;
        //2. ��� �ð��� ���� ���� �ð��� �ʰ��ϸ�
        if(currentTime > attackDelayTime)
        {
            //3. ����
            //4. ��� �ð� �ʱ�ȭ
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
