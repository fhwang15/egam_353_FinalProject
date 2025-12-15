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
        currentSlotIndex = 0;
        UpdateSlotSelection();
        Debug.Log("Game started - Slot 1 selected");
    }

    void ChangeSlot()
    {
        int startIndex = currentSlotIndex;
        do
        {
            currentSlotIndex = (currentSlotIndex + 1) % 3;

            SlotUI slot = slotUIManager.GetSlot(currentSlotIndex);
            if (slot != null && slot.IsActive())
            {
                break;
            }

        } while (currentSlotIndex != startIndex);

        UpdateSlotSelection();
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

        Recipe recipe = currentSlot.GetRecipe();
        int score = scoreManager.CalculateScore(recipe);

        currentSlot.Submit(score);

        AddScore(score);

        Debug.Log($"Submitted! Score: {score}, Total: {totalScore}");
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log($"Total Score: {totalScore}");
        // TODO: UI¿¡ ÃÑÁ¡ Ç¥½Ã
    }

    public void SubtractScore(int penalty)
    {
        totalScore -= penalty;
        Debug.Log($"Penalty! Total Score: {totalScore}");
        // TODO: UI¿¡ ÃÑÁ¡ Ç¥½Ã
    }
    public void InitializeSelection()
    {
        currentSlotIndex = 0;
        UpdateSlotSelection();
        Debug.Log("First slot selected!");
    }
}
