using UnityEngine;
using UnityEngine.InputSystem;

public class Check : MonoBehaviour
{
    [SerializeField] private QuestionManager qman; // �ν����Ϳ��� �Ҵ� ����
    private string _currentTouchTag = null;        // ���� ����ִ� �±� ����

    private void Awake()
    {
        if (qman == null)
            qman = GetComponent<QuestionManager>(); // ���� ������Ʈ�� ������ �ڵ� ã��
    }

    // ����ִ� Trigger�� �±׸� ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        _currentTouchTag = other.tag;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _currentTouchTag = other.tag;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ������ �±� �ʱ�ȭ(��ħ ��Ȳ ����� null��)
        _currentTouchTag = null;
    }

    private void Update()
    {
        // ����: QŰ�� ���� ����
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            CheckByTag();
        }
    }

    public void CheckByTag()
    {
        if (qman == null) return;

        switch (_currentTouchTag)
        {
            case "O":
                qman.OtextOn();
                break;
            case "X":
                qman.XtextOn();
                break;
            case "Sai":
                break;
            default:
                break;
        }
    }
}
