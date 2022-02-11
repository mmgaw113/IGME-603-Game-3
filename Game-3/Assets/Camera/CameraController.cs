using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera virtualCamera;
    private Cinemachine.CinemachineTransposer transposerSettings;
    [Range(0, 50)] public float cameraDistance;
    [Min(0)] public Vector3 damperSpeed;

    private void Awake()
    {
        virtualCamera = this.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        transposerSettings = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>();
    }

    public void Update()
    {
        // Remove all this once we dont need to change values at runtime.
        if (virtualCamera != null && transposerSettings != null)
        {
            if(transposerSettings.m_FollowOffset.y != cameraDistance)
            {
                transposerSettings.m_FollowOffset = new Vector3(0f, cameraDistance, 0f);
            }

            transposerSettings.m_XDamping = damperSpeed.x;
            transposerSettings.m_YDamping = damperSpeed.y;
            transposerSettings.m_ZDamping = damperSpeed.z;
        }
    }
    /// <summary>
    /// Sets the lookAt and follow targets for the camera.
    /// </summary>
    /// <param name="lookAt">The transform to look at.</param>
    /// <param name="follow">The transform to follow.</param>
    public void SetFollowLookAtTarget(Transform lookAt, Transform follow)
    {

        if(virtualCamera == null || lookAt == null || follow == null)
        {
            Debug.Log("Assigning camera lookAt and follow failed.");
            return;
        }

        virtualCamera.LookAt = lookAt;
        virtualCamera.Follow = follow;
    }
}
