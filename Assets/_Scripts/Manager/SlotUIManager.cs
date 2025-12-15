using UnityEngine;
using System.Collections.Generic;

public class SlotUIManager : MonoBehaviour
{
    public GameObject slotUIPrefab;
    public Transform slotContainer;
    public RenderTexture[] renderTextures;  // RecipeRT1, 2, 3
    public RecipeSlot[] recipeSlots;  // FirstSlot, SecondSlot, ThirdSlot
    public GameManager gameManager;

    private SlotUI[] slots = new SlotUI[3];
    private bool[] slotRespawnPending = new bool[3];

    void Start()
    {
        Debug.Log("=== SlotUIManager Start ===");

        if (RecipeManager.Instance == null)
        {
            Debug.LogError("RecipeManager not found!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log($"Creating slot {i}");
            CreateSlotAtIndex(i);
        }

        Invoke("NotifyGameManager", 0.1f);
    }

    void NotifyGameManager()
    {
        if (gameManager != null)
        {
            gameManager.InitializeSelection();
        }
    }

    void CreateSlotAtIndex(int index)
    {
        Recipe recipe = RecipeManager.Instance.GetRandomRecipe();

        Debug.Log($"=== CreateSlotAtIndex {index} START ===");
        Debug.Log($"Recipe: {recipe.recipeName}");
        Debug.Log($"RecipeSlot: {recipeSlots[index]?.name}");
        Debug.Log($"RenderTexture: {renderTextures[index]?.name}");

        if (slots[index] != null)
        {
            Debug.Log($"Destroying existing slot {index}");
            Destroy(slots[index].gameObject);
            slots[index] = null;
        }

        Debug.Log($"Creating slot {index} with recipe: {recipe.recipeName}");

        recipeSlots[index].DisplayRecipe(recipe);

        GameObject slotObj = Instantiate(slotUIPrefab, slotContainer);
        SlotUI slotUI = slotObj.GetComponent<SlotUI>();

        RectTransform rect = slotObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-500 + index * 500, 400);

        slotUI.Initialize(recipe, renderTextures[index], index);

        Debug.Log($"SlotUI initialized with RT: {renderTextures[index]?.name}");

        slotUI.OnSlotExpired = OnSlotExpired;
        slotUI.OnSlotSubmitted = OnSlotSubmitted;

        slots[index] = slotUI;
    }

    void OnSlotExpired(int index)
    {
        Debug.Log($"Slot {index} expired! -10 points");
        gameManager.SubtractScore(10);

        slotRespawnPending[index] = true;
        Invoke($"RespawnSlot{index}", 5f);
    }

    void OnSlotSubmitted(int index, int score)
    {
        Debug.Log($"Slot {index} submitted! +{score} points");

        slotRespawnPending[index] = true;
        Invoke($"RespawnSlot{index}", 0.5f);
    }

    void RespawnSlot0() { RespawnSlot(0); }
    void RespawnSlot1() { RespawnSlot(1); }
    void RespawnSlot2() { RespawnSlot(2); }

    void RespawnSlot(int index)
    {
        if (!slotRespawnPending[index]) return;

        slotRespawnPending[index] = false;

        if (slots[index] != null)
        {
            Destroy(slots[index].gameObject);
        }

        CreateSlotAtIndex(index);
        Debug.Log($"New recipe spawned at slot {index}");
    }

    public SlotUI GetSlot(int index)
    {
        if (index >= 0 && index < 3)
            return slots[index];
        return null;
    }

}