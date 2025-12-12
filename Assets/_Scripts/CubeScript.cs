using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int x, y, z;  // cube coordinate
    public bool isActive = true;

    public void OnHandTouch()
    {
        if (isActive)
        {
            RemoveCube();
        }
    }

    void OnMouseDown()
    {
        if (isActive)
        {
            RemoveCube();
        }
    }

    void RemoveCube()
    {
        isActive = false;
        gameObject.SetActive(false);
        Debug.Log($"Hand touched! Cube removed: {x}, {y}, {z}");
    }
}
