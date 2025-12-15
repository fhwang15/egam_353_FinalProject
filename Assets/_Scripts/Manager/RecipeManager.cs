using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;  // ΩÃ±€≈Ê

    public Recipe[] recipes = new Recipe[3];
    public RecipeSlot[] slots = new RecipeSlot[3];

    void Awake()  // Start ¥ÎΩ≈ Awake!
    {
        Instance = this;
        CreateRecipes();
        DisplayRecipes();
    }
    void CreateRecipes()
    {
        // Center Pillar 30 sec
        recipes[0] = new Recipe("Center Pillar", 30f);
        for (int y = 0; y < 4; y++)
        {
            recipes[0].targetShape[1, y, 1] = true;
            recipes[0].targetShape[1, y, 2] = true;
            recipes[0].targetShape[2, y, 1] = true;
            recipes[0].targetShape[2, y, 2] = true;
        }
        recipes[0].CalculateLayerCounts();

        // Diagonal Stairs 45 sec
        recipes[1] = new Recipe("Diagonal Stairs", 45f);
        // nothing on top floor
        recipes[1].targetShape[0, 0, 0] = true;
        recipes[1].targetShape[0, 0, 1] = true;
        recipes[1].targetShape[1, 0, 0] = true;
        // top-1 floor
        recipes[1].targetShape[1, 1, 1] = true;
        recipes[1].targetShape[1, 1, 2] = true;
        recipes[1].targetShape[2, 1, 1] = true;
        // top-2 floor
        recipes[1].targetShape[2, 2, 2] = true;
        recipes[1].targetShape[2, 2, 3] = true;
        recipes[1].targetShape[3, 2, 2] = true;
        // Bottom floor
        recipes[1].targetShape[3, 3, 3] = true;
        recipes[1].CalculateLayerCounts();

        // Pyramid 60 sec
        recipes[2] = new Recipe("Pyramid", 60f);

        // top
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {
                recipes[2].targetShape[x, 0, z] = true;
            }

        }

        // top -1 
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                recipes[2].targetShape[x, 1, z] = true;

            }

        }

        // top - 2
        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 2; z++)
            {

                recipes[2].targetShape[x, 2, z] = true;
            }


        }
        // botom
        recipes[2].targetShape[0, 3, 0] = true;
        recipes[2].CalculateLayerCounts();
    }

    void DisplayRecipes()
    {
        for (int i = 0; i < 3; i++)
        {
            slots[i].DisplayRecipe(recipes[i]);
        }
    }

    public Recipe GetRandomRecipe()
    {
        int randomIndex = Random.Range(0, recipes.Length);
        return recipes[randomIndex];
    }

}
