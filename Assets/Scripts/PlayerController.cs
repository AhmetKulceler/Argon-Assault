using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the ship moves based upon player input")] [SerializeField] float controlSpeed = 25.0f;
    [Tooltip("How far the ship is able to move horizontally")]   [SerializeField] float xRange = 10f;
    [Tooltip("How far the ship is able to move vertically")]     [SerializeField] float yRange = 6f;    

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player Input Based Tuning")]
    [SerializeField] float controlPitchFactor = -10f;    
    [SerializeField] float controlRollFactor = -15f;

    [Header("Laser Gun Array")]
    [SerializeField] GameObject[] lasers;

    float xControl, yControl;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xControl = Input.GetAxis("Horizontal");
        yControl = Input.GetAxis("Vertical");

        float xOffset = xControl * controlSpeed * Time.deltaTime;
        float yOffset = yControl * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float limitedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float limitedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(limitedXPos, limitedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yControl * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControl = xControl * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = yawDueToPosition;
        float roll = rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetLasersActive(true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
