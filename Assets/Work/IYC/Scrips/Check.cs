using UnityEngine;
using UnityEngine.InputSystem;

public class Check : MonoBehaviour
{
    [SerializeField] private QuestionManager qman; // 인스펙터에서 할당 권장
    private string _currentTouchTag = null;        // 현재 닿아있는 태그

    private void Awake()
    {
        if (qman == null)
            qman = FindObjectOfType<QuestionManager>(); // 같은 오브젝트가 아니면 씬에서 찾아오기
    }

    // Player 쪽 콜라이더는 isTrigger 체크(또는 O/X/Sai 쪽이 트리거)
    private void OnTriggerEnter2D(Collider2D other) { _currentTouchTag = other.tag; }
    private void OnTriggerStay2D(Collider2D other) { _currentTouchTag = other.tag; }
    private void OnTriggerExit2D(Collider2D other) { _currentTouchTag = null; }

    // UI 버튼 OnClick에 이 함수 연결
    public void CheckByTag()
    {
        if (qman == null || string.IsNullOrEmpty(_currentTouchTag)) return;

        switch (_currentTouchTag)
        {
            case "O": qman.ChooseO(); break;
            case "X": qman.ChooseX(); break;
            case "Sai": qman.Skip(); break; // 점수/연출 없이 다음
        }
    }

    // 키보드 테스트용(원치 않으면 삭제)
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            CheckByTag();
    }
}
