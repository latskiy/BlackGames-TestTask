using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    // Обьекты
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _Speed;
    // Кликабельный слой
    public LayerMask clickOn;
    // Агент
    private NavMeshAgent m_Agent;

    private void Start()
    {
        // Получаем компонент агента
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Передвижение тапом
    private void Update()
    {
        //Фиксируем нажатие
        if (Input.GetMouseButtonDown(0))
        {
            //Пускаем луч
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hInfo;
            if (Physics.Raycast(myRay, out hInfo, 100, clickOn))
            {
                    // Двигаем
                    m_Agent.SetDestination(hInfo.point);
            }
        }
    }

    //Передвижение джойстиком
    private void FixedUpdate()
    {

        // Проверяем активен ли джойстик
        if(_joystick.Horizontal != 0)
        {
            //Выключаем агент
            m_Agent.enabled = false;
        }
        else
        {
            //Включаем агент
            m_Agent.enabled = true;
        }
        //Двигаем
        _rb.velocity = new Vector3(_joystick.Horizontal * _Speed , _rb.velocity.y , _joystick.Vertical * _Speed);
       
    }
}
