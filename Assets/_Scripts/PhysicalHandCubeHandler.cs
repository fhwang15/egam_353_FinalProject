using UnityEngine;
using Leap;
using Leap.PhysicalHands;

public class PhysicalHandsCubeHandler : MonoBehaviour
{
    private PhysicalHandsManager physicalHandsManager;

    void Start()
    {
        physicalHandsManager = GetComponent<PhysicalHandsManager>();

        if (physicalHandsManager != null)
        {
            // Contact 이벤트에 리스너 추가
            physicalHandsManager.onContact.AddListener(OnHandContactCube);
            Debug.Log("Physical Hands Contact Event registered!");
        }
        else
        {
            Debug.LogError("PhysicalHandsManager not found!");
        }
    }

    void OnDestroy()
    {
        // 메모리 누수 방지
        if (physicalHandsManager != null)
        {
            physicalHandsManager.onContact.RemoveListener(OnHandContactCube);
        }
    }

    void OnHandContactCube(ContactHand hand, Rigidbody contactedRigidbody)
    {
        if (contactedRigidbody == null) return;

        Debug.Log($"Hand contact detected with: {contactedRigidbody.gameObject.name}");

        // Rigidbody가 붙어있는 GameObject에서 CubeScript 찾기
        CubeScript cubeScript = contactedRigidbody.GetComponent<CubeScript>();

        if (cubeScript != null && cubeScript.isActive)
        {
            Debug.Log($"Removing cube at ({cubeScript.x}, {cubeScript.y}, {cubeScript.z})");
            cubeScript.OnHandTouch();
        }
    }
}