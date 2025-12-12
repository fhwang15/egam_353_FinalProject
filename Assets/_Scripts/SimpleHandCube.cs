using UnityEngine;

public class SimpleHandCube : MonoBehaviour
{
    private CubeScript cubeScript;

    void Start()
    {
        cubeScript = GetComponent<CubeScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        // 손 Collider인지 확인
        if (other.gameObject.name.Contains("Capsule") ||
            other.gameObject.name.Contains("Hand") ||
            other.gameObject.name.Contains("Finger"))
        {
            Debug.Log("Hand touched: " + other.gameObject.name);

            if (cubeScript != null && cubeScript.isActive)
            {
                cubeScript.OnHandTouch();
            }
        }
    }
}