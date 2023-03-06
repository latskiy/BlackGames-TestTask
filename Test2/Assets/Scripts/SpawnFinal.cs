using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnFinal : MonoBehaviour
{
    public GameObject blueCubePrefab; // ������ �������� ����
    public GameObject redCubePrefab; // ������ �������� ����
    public int totalCubeCount; // ����� ���������� �����, ������� ����� �������
    public float spawnDelay; // �������� ����� ����������� �����
    public string objectTag; // ��� �������, ������� ����� ����������� ��� �������� ������� �������� � �����
    public GameObject objectToActivate; // ������, ������� ����� ������������, ���� �������� � �������� ����� ��� � �����
    public string RedScore; // ��� �������, ����������� ����� ��� ����������� ���������� ����� ������� �������
    private Text RedScoreText; // ��������� Text, ��������� � �������� ��� ����������� ���������� ����� ������� �������
    public string BlueScore; // ��� �������, ����������� ����� ��� ����������� ���������� ����� ������� �������
    private Text BlueScoreText; // ��������� Text, ��������� � �������� ��� ����������� ���������� ����� ������� �������

    private void Start()
    {
        RedScoreText = GameObject.Find(RedScore).GetComponent<Text>(); // ������� ������ Text ��� ����������� ���������� ����� ������� �������
        BlueScoreText = GameObject.Find(BlueScore).GetComponent<Text>(); // ������� ������ Text ��� ����������� ���������� ����� ������� �������
        StartCoroutine(SpawnCubes()); // ��������� �������� ��� �������� ����� � ��������� �����������
    }

    private IEnumerator SpawnCubes()
    {
        for (int i = 0; i < totalCubeCount; i++) // ������� �������� ���������� �����
        {
            GameObject cubePrefab = (Random.Range(0, 2) == 0) ? blueCubePrefab : redCubePrefab; // �������� ��������� ������� ������ ��� �������� ����

            Vector3 spawnPosition = GetSpawnPosition(); // �������� �������, ��� ����� ������� ���

            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity); // ������� ��� � ��������� �������� � �������� ��������

            yield return new WaitForSeconds(spawnDelay); // ���� �������� �������� ����� ��������� ���������� ����
        }
    }

    private void Update()
    {
        // �������� ��� ������� � ����� "objectTag"
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(objectTag);// ����������� � �����, ���� ��������� ��� �� ������

        if (objectsWithTag.Length == 0)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            objectToActivate.SetActive(false);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        // �������� ���������� �������� �������
        float rX = transform.position.x;
        float rY = transform.position.y;
        float rZ = transform.position.z;

        // ������� ����� ������� ��� ������ ���� ��������� ���������� �������� �������
        Vector3 spawnPosition = new Vector3(rX, rY, rZ);
        return spawnPosition;
    }

    public void RestartSpawnAndDeactivate()
    {
        // ������������ "objectToActivate", ���������� �������� ����� � �������� ����� ����� ������
        objectToActivate.SetActive(false);
        RedScoreText.text = "0";
        BlueScoreText.text = "0";
        StartCoroutine(SpawnCubes());
    }

}
