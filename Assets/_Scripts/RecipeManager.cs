using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    public List<Recipe> recipes;
    public List<RecipeSlot> slots;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateRecipes();
        DisplayRecipes();
    }

    void CreateRecipes()
    {
        // L Shape
        recipes[0] = new Recipe("L Shape", 30f);
        recipes[0].targetShape[0, 0, 0] = true;
        recipes[0].targetShape[0, 0, 1] = true;
        recipes[0].targetShape[0, 0, 2] = true;
        recipes[0].targetShape[1, 0, 0] = true;

        // T Shape
        recipes[1] = new Recipe("T Shape", 45f);
        recipes[1].targetShape[0, 0, 1] = true;
        recipes[1].targetShape[1, 0, 1] = true;
        recipes[1].targetShape[2, 0, 1] = true;
        recipes[1].targetShape[1, 0, 0] = true;

        // Stairs Shape
        recipes[2] = new Recipe("Stairs", 60f);
        recipes[2].targetShape[0, 0, 0] = true;
        recipes[2].targetShape[1, 1, 0] = true;
        recipes[2].targetShape[2, 2, 0] = true;
    }

    void DisplayRecipes()  // Ãß°¡!
    {
        for (int i = 0; i < 3; i++)
        {
            slots[i].DisplayRecipe(recipes[i]);
        }
    }

}
