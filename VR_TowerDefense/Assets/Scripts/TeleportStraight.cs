using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TeleportStraight : MonoBehaviour
{
    public Transform teleportCircleUI; //�ڷ���Ʈ�� ǥ���� UI
    LineRenderer lr; //���� ���� ���� ������
    Vector3 originScale = Vector3.one * 0.02f;
    public bool isWarp = false; //���� ��� ����
    public float warpTime = 0.1f; //������ �ɸ��� �ð�
    public PostProcessVolume post; //����ϰ� �ִ� ����Ʈ ���μ��� ���� ������Ʈ

    void Start()
    {
        //������ �� ��Ȱ��ȭ
        teleportCircleUI.gameObject.SetActive(false);
        //���� ������ ������Ʈ ������
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //���� ��Ʈ�ѷ��� One ��ư�� ������
        if(ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //���� ������ ������Ʈ Ȱ��ȭ
            lr.enabled = true;
        }
        //���� ��Ʈ�ѷ��� One ��ư���� ���� ����
        else if(ARAVRInput.GetUp(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //���� ������ ��Ȱ��ȭ
            lr.enabled = false;
            if(teleportCircleUI.gameObject.activeSelf)
            {
                GetComponent<CharacterController>().enabled = false;
                //�ڷ���Ʈ UI ��ġ�� ���� �̵�
                transform.position = teleportCircleUI.position + Vector3.up;
                GetComponent<CharacterController>().enabled = true;
            }
            //�ڷ���Ʈ UI ��Ȱ��ȭ
            teleportCircleUI.gameObject.SetActive(false);
        }
        //���� ��Ʈ�ѷ��� One ��ư�� ������ ���� ��
        else if(ARAVRInput.Get(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            //1. ���� ��Ʈ�ѷ��� �������� Ray �����
            Ray ray = new Ray(ARAVRInput.LHandPosition, ARAVRInput.LHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("Terrain");
            //2. Terrain�� Ray �浹 ����
            if(Physics.Raycast(ray, out hitInfo, 200, layer))
            {
                //3. Ray�� �ε��� ������ ���� �׸���
                lr.SetPosition(0, ray.origin); //���� ����
                lr.SetPosition(1, hitInfo.point); //��ǥ ����
                //4. Ray�� �ε��� ������ �ڷ���Ʈ UI ǥ��
                teleportCircleUI.gameObject.SetActive(true);
                teleportCircleUI.position = hitInfo.point; //hitInfo.point : �浹�� ���� ��ǥ ��ȯ
                //�ڷ���Ʈ UI�� ���� ���� �ֵ��� ���� ����
                teleportCircleUI.forward = hitInfo.normal; //hitInfo.normal : �浹�� ��ü�� ǥ�� ��ȯ
                //�ڷ���Ʈ UI�� ũ�Ⱑ �Ÿ��� ���� �����ǵ��� ����
                teleportCircleUI.localScale = originScale * Mathf.Max(1, hitInfo.distance);
            }
            else
            {
                //Ray �浹�� �߻����� ������ ���� Ray �������� �׷������� ó��
                lr.SetPosition(0, ray.origin);
                lr.SetPosition(1, ray.origin + ARAVRInput.LHandDirection * 200);
                //�ڷ���Ʈ UI�� ȭ�鿡�� ��Ȱ��ȭ
                teleportCircleUI.gameObject.SetActive(false);
            }
        }
    }
}
