using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public CubeManager cubeManager;

    public int CalculateScore(Recipe recipe)
    {
        int[] currentLayerCounts = cubeManager.GetCurrentLayerCounts();

        // 완벽 매칭 체크
        if (IsPerfectMatch(recipe, currentLayerCounts))
        {
            Debug.Log("Perfect Match! 100 points!");
            return 100;
        }

        // 층별 부분 점수
        int score = 0;
        for (int y = 0; y < 4; y++)
        {
            if (currentLayerCounts[y] == recipe.layerCounts[y])
            {
                score += 25;  // 층 정확히 일치
                Debug.Log($"Layer {y} perfect: +25");
            }
            else
            {
                // 근접도 점수
                int diff = Mathf.Abs(currentLayerCounts[y] - recipe.layerCounts[y]);
                int layerScore = Mathf.Max(0, 25 - diff * 2);
                score += layerScore;
                Debug.Log($"Layer {y} close: +{layerScore} (diff: {diff})");
            }
        }

        return score;
    }

    bool IsPerfectMatch(Recipe recipe, int[] currentCounts)
    {
        for (int y = 0; y < 4; y++)
        {
            if (currentCounts[y] != recipe.layerCounts[y])
                return false;
        }
        return true;
    }
}