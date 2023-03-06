using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Test : MonoBehaviour, IPointerClickHandler
{
    // Таймер
    private float timePressed = 0f;
    //Текст
    public Text redText;
    public Text greenText;
    public Text blueText;
    //Кнопки
    public Button randomButton;
    public Button increaseButton;
    public Button decreaseButton;
    //Слайдер
    public Slider greenSlider;
    //Переменные обьектов
    private Renderer objectRenderer;
    private Graphic panelRenderer;
    //Переменная слоя
    int pTag;
    void Start()
    {
        //Задаем стартовые значения
        objectRenderer = null;
        panelRenderer = null;
        redText.text = "0";
        greenText.text = "0";
        blueText.text = "0";
        // Запускаем прослушку
        increaseButton.onClick.AddListener(IncreaseRed);
        decreaseButton.onClick.AddListener(DecreaseRed);
        greenSlider.onValueChanged.AddListener(UpdateGreen);
        randomButton.onClick.AddListener(RandomizeBlue);
    }
    

    //Обрабатываем нажатия
    public void OnPointerClick(PointerEventData eventData)
    {

        //Debug.Log("Button is clicked");
        GameObject clickedObjectP = eventData.pointerCurrentRaycast.gameObject;
        //Проверяем есть ли тэг UI
        if (clickedObjectP.CompareTag("PTag"))
        {
            pTag= 1;
      
            //Получаем компонент
            var graphic = eventData.pointerCurrentRaycast.gameObject.GetComponent<UnityEngine.UI.Graphic>();
            //Проверяем графический ли он
            if (graphic != null)
            {
                // Делаем ссылку
                panelRenderer = clickedObjectP.GetComponent<Graphic>();
                // Записываем цвет в текстовые поля
                Color panelColor = graphic.color;
                redText.text = Mathf.RoundToInt(panelColor.r * 255f).ToString();
                greenText.text = Mathf.RoundToInt(panelColor.g * 255f).ToString();
                blueText.text = Mathf.RoundToInt(panelColor.b * 255f).ToString();
                float greenP = float.Parse(greenText.text) / 255f;
                greenSlider.value = greenP;
            }

        }
        else
        {
            pTag= 0;

            // Получаем объект, на который нажали
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            
            if (clickedObject == null)
            {
                // Объект не найден
                return;
            }

            // Получаем рендерер объекта, на который нажали
            objectRenderer = clickedObject.GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                // Рендерер не найден
                return;
            }

            // Получаем цвет материала объекта
            Color objectColor = objectRenderer.material.color;

            // Записываем цвет в текстовые поля
            redText.text = Mathf.RoundToInt(objectColor.r * 255f).ToString();
            greenText.text = Mathf.RoundToInt(objectColor.g * 255f).ToString();
            blueText.text = Mathf.RoundToInt(objectColor.b * 255f).ToString();
            float green = float.Parse(greenText.text) / 255f;
            greenSlider.value = green;
        }
    }

    //Увеличение красного 
    public void IncreaseRed()
    {
        int red = int.Parse(redText.text);
        red++;
        if (red > 255)
        {
            red = 255;
        }
        redText.text = red.ToString();
        UpdateColor();
    }

    //Уменьшение красного
    public void DecreaseRed()
    {
        int red = int.Parse(redText.text);
        red--;
        if (red < 0)
        {
            red = 0;
        }
        redText.text = red.ToString();
        UpdateColor();
    }

    //Обновление положения слайдера
    public void UpdateGreen(float value)
    {
        int green = Mathf.RoundToInt(value * 255f);
        greenText.text = green.ToString();
        greenSlider.value = value;
        UpdateColor();
    }

    //Задаем цвет
    private void UpdateColor()
    {
        //Если это 3d
        if (pTag == 0)
        {
            
            float red = int.Parse(redText.text) / 255f;
            float green = int.Parse(greenText.text) / 255f;
            float blue = int.Parse(blueText.text) / 255f;

            Color newColor = new Color(red, green, blue);

            if (objectRenderer != null)
            {
                objectRenderer.material.color = newColor;
            }
        }
        //Если это UI
        else
        {
            
            float red = int.Parse(redText.text) / 255f;
            float green = int.Parse(greenText.text) / 255f;
            float blue = int.Parse(blueText.text) / 255f;

            Color newColorP = new Color(red, green, blue);
            panelRenderer.color= newColorP;
        }
    }

    //Рандомизация синего
    public void RandomizeBlue()
    {
        int blue = UnityEngine.Random.Range(0, 256);
        blueText.text = blue.ToString();
        UpdateColor();
    }

    // Зажатие + и -
    public void Update()
    {
        // Проверяем нажатие
        if (Input.GetMouseButton(0))
        {
            if (increaseButton == null) return;
            // Проверяем что нажата кнопка +
            if (increaseButton.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                timePressed += Time.deltaTime; // добавляем время, прошедшее с момента зажатия кнопки
                if (timePressed >= 0.2f) // если кнопка была зажата 0.2 секунды или более
                {
                    //Запускаем перемотку
                    //Debug.Log("0.2 seconds");
                    IncreaseRed();
                }
            }
            if (decreaseButton == null) return;
            // Проверяем что нажата кнопка -
            if (decreaseButton.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                timePressed += Time.deltaTime; // добавляем время, прошедшее с момента зажатия кнопки
                if (timePressed >= 0.2f) // если кнопка была зажата 0.2 секунды или более
                {
                    //Запускаем перемотку
                    //Debug.Log("0.2 seconds");
                    DecreaseRed();
                }
            }
        }
        else
        {
            timePressed = 0f; // обнуляем время, если кнопка не зажата
        }
    }

}
