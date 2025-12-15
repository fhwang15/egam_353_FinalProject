using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject cubePrefab;  
    public float cubeSize = 1f;    
    public float spacing = 0.6f;

    public Transform platform; 
    public float rotationSpeed = 30f;

    private GameObject[,,] cubes = new GameObject[4, 4, 4];
    void Start()
    {
        GenerateCubes();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            RotatePlatform();
        }
    }

    //Makey Makey: Rotate the platform
    void RotatePlatform() 
    {
        platform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void GenerateCubes()
    {
        float totalSize = cubeSize + spacing; //Cube size and spacing

        //Actually generate cubes in a 3x3x3 grid
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++) 
                {

                    Vector3 position = new Vector3((x-1.5f) * totalSize, (y- 1.5f) * totalSize, (z - 1.5f) * totalSize);

                    // Generating the Cube (with cube prefab)
                    GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                    cube.transform.parent = platform; //Set parent to this manager
                  

                    cube.AddComponent<SimpleHandCube>();

                    CubeScript cubeScript = cube.AddComponent<CubeScript>();
                    cubeScript.x = x;
                    cubeScript.y = y;
                    cubeScript.z = z;

                    cube.name = $"Cube_{x}_{y}_{z}"; //Names


                    cubes[x, y, z] = cube;

                    SetCubeColor(cube, y);
                }
            }
        }
    }

    void SetCubeColor(GameObject cube, int layer)
    {
        Renderer renderer = cube.GetComponent<Renderer>();

        if (layer == 0)
        {
            renderer.material.color = Color.red;   

        }
        else if (layer == 1)
        {
            renderer.material.color = Color.orange;   

        }
        else if (layer == 2)
        {
            renderer.material.color = Color.yellow; 

        } else if(layer == 3)
        {                       
            renderer.material.color = Color.green;
        }
    }

    public int[] GetCurrentLayerCounts()
    {
        int[] counts = new int[4];

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (cubes[x, y, z] != null && cubes[x, y, z].activeSelf)
                    {
                        counts[y]++;
                    }
                }
            }
        }

        Debug.Log($"Layer counts: [{counts[0]}, {counts[1]}, {counts[2]}, {counts[3]}]");
        return counts;
    }

    public bool IsCubeActive(int x, int y, int z)
    {
        if (x < 0 || x >= 4 || y < 0 || y >= 4 || z < 0 || z >= 4)
            return false;

        return cubes[x, y, z] != null && cubes[x, y, z].activeSelf;
    }

    public void ResetCubes()
    {
        Debug.Log("Resetting all cubes!");

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int z = 0; z < 4; z++)
                {
                    if (cubes[x, y, z] != null)
                    {
                        cubes[x, y, z].SetActive(true);

                        CubeScript cubeScript = cubes[x, y, z].GetComponent<CubeScript>();
                        if (cubeScript != null)
                        {
                            cubeScript.isActive = true;
                        }
                    }
                }
            }
        }
    }
}
