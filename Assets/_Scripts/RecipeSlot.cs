using UnityEngine;
using UnityEngine.UIElements;

public class RecipeSlot : MonoBehaviour
{
    public GameObject miniCubePrefab;  // small cube prefab
    public float miniCubeSize = 0.3f;  // size of minicube
    public Recipe recipe;          // recipe data

    public float rotationSpeed = 20f;

    private Transform miniCubeContainer;

    void Start()
    {
        GameObject container = new GameObject("MiniCubeContainer");
        container.transform.parent = transform;
        container.transform.localPosition = Vector3.zero;
        miniCubeContainer = container.transform;
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

        // create mini cubes based on recipe target shape
        //Same code used from the original cube generation in cube manager.

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    if (recipe.targetShape[x, y, z])
                    {
                        Vector3 position = new Vector3((x - 1) * miniCubeSize,(y - 1) * miniCubeSize,(z - 1) * miniCubeSize);

                        GameObject miniCube = Instantiate(miniCubePrefab, position, Quaternion.identity);
                        miniCube.transform.parent = miniCubeContainer;
                        miniCube.transform.localPosition = position;

                        miniCube.layer = LayerMask.NameToLayer("MiniCube");

                        // colors
                        Renderer renderer = miniCube.GetComponent<Renderer>();
                        if (y == 0)
                        {
                            renderer.material.color = Color.red;
                        }
                        else if (y == 1)
                        {
                            renderer.material.color = Color.yellow;
                        }
                        else if (y == 2)
                        {
                            renderer.material.color = Color.green;
                        }
                    }
                }
            }
        }
    }
}
