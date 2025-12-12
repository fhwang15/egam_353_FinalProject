using Leap;
using Leap.PhysicalHands;
using UnityEngine;

public class HandCubeInteraction : MonoBehaviour
{
    public void OnHandContactCube(ContactHand hand, Rigidbody cubeRigidbody)
    {
        if (cubeRigidbody == null) return;

        // Rigidbody가 있는 오브젝트에서 CubeScript 찾기
        CubeScript cube = cubeRigidbody.GetComponent<CubeScript>();

        if (cube != null && cube.isActive)
        {
            cube.OnHandTouch();
        }
    }
}