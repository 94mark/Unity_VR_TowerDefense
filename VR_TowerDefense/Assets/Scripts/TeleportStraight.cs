using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStraight : MonoBehaviour
{
    public Transform teleportCircleUI; //�ڷ���Ʈ�� ǥ���� UI
    LineRenderer lr; //���� ���� ���� ������

    void Start()
    {
        //������ �� ��Ȱ��ȭ
        teleportCircleUI.gameObject.SetActive(false);
        //���� ������ ������Ʈ ������
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //���� ��Ʈ�ѷ��� One ��ư�� ������ ���� ��
        if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //1. ���� ��Ʈ�ѷ��� �������� Ray �����
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain�� Ray �浹 ����
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //�ε��� ������ �ڷ���Ʈ UI ǥ��
            }
        }
    }
}
