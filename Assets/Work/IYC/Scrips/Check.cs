using UnityEngine;
using UnityEngine.InputSystem;

public class Check : MonoBehaviour
{
    [SerializeField] private QuestionManager qman; // 인스펙터에서 할당 권장
    private string _currentTouchTag = null;        // 현재 닿아있는 태그 저장

    private void Awake()
    {
        if (qman == null)
            qman = GetComponent<QuestionManager>(); // 같은 오브젝트에 있으면 자동 찾기
    }

    // 닿아있는 Trigger의 태그를 기록
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
        // 나가면 태그 초기화(겹침 상황 고려해 null로)
        _currentTouchTag = null;
    }

    private void Update()
    {
        // 예시: Q키로 판정 실행
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
