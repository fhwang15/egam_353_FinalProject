using UnityEngine;
using UnityEngine.UIElements;

public class RecipeSlot : MonoBehaviour
{
    public GameObject miniCubePrefab;
    public float miniCubeSize = 0.3f;
    public Recipe recipe;
    public float rotationSpeed = 20f;

    private Transform miniCubeContainer;

    void Start()
    {
        SetContainer();
    }


    void SetContainer()  
    {
        if (miniCubeContainer == null)
        {
            GameObject container = new GameObject("MiniCubeContainer");
            container.transform.parent = transform;
            container.transform.localPosition = Vector3.zero;
            miniCubeContainer = container.transform;
            Debug.Log($"Container created for {gameObject.name}");
        }
    }


    void Update()
    {
        if (miniCubeContainer != null)
        {
            miniCubeContainer.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    public void DisplayRecipe(Recipe recipeData)
    {
        recipe = recipeData;
        SetContainer();
        GenerateMiniCubes();
    }

    void GenerateMiniCubes()
    {
        if (miniCubeContainer != null)
        {
            foreach (Transform child in miniCubeContainer)
            {
                Destroy(child.gameObject);
            }
        }

        for (int y = 0; y < 4; y++)     
        {
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (recipe.targetShape[x, y, z])
                    {
                        Vector3 position = new Vector3((x - 1.5f) * miniCubeSize, (y - 1.5f) * miniCubeSize, (z - 1.5f) * miniCubeSize);

                        GameObject miniCube = Instantiate(miniCubePrefab);
                        miniCube.transform.SetParent(miniCubeContainer, false);
                        miniCube.transform.localPosition = position;
                        miniCube.layer = LayerMask.NameToLayer("MiniCube");

                        Renderer renderer = miniCube.GetComponent<Renderer>();
                        if (y == 0)
                            renderer.material.color = Color.red;
                        else if (y == 1)
                            renderer.material.color = Color.orange;
                        else if (y == 2)
                            renderer.material.color = Color.yellow;
                        else if (y == 3)
                            renderer.material.color = Color.green;
                    }
                }
            }
        }
    }
}
