using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    public GameObject anchor1stPerson;

    public GameObject anchor3rdPerson;

    public GameObject turret;

    public GameObject cameraTarget;

    public Camera mainCamera;

    public float cameraOffsetFloating = 100f;

    public float mouseSensitivity = 100;

    private Vector3 localPosCamera;

    private float cameraDistanceOffset;

    private float mouseWheel;

    private float mouseAxisX;

    private float mouseAxisY;

    private bool is1stPersoncamera = false;

    void Start()
    {
        Camera.SetupCurrent(mainCamera);

        localPosCamera = anchor1stPerson.transform.InverseTransformPoint(anchor3rdPerson.transform.position);
    }

    void Update()
    {
        CameraModeChange();

        CameraMove();

        TargetRotate();
    }

    void LateUpdate()
    {

    }

    void CameraModeChange()
    {
        mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel != 0)
        {
            // Меняем режим от 1 лица если камера 3 лица близко
            cameraDistanceOffset = Mathf.Clamp(cameraDistanceOffset + mouseWheel, 0, 0.8f);
                        
            if (cameraDistanceOffset > 0.7) { is1stPersoncamera = true; }

            else { is1stPersoncamera = false; }

            // При вращении колеса мыши тащим камеру вдоль оси между дальним видом от 3 лица и видом от 1 лица
            var directionOffsetVector = anchor1stPerson.transform.position - anchor3rdPerson.transform.position;

            directionOffsetVector = directionOffsetVector * cameraDistanceOffset;

            var offset = anchor3rdPerson.transform.position + directionOffsetVector;

            localPosCamera = anchor1stPerson.transform.InverseTransformPoint(offset);
        }
    }

    void CameraMove()
    {
        // Двигаем камеру плавно или твёрдо за целью камеры в зависимости от текущего режима вида
        if (is1stPersoncamera)
        {
            var newPosition = anchor1stPerson.transform.TransformPoint(localPosCamera * 0.8f);

            transform.position = newPosition;

            transform.LookAt(anchor1stPerson.transform.position + Vector3.up * 0.5f);
        }
        else
        {
            var newPosition = anchor1stPerson.transform.TransformPoint(localPosCamera);

            transform.position = Vector3.Lerp(transform.position, newPosition, cameraOffsetFloating * Time.deltaTime);

            transform.LookAt(anchor1stPerson.transform.position + Vector3.up * 0.5f);
        }        
    }

    void TargetRotate()
    {
        // Двигаем цель камеры по осям движения мыши
        mouseAxisX += Input.GetAxis("Mouse X");

        mouseAxisY -= Input.GetAxis("Mouse Y");

        mouseAxisY = Math.Clamp(mouseAxisY, -10f, 10f);

        cameraTarget.transform.rotation = Quaternion.Euler(mouseAxisY, mouseAxisX, 0);

        //cameraTarget.transform.rotation = Quaternion.Lerp(target.transform.rotation, Quaternion.Euler(mouseAxisY, mouseAxisX, 0), cameraOffsetFloating * Time.deltaTime);
    }
}
