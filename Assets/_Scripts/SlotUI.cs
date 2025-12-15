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

    public Image timerCircle;

    [HideInInspector]
    public int slotIndex;

    private Recipe recipe;
    private float remainingTime;
    private bool isActive = false;

    private float maxTime;


    public Action<int> OnSlotExpired;
    public Action<int, int> OnSlotSubmitted;


    void Update()
    {
        if (isActive && !isSubmitted)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
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
        recipeDisplay.texture = renderTexture; 

        recipeName.text = recipe.recipeName;
        remainingTime = recipe.timeLimit;
        maxTime = recipe.timeLimit;
        isActive = true;
        isSubmitted = false;

        UpdateTimerDisplay();
        UpdateVisuals();
    }



    public void Submit(int score)
    {
        if (isSubmitted) return;

        isSubmitted = true;

        timerCircle.fillAmount = 0;
        if (timerText != null)
        {
            timerText.text = $"Score: {score}";

        }
        UpdateVisuals();

        OnSlotSubmitted?.Invoke(slotIndex, score);
        Invoke("DestroySelf", 1f);
    }

    void SlotExpired()
    {
        isActive = false;
        timerText.text = "EXPIRED!";
        background.color = Color.red;

        OnSlotExpired?.Invoke(slotIndex);

    
        Invoke("DestroySelf", 2f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void UpdateTimerDisplay()
    {

        timerCircle.fillAmount = remainingTime / maxTime;


        float ratio = remainingTime / maxTime;
        if (ratio > 0.5f)
        {
            timerCircle.color = Color.green;

        }
        else if (ratio > 0.25f)
        {
            timerCircle.color = Color.yellow;

        }
        else
        {
            timerCircle.color = Color.red;

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
            background.color = Color.gray;
        }
        else if (isSelected)
        {
            background.color = Color.yellow;
        }
        else
        {
            background.color = Color.white; 
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
