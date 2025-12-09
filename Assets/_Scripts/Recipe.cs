using UnityEngine;
[System.Serializable]
public class Recipe
{

    public string recipeName;
    public float timeLimit;
    public bool[,,] targetShape = new bool[3, 3, 3];

    public Recipe(string name, float time)
    {
        recipeName = name;
        timeLimit = time;
    }
}
