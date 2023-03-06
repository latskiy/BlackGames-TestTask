using UnityEngine;
public class CubeMovement : MonoBehaviour
{
    private Rigidbody rb; // ��������� Rigidbody ��� ����������� �������
    private bool isDragging; // ����, ������������, ��� ������ ������������
    private Vector3 startPos; // ��������� ������� �������
    private Vector3 dragOffset; // ���������� ����� �������� ���� � ��������
    private ScoreBlue bluecube;// ������ �� ����� ���
    private ScoreRed redcube;// ������ �� ������� ���
    public float veloc;
    void Start()
    {
        redcube = GetComponent<ScoreRed>();// �������� ������ �������
        bluecube = GetComponent<ScoreBlue>(); // �������� ������ �����
        rb = GetComponent<Rigidbody>(); // �������� ��������� Rigidbody � �������� �������
    }

    void OnMouseDown()
    {
        isDragging = true; // ������������� ����, ������������, ��� ������ ������������
        startPos = transform.position; // ��������� ��������� ������� �������
        // ��������� ���������� ����� �������� ���� � ��������
        dragOffset = startPos - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(startPos).z));
    }

    void OnMouseDrag()
    {
        if (bluecube != null)//������ ��������
        {
            if (bluecube.dropBlue)// ���� �� �� �����
            {
                veloc = 3f;//��������� ���������
                OnMouseUp();//���������
            }
        }
        if (redcube != null)// ������ ��������
        {
            if (redcube.dropRed)// ���� �� �� �����
            {
                veloc = 3f;//��������� ���������
                OnMouseUp();//���������
            }
        }
        if (!isDragging) return; // ���� ������ �� ������������, ������� �� �������

        RaycastHit hit; // ���������� ��� �������� ���������� � ������������ ���� � ��������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������� ������� ����
        if (Physics.Raycast(ray, out hit)) // ���������, ���������� �� ��� �����-���� ������
        {
            if (hit.collider.CompareTag("Movable")) // ���� ������ ����� ���������� (����� ��� "Movable")
            {
                rb.mass = 0.0001f; // ������� �����
                rb.useGravity = false; // ������� ����������
                rb.freezeRotation = true; // ��������� ��������                                                 
                Vector3 newPosition = hit.point + dragOffset; // ��������� ����� ������� �������
                newPosition.z = startPos.z; // �� ���������� ������ �� ��� Z
                rb.MovePosition(newPosition); // ���������� ������ � ����� �������
            }
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return; // ���� ������ �� �����������, ������� �� �������
        rb.mass = 1f; //�������� �����
        rb.useGravity = true; // �������� ����������
        rb.freezeRotation = false; // ���������� ��������
        isDragging = false; // ���������� ����, ������������, ��� ������ ������������
        Vector3 endPos = transform.position; // ��������� �������� ������� �������
        rb.velocity = new Vector3(0f, veloc, 0f);
        transform.position = startPos + (endPos - startPos); // ���������� ������ �� ����������, �� ������� ��� �������� �� ����� �����������
    }

}




