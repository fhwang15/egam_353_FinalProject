using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public CubeManager cubeManager;
    public ScoreManager scoreManager;
    public SlotUIManager slotUIManager;

    private int currentSlotIndex = 0;
    private int totalScore = 0;


    private void Start()
    {
        Invoke("Initialize", 0.2f); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSlot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SubmitCurrentSlot();
        }
    }

    void Initialize()
    {
        currentSlotIndex = 0;  // 첫 번째 슬롯
        UpdateSlotSelection();
        Debug.Log("Game started - Slot 1 selected");
    }

    void ChangeSlot()
    {
        // 다음 활성 슬롯 찾기
        int startIndex = currentSlotIndex;
        do
        {
            currentSlotIndex = (currentSlotIndex + 1) % 3;

            SlotUI slot = slotUIManager.GetSlot(currentSlotIndex);
            if (slot != null && slot.IsActive())
            {
                break;  // 활성 슬롯 찾음
            }

        } while (currentSlotIndex != startIndex);

        UpdateSlotSelection();
        Debug.Log($"Selected Slot: {currentSlotIndex + 1}");
    }

    void UpdateSlotSelection()
    {
        for (int i = 0; i < 3; i++)
        {
            SlotUI slot = slotUIManager.GetSlot(i);
            if (slot != null)
            {
                slot.SetSelected(i == currentSlotIndex);
            }
        }
    }

    void SubmitCurrentSlot()
    {
        SlotUI currentSlot = slotUIManager.GetSlot(currentSlotIndex);

        if (currentSlot == null || !currentSlot.IsActive())
        {
            Debug.Log("No active slot selected!");
            return;
        }

        // 점수 계산
        Recipe recipe = currentSlot.GetRecipe();
        int score = scoreManager.CalculateScore(recipe);

        // 슬롯에 제출 (자동으로 OnSlotSubmitted 콜백 호출됨)
        currentSlot.Submit(score);

        // 총점 추가
        AddScore(score);

        Debug.Log($"Submitted! Score: {score}, Total: {totalScore}");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"Total Score: {totalScore}");
        // TODO: UI에 총점 표시
    }

    public void SubtractScore(int penalty)
    {
        totalScore -= penalty;
        Debug.Log($"Penalty! Total Score: {totalScore}");
        // TODO: UI에 총점 표시
    }
    public void InitializeSelection()
    {
        currentSlotIndex = 0;
        UpdateSlotSelection();
        Debug.Log("First slot selected!");
    }
}
