using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Idle() //���� �ð� ���� ��ٷȴٰ� ���¸� �������� ��ȯ
    {
        //1. �ð��� �귯�� �Ѵ�
        currentTime += Time.deltaTime;
        //2. ���� ��� �ð��� ��� �ð��� �ʰ��ߴٸ�
        if(currentTime > idleDelayTime)
        {
            //3. ���¸� �̵����� ��ȯ
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
