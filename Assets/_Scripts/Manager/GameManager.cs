using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public CubeManager cubeManager;
    public ScoreManager scoreManager;
    public SlotUIManager slotUIManager;

    public Slider gameTimerSlider;  
    public float gameTimeLimit = 300f; 

    public TextMeshProUGUI scoreText;
    private bool gameOver = false;
    private float remainingTime;

    private int currentSlotIndex = 0;
    private int totalScore = 0;



    private void Start()
    {
        Invoke("Initialize", 0.2f); 
        remainingTime = gameTimeLimit;
    }
    void Update()
    {
        if (!gameOver)
        {

            remainingTime -= Time.deltaTime;
            UpdateGameTimer();

            if (remainingTime <= 0)
            {
                GameOver();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSlot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SubmitCurrentSlot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCubes();
        }
    }

    void Initialize()
    {
        currentSlotIndex = 0;
        UpdateSlotSelection();
    }


    void UpdateGameTimer()
    {
        gameTimerSlider.value = remainingTime / gameTimeLimit;

        Image fill = gameTimerSlider.fillRect.GetComponent<Image>();
        float ratio = remainingTime / gameTimeLimit;

        if (ratio > 0.5f)
        {
            fill.color = Color.aquamarine;
        }
        else if (ratio > 0.25f)
        {
            fill.color = Color.yellow;
        }
        else
        {
            fill.color = Color.red;

        }
    }

    void GameOver()
    {
        gameOver = true;
        Debug.Log($"=== GAME OVER ===");
        Debug.Log($"Final Score: {totalScore}");
        // TODO: 게임 오버 화면
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

    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreText.text = $"Score: {totalScore}";
    }

    public void SubtractScore(int penalty)
    {
        totalScore -= penalty;
        scoreText.text = $"Score: {totalScore}";
    }
    public void InitializeSelection()
    {
        currentSlotIndex = 0;
        UpdateSlotSelection();
    }

    void ResetCubes()
    {
        cubeManager.ResetCubes();
    }
}
