using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCurve : MonoBehaviour
{
    public Transform teleportCircleUI; //�ڷ���Ʈ�� ǥ���� UI
    LineRenderer lr; //���� �׸� ���� ������
    Vector3 originScale = Vector3.one * 0.02f;
    public int lineSmooth = 40; //Ŀ���� �ε巯�� ����
    public float curveLength = 50; //Ŀ���� ����
    public float gravity = -60; //Ŀ���� �߷�
    public float simulateTime = 0.02f; //� �ùķ��̼��� ���� �� �ð�
    List<Vector3> lines = new List<Vector3>(); //��� �̷�� ������ ����� ����Ʈ
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
