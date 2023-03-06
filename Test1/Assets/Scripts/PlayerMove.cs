using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    // �������
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _Speed;
    // ������������ ����
    public LayerMask clickOn;
    // �����
    private NavMeshAgent m_Agent;

    private void Start()
    {
        // �������� ��������� ������
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // ������������ �����
    private void Update()
    {
        //��������� �������
        if (Input.GetMouseButtonDown(0))
        {
            //������� ���
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hInfo;
            if (Physics.Raycast(myRay, out hInfo, 100, clickOn))
            {
                    // �������
                    m_Agent.SetDestination(hInfo.point);
            }
        }
    }

    //������������ ����������
    private void FixedUpdate()
    {

        // ��������� ������� �� ��������
        if(_joystick.Horizontal != 0)
        {
            //��������� �����
            m_Agent.enabled = false;
        }
        else
        {
            //�������� �����
            m_Agent.enabled = true;
        }
        //�������
        _rb.velocity = new Vector3(_joystick.Horizontal * _Speed , _rb.velocity.y , _joystick.Vertical * _Speed);
       
    }
}
