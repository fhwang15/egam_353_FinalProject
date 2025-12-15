using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public CubeManager cubeManager;

    public int CalculateScore(Recipe recipe)
    {
        if (IsPerfectShapeMatch(recipe))
        {
            Debug.Log("Perfect Shape Match! 100 points!");
            return 100;
        }

        int[] currentLayerCounts = cubeManager.GetCurrentLayerCounts();
        int score = 0;

        for (int y = 0; y < 4; y++)
        {
            if (currentLayerCounts[y] == recipe.layerCounts[y])
            {
                score += 25;
            }
            else
            {
                int diff = Mathf.Abs(currentLayerCounts[y] - recipe.layerCounts[y]);
                int layerScore = Mathf.Max(0, 25 - diff * 2);
                score += layerScore;
            }
        }

        return score;
    }

    bool IsPerfectShapeMatch(Recipe recipe)
    {
        for (int rotation = 0; rotation < 4; rotation++)
        {
            if (CheckRotatedMatch(recipe, rotation))
            {
                return true;
            }
        }

        return false;
    }

    bool CheckRotatedMatch(Recipe recipe, int rotation)
    {
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    int rotatedX, rotatedZ;
                    GetRotatedCoordinates(x, z, rotation, out rotatedX, out rotatedZ);

                    bool shouldExist = recipe.targetShape[x, y, z];
                    bool doesExist = cubeManager.IsCubeActive(rotatedX, y, rotatedZ);

                    if (shouldExist != doesExist)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    void GetRotatedCoordinates(int x, int z, int rotation, out int newX, out int newZ)
    {
        float centerX = 1.5f;
        float centerZ = 1.5f;

        float relX = x - centerX;
        float relZ = z - centerZ;

        switch (rotation)
        {
            case 0: 
                newX = x;
                newZ = z;
                break;

            case 1:
                newX = Mathf.RoundToInt(centerX + relZ);
                newZ = Mathf.RoundToInt(centerZ - relX);
                break;

            case 2:
                newX = Mathf.RoundToInt(centerX - relX);
                newZ = Mathf.RoundToInt(centerZ - relZ);
                break;

            case 3:
                newX = Mathf.RoundToInt(centerX - relZ);
                newZ = Mathf.RoundToInt(centerZ + relX);
                break;

            default:
                newX = x;
                newZ = z;
                break;
        }

        newX = Mathf.Clamp(newX, 0, 3);
        newZ = Mathf.Clamp(newZ, 0, 3);
    }
}
