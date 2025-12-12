using UnityEngine;

public class SimpleHandCube : MonoBehaviour
{
    private CubeScript cubeScript;
    private bool hasBeenTouched = false;

    void Start()
    {
        cubeScript = GetComponent<CubeScript>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasBeenTouched) return;

        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        // Physical Hands의 collider 감지
        if (collision.gameObject.name.Contains("Hand") ||
            collision.gameObject.name.Contains("Palm") ||
            collision.gameObject.name.Contains("Finger") ||
            collision.gameObject.name.Contains("Bone") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Hands"))
        {
            Debug.Log($" Hand collision confirmed! Removing cube at ({cubeScript.x}, {cubeScript.y}, {cubeScript.z})");

            if (cubeScript != null && cubeScript.isActive)
            {
                hasBeenTouched = true;
                cubeScript.OnHandTouch();
            }
        }
    }

    // Trigger도 함께 감지 (혹시 몰라서)
    void OnTriggerEnter(Collider other)
    {
        if (hasBeenTouched) return;

        Debug.Log($"Trigger detected with: {other.gameObject.name}");

        if (other.gameObject.name.Contains("Hand") ||
            other.gameObject.name.Contains("Palm") ||
            other.gameObject.name.Contains("Finger") ||
            other.gameObject.name.Contains("Bone"))
        {
            Debug.Log($"Hand trigger confirmed!");

            if (cubeScript != null && cubeScript.isActive)
            {
                hasBeenTouched = true;
                cubeScript.OnHandTouch();
            }
        }
    }
}