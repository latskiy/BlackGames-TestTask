using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBlue : MonoBehaviour
{
    public string collisionTag; // ��� �������, � ������� ������ ����������� ���
    public string DropTagBlue;//��� �������
    public string collisionCounterName; // ��� �������� UI Text, � ������� ����� ���������� ���� ������������
    private int collisionCount = 0;//������� �����
    private Text collisionCounter;//��������� ������
    public bool dropBlue = false;// ���������� ������ �������
    private void Start()
    {
        // ������� ������� UI Text �� �����
        collisionCounter = GameObject.Find(collisionCounterName).GetComponent<Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            dropBlue = false;//�� ������� ���
            Destroy(gameObject); // ������� ���
            collisionCount++; // ����������� ������� ������������
            collisionCounter.text = (int.Parse(collisionCounter.text) + 1).ToString(); // ��������� UI Text � ��������� ������������
        }
        else if (collision.gameObject.CompareTag(DropTagBlue))
        {
            dropBlue = true;//������� ���
        }
        else
        {
            dropBlue = false;//�� ������� ���
        }
      
    }
}