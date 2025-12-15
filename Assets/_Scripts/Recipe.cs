using UnityEngine;
[System.Serializable]
public class Recipe
{
    public string recipeName;
    public float timeLimit;
    public bool[,,] targetShape = new bool[4, 4, 4];  //Change to 4*4*4 cuz playtesting made this feel better
    public int[] layerCounts = new int[4];  // how many cubes in each layer

    public Recipe(string name, float time)
    {
        recipeName = name;
        timeLimit = time;
        CalculateLayerCounts();
    }

    public void CalculateLayerCounts()
    {
        for (int y = 0; y < 4; y++)
        {
            int count = 0;
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (targetShape[x, y, z])
                        count++;
                }
            }
            layerCounts[y] = count;
        }
    }
}