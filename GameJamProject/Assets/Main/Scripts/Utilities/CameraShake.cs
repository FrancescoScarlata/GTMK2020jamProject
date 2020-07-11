using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

/// <summary>
/// Class that will handle the camera shake
/// </summary>
public class CameraShake : MonoBehaviour
{
    [Header("the virtual camera used in the level")]
    public CinemachineVirtualCamera virtualCamera;

    [Space(10)]
    [Header("- - - Test - - -")]
    public float duration;
    public float magnitude;
    public float frequency;

    protected CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public static CameraShake instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
            StartCoroutine(CheckCameraHasAFollow());
        }
            

    }





    /// <summary>
    /// Method made to test the shake with a UI button
    /// </summary>
    public void ExecuteShake()
    {
        ShakeTheCamera(duration,magnitude,frequency);
    }

    /// <summary>
    /// Method to call to shake the camera.
    /// </summary>
    /// <param name="duration">the duration of the shake</param>
    /// <param name="magnitude">the strength of the shake</param>
    public void ShakeTheCamera(float duration=0.1f,float magnitude=1, float frequency=1f)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    // this will make the real shake
    IEnumerator Shake(float duration, float magnitude)
    {
        virtualCameraNoise.m_AmplitudeGain = magnitude;
        yield return new WaitForSeconds(duration);
        virtualCameraNoise.m_AmplitudeGain = 0;
    }


    IEnumerator CheckCameraHasAFollow()
    {
        yield return new WaitForSeconds(3);
        if (virtualCamera.m_Follow == null)
        {
            virtualCamera.m_Follow = CharacterController.instance.transform;
        }
    }



}
