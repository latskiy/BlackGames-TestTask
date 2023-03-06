using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnFinal : MonoBehaviour
{
    public GameObject blueCubePrefab; // префаб голубого куба
    public GameObject redCubePrefab; // префаб красного куба
    public int totalCubeCount; // общее количество кубов, которые будут созданы
    public float spawnDelay; // задержка между появлениями кубов
    public string objectTag; // тег объекта, который будет использован для проверки наличия объектов в сцене
    public GameObject objectToActivate; // объект, который нужно активировать, если объектов с заданным тегом нет в сцене
    public string RedScore; // имя объекта, содержащего текст для отображения количества очков красной команды
    private Text RedScoreText; // компонент Text, связанный с объектом для отображения количества очков красной команды
    public string BlueScore; // имя объекта, содержащего текст для отображения количества очков голубой команды
    private Text BlueScoreText; // компонент Text, связанный с объектом для отображения количества очков голубой команды

    private void Start()
    {
        RedScoreText = GameObject.Find(RedScore).GetComponent<Text>(); // находим объект Text для отображения количества очков красной команды
        BlueScoreText = GameObject.Find(BlueScore).GetComponent<Text>(); // находим объект Text для отображения количества очков голубой команды
        StartCoroutine(SpawnCubes()); // запускаем корутину для создания кубов с заданными параметрами
    }

    private IEnumerator SpawnCubes()
    {
        for (int i = 0; i < totalCubeCount; i++) // создаем заданное количество кубов
        {
            GameObject cubePrefab = (Random.Range(0, 2) == 0) ? blueCubePrefab : redCubePrefab; // выбираем случайным образом префаб для создания куба

            Vector3 spawnPosition = GetSpawnPosition(); // получаем позицию, где нужно создать куб

            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity); // создаем куб с выбранным префабом и заданной позицией

            yield return new WaitForSeconds(spawnDelay); // ждем заданную задержку перед созданием следующего куба
        }
    }

    private void Update()
    {
        // Получаем все объекты с тегом "objectTag"
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(objectTag);// оптимизация в минус, надо придумать что то другое

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
        // Получаем координаты текущего объекта
        float rX = transform.position.x;
        float rY = transform.position.y;
        float rZ = transform.position.z;

        // Создаем новую позицию для спавна куба используя координаты текущего объекта
        Vector3 spawnPosition = new Vector3(rX, rY, rZ);
        return spawnPosition;
    }

    public void RestartSpawnAndDeactivate()
    {
        // Деактивируем "objectToActivate", сбрасываем счетчики очков и начинаем спавн кубов заново
        objectToActivate.SetActive(false);
        RedScoreText.text = "0";
        BlueScoreText.text = "0";
        StartCoroutine(SpawnCubes());
    }

}
