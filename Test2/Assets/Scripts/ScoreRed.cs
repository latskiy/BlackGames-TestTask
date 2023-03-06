using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRed : MonoBehaviour
{
    public string collisionTag; // ��� �������, � ������� ������ ����������� ���
    public string collisionCounterName; // ��� �������� UI Text, � ������� ����� ���������� ���� ������������
    public string DropTagRed;//��� �����
    private int collisionCount = 0;//������� �������
    private Text collisionCounter;//��������� ������ 
    public bool dropRed = false;// ���������� ������ �������
    private void Start()
    {
        // ������� ������� UI Text �� �����
        collisionCounter = GameObject.Find(collisionCounterName).GetComponent<Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            dropRed = false;//�� ������� ���
            Destroy(gameObject); // ������� ���
            collisionCount++; // ����������� ������� ������������
            collisionCounter.text = (int.Parse(collisionCounter.text) + 1).ToString(); // ��������� UI Text � ��������� ������������
        }
        else if (collision.gameObject.CompareTag(DropTagRed))
        {
            dropRed = true;//������� ���
        }
        else
        {
            dropRed = false;//�� ������� ���
        }
    }

}