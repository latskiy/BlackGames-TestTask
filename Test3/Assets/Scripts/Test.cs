using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Test : MonoBehaviour, IPointerClickHandler
{
    // ������
    private float timePressed = 0f;
    //�����
    public Text redText;
    public Text greenText;
    public Text blueText;
    //������
    public Button randomButton;
    public Button increaseButton;
    public Button decreaseButton;
    //�������
    public Slider greenSlider;
    //���������� ��������
    private Renderer objectRenderer;
    private Graphic panelRenderer;
    //���������� ����
    int pTag;
    void Start()
    {
        //������ ��������� ��������
        objectRenderer = null;
        panelRenderer = null;
        redText.text = "0";
        greenText.text = "0";
        blueText.text = "0";
        // ��������� ���������
        increaseButton.onClick.AddListener(IncreaseRed);
        decreaseButton.onClick.AddListener(DecreaseRed);
        greenSlider.onValueChanged.AddListener(UpdateGreen);
        randomButton.onClick.AddListener(RandomizeBlue);
    }
    

    //������������ �������
    public void OnPointerClick(PointerEventData eventData)
    {

        //Debug.Log("Button is clicked");
        GameObject clickedObjectP = eventData.pointerCurrentRaycast.gameObject;
        //��������� ���� �� ��� UI
        if (clickedObjectP.CompareTag("PTag"))
        {
            pTag= 1;
      
            //�������� ���������
            var graphic = eventData.pointerCurrentRaycast.gameObject.GetComponent<UnityEngine.UI.Graphic>();
            //��������� ����������� �� ��
            if (graphic != null)
            {
                // ������ ������
                panelRenderer = clickedObjectP.GetComponent<Graphic>();
                // ���������� ���� � ��������� ����
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

            // �������� ������, �� ������� ������
            GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
            
            if (clickedObject == null)
            {
                // ������ �� ������
                return;
            }

            // �������� �������� �������, �� ������� ������
            objectRenderer = clickedObject.GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                // �������� �� ������
                return;
            }

            // �������� ���� ��������� �������
            Color objectColor = objectRenderer.material.color;

            // ���������� ���� � ��������� ����
            redText.text = Mathf.RoundToInt(objectColor.r * 255f).ToString();
            greenText.text = Mathf.RoundToInt(objectColor.g * 255f).ToString();
            blueText.text = Mathf.RoundToInt(objectColor.b * 255f).ToString();
            float green = float.Parse(greenText.text) / 255f;
            greenSlider.value = green;
        }
    }

    //���������� �������� 
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

    //���������� ��������
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

    //���������� ��������� ��������
    public void UpdateGreen(float value)
    {
        int green = Mathf.RoundToInt(value * 255f);
        greenText.text = green.ToString();
        greenSlider.value = value;
        UpdateColor();
    }

    //������ ����
    private void UpdateColor()
    {
        //���� ��� 3d
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
        //���� ��� UI
        else
        {
            
            float red = int.Parse(redText.text) / 255f;
            float green = int.Parse(greenText.text) / 255f;
            float blue = int.Parse(blueText.text) / 255f;

            Color newColorP = new Color(red, green, blue);
            panelRenderer.color= newColorP;
        }
    }

    //������������ ������
    public void RandomizeBlue()
    {
        int blue = UnityEngine.Random.Range(0, 256);
        blueText.text = blue.ToString();
        UpdateColor();
    }

    // ������� + � -
    public void Update()
    {
        // ��������� �������
        if (Input.GetMouseButton(0))
        {
            if (increaseButton == null) return;
            // ��������� ��� ������ ������ +
            if (increaseButton.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                timePressed += Time.deltaTime; // ��������� �����, ��������� � ������� ������� ������
                if (timePressed >= 0.2f) // ���� ������ ���� ������ 0.2 ������� ��� �����
                {
                    //��������� ���������
                    //Debug.Log("0.2 seconds");
                    IncreaseRed();
                }
            }
            if (decreaseButton == null) return;
            // ��������� ��� ������ ������ -
            if (decreaseButton.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                timePressed += Time.deltaTime; // ��������� �����, ��������� � ������� ������� ������
                if (timePressed >= 0.2f) // ���� ������ ���� ������ 0.2 ������� ��� �����
                {
                    //��������� ���������
                    //Debug.Log("0.2 seconds");
                    DecreaseRed();
                }
            }
        }
        else
        {
            timePressed = 0f; // �������� �����, ���� ������ �� ������
        }
    }

}
