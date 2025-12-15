using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SlotUI : MonoBehaviour
{
    public RawImage recipeDisplay;
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI timerText;
    public Image background;
    private bool isSelected = false;
    private bool isSubmitted = false;

    [HideInInspector]
    public int slotIndex;

    private Recipe recipe;
    private float remainingTime;
    private bool isActive = false;


    public Action<int> OnSlotExpired;
    public Action<int, int> OnSlotSubmitted;


    void Update()
    {
        if (isActive && !isSubmitted)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                // 시간 초과!
                SlotExpired();
            }
            else
            {
                UpdateTimerDisplay();
            }
        }
    }

    public void Initialize(Recipe recipeData, RenderTexture renderTexture, int index)
    {
        recipe = recipeData;
        slotIndex = index;
        recipeDisplay.texture = renderTexture;  // ← 이게 제대로 실행되는지!

        Debug.Log($"SlotUI {index}: Setting texture to {renderTexture?.name}");  // 디버그 추가!

        recipeName.text = recipe.recipeName;
        remainingTime = recipe.timeLimit;
        isActive = true;
        isSubmitted = false;

        UpdateTimerDisplay();
        UpdateVisuals();
    }

    public void Submit(int score)
    {
        if (isSubmitted) return;

        isSubmitted = true;
        timerText.text = $"Score: {score}";
        UpdateVisuals();

        OnSlotSubmitted?.Invoke(slotIndex, score);
    }

    void SlotExpired()
    {
        isActive = false;
        timerText.text = "EXPIRED!";
        background.color = Color.red;

        OnSlotExpired?.Invoke(slotIndex);

        // 5초 후 삭제
        Invoke("DestroySelf", 5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(remainingTime);
        timerText.text = $"{seconds}s";

        // 시간 적으면 색깔 변경
        if (remainingTime <= 10f)
        {
            timerText.color = Color.red;
        }
        else if (remainingTime <= 20f)
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (isSubmitted)
        {
            background.color = new Color(0.3f, 0.3f, 0.3f);  // 회색
        }
        else if (isSelected)
        {
            background.color = Color.yellow;  // 노랑
        }
        else
        {
            background.color = Color.white;  // 기본
        }
    }

    public bool IsActive()
    {
        return isActive && !isSubmitted;
    }

    public Recipe GetRecipe()
    {
        return recipe;
    }
}
