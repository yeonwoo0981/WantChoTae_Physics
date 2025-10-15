using UnityEngine;
using UnityEngine.InputSystem;

public class Check : MonoBehaviour
{
    [SerializeField] private QuestionManager qman; // �ν����Ϳ��� �Ҵ� ����
    private string _currentTouchTag = null;        // ���� ����ִ� �±�

    private void Awake()
    {
        if (qman == null)
            qman = FindObjectOfType<QuestionManager>(); // ���� ������Ʈ�� �ƴϸ� ������ ã�ƿ���
    }

    // Player �� �ݶ��̴��� isTrigger üũ(�Ǵ� O/X/Sai ���� Ʈ����)
    private void OnTriggerEnter2D(Collider2D other) { _currentTouchTag = other.tag; }
    private void OnTriggerStay2D(Collider2D other) { _currentTouchTag = other.tag; }
    private void OnTriggerExit2D(Collider2D other) { _currentTouchTag = null; }

    // UI ��ư OnClick�� �� �Լ� ����
    public void CheckByTag()
    {
        if (qman == null || string.IsNullOrEmpty(_currentTouchTag)) return;

        switch (_currentTouchTag)
        {
            case "O": qman.ChooseO(); break;
            case "X": qman.ChooseX(); break;
            case "Sai": qman.Skip(); break; // ����/���� ���� ����
        }
    }

    // Ű���� �׽�Ʈ��(��ġ ������ ����)
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            CheckByTag();
    }
}
