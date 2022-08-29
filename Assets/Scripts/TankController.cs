using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public GameObject cameraTarget;

    public float maxSpeed = 5f;

    public float maxTurnSpeedBody = 30f;

    public float maxTurnSpeedTower = 10f;

    public float maxTurnSpeedTurret = 10f;

    public float deadZoneTower = 0f;
    
    private float horAxis;

    private float verAxis;
       
    void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MoveBody();

        MoveTower();

        MoveTurret();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void LateUpdate()
    {

    }

    void Shoot()
    {
        GameObject shell = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        shell.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        shell.transform.position = transform.GetChild(0).GetChild(0).transform.position;

        shell.transform.rotation = transform.GetChild(0).GetChild(0).transform.rotation;

        shell.AddComponent<Rigidbody>().mass = 5;

        shell.AddComponent<Shell>();
    }

    void MoveBody()
    {
        // ѕерехватываем оси управлени€ танком
        horAxis = Input.GetAxis("Horizontal");

        verAxis = Input.GetAxis("Vertical");

        // ”правл€ем корпусом танка через
        transform.Translate(Vector3.forward * Time.deltaTime * maxSpeed * verAxis);

        transform.Rotate(Vector3.up * Time.deltaTime * maxTurnSpeedBody * horAxis);
    }

    void MoveTower()
    {
        // Ќаходим величину угла отклонени€ направлени€ камеры от направлени€ башни в условных ед.
        var currentAngle = Vector3.Cross(transform.GetChild(0).transform.forward, cameraTarget.transform.forward);

        var projection = Vector3.Dot(currentAngle, transform.GetChild(0).transform.up);

        //  орректируем положение башни в зависимости от положени€ камеры
        if (projection* 100 > 1 * deadZoneTower) transform.GetChild(0).transform.Rotate(Vector3.up* Time.deltaTime* maxTurnSpeedTower * 20);

        else if (projection* 100 < -1 * deadZoneTower) transform.GetChild(0).transform.Rotate(Vector3.down* Time.deltaTime* maxTurnSpeedTower * 20);
    }

    void MoveTurret()
    {
        // Ќаходим величину угла отклонени€ направлени€ камеры от направлени€ пушки в условных ед.
        var currentAngle = Vector3.Cross(transform.GetChild(0).GetChild(0).transform.forward, cameraTarget.transform.forward);

        var projection = Vector3.Dot(currentAngle, transform.GetChild(0).GetChild(0).transform.right);

        //  орректируем положение пушки в зависимости от положени€ камеры
        if (projection* 100 > 1 * deadZoneTower) transform.GetChild(0).GetChild(0).transform.Rotate(Vector3.right* Time.deltaTime* maxTurnSpeedTurret * 10);

        else if (projection* 100 < -1 * deadZoneTower) transform.GetChild(0).GetChild(0).transform.Rotate(Vector3.left* Time.deltaTime* maxTurnSpeedTurret * 10);
}
}
