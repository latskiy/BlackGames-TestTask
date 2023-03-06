using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreRed : MonoBehaviour
{
    public string collisionTag; // Тег объекта, с которым должен столкнуться куб
    public string collisionCounterName; // Имя элемента UI Text, в который нужно записывать счет столкновений
    public string DropTagRed;//Тэг синих
    private int collisionCount = 0;//Счетчик красных
    private Text collisionCounter;//Компонент текста 
    public bool dropRed = false;// Переменная сброса обьекта
    private void Start()
    {
        // Находим элемент UI Text по имени
        collisionCounter = GameObject.Find(collisionCounterName).GetComponent<Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            dropRed = false;//не бросаем куб
            Destroy(gameObject); // Удаляем куб
            collisionCount++; // Увеличиваем счетчик столкновений
            collisionCounter.text = (int.Parse(collisionCounter.text) + 1).ToString(); // Обновляем UI Text с счетчиком столкновений
        }
        else if (collision.gameObject.CompareTag(DropTagRed))
        {
            dropRed = true;//бросаем куб
        }
        else
        {
            dropRed = false;//не бросаем куб
        }
    }

}