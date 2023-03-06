using UnityEngine;
public class CubeMovement : MonoBehaviour
{
    private Rigidbody rb; // компонент Rigidbody для перемещения объекта
    private bool isDragging; // флаг, показывающий, что объект перемещается
    private Vector3 startPos; // начальная позиция объекта
    private Vector3 dragOffset; // расстояние между курсором мыши и объектом
    private ScoreBlue bluecube;// ссылка на синий куб
    private ScoreRed redcube;// ссылка на красный куб
    public float veloc;
    void Start()
    {
        redcube = GetComponent<ScoreRed>();// получаем скрипт красных
        bluecube = GetComponent<ScoreBlue>(); // получаем скрипт синих
        rb = GetComponent<Rigidbody>(); // получаем компонент Rigidbody у текущего объекта
    }

    void OnMouseDown()
    {
        isDragging = true; // устанавливаем флаг, показывающий, что объект перемещается
        startPos = transform.position; // сохраняем начальную позицию объекта
        // вычисляем расстояние между курсором мыши и объектом
        dragOffset = startPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(startPos).z));
    }

    void OnMouseDrag()
    {
        if (bluecube != null)//делаем проверку
        {
            if (bluecube.dropBlue)// если не та стена
            {
                veloc = 3f;//добавляем ускорение
                OnMouseUp();//отпускаем
            }
        }
        if (redcube != null)// делаем проверку
        {
            if (redcube.dropRed)// если не та стена
            {
                veloc = 3f;//добавляем ускорение
                OnMouseUp();//отпускаем
            }
        }
        if (!isDragging) return; // если объект не перемещается, выходим из функции

        RaycastHit hit; // переменная для хранения информации о столкновении луча с объектом
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // создаем луч из позиции курсора мыши
        if (Physics.Raycast(ray, out hit)) // проверяем, пересекает ли луч какой-либо объект
        {
            if (hit.collider.CompareTag("Movable")) // если объект можно перемещать (имеет тег "Movable")
            {
                rb.mass = 0.0001f; // убираем массу
                rb.useGravity = false; // убираем гравитацию
                rb.freezeRotation = true; // заморозка вращения                                                 
                Vector3 newPosition = hit.point + dragOffset; // вычисляем новую позицию объекта
                newPosition.z = startPos.z; // не перемещаем объект по оси Z
                rb.MovePosition(newPosition); // перемещаем объект в новую позицию
            }
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return; // если объект не перемещался, выходим из функции
        rb.mass = 1f; //включаем массу
        rb.useGravity = true; // включаем гравитацию
        rb.freezeRotation = false; // размарозка вращения
        isDragging = false; // сбрасываем флаг, показывающий, что объект перемещается
        Vector3 endPos = transform.position; // сохраняем конечную позицию объекта
        rb.velocity = new Vector3(0f, veloc, 0f);
        transform.position = startPos + (endPos - startPos); // перемещаем объект на расстояние, на которое его сдвинули во время перемещения
    }

}




