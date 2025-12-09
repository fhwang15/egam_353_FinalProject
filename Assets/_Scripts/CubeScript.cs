using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public int x, y, z;  // cube coordinate
    public bool isActive = true;

    void OnMouseDown()
    {
        if (!isActive) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                RemoveCube();
            }
        }
    }

    void RemoveCube()
    {
        isActive = false;
        gameObject.SetActive(false);  // 일단 숨기기

        // TODO: 스코어 추가, 효과음 등
        Debug.Log($"Cube removed: {x}, {y}, {z}");
    }
}
