using UnityEngine;

public class TankController : MonoBehaviour
{
    public GameObject cameraTarget;

    public float maxSpeed = 5f;

    public float maxTurnSpeedBody = 5f;

    public float maxTurnSpeedTower = 5f;

    public float maxTurnSpeedTurret = 5f;

    public float deadZoneTower = 1f;
    
    private float horAxis;

    private float verAxis;

    private Vector3 defaultPosition;

    private Quaternion defaultRotation;

    void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        defaultPosition = transform.position;

        defaultRotation = transform.rotation;
    }

    void Update()
    {        
        MoveTower();

        MoveTurret(); 

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Respawn();
        }

        // Проверяем ввод 
        horAxis = Input.GetAxis("Horizontal");

        verAxis = Input.GetAxis("Vertical");
    }

    void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        MoveBody();
    }

    void Respawn()
    {
        transform.position = defaultPosition;

        transform.rotation = defaultRotation;
    }

    void Shoot()
    {
        // При клике ЛКМ совершаем выстрел создавая объект снаряда 
        GameObject shell = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        shell.transform.localScale = new Vector3(0.1f, 0.1f, 1f);

        shell.transform.position = transform.GetChild(0).GetChild(0).transform.position;

        shell.transform.rotation = transform.GetChild(0).GetChild(0).transform.rotation;

        shell.AddComponent<Rigidbody>().mass = 20;

        shell.AddComponent<Shell>();
    }

    void MoveBody()
    {
        // Управляем корпусом танка через
        transform.Translate(Vector3.forward * Time.deltaTime * maxSpeed * verAxis);

        transform.Rotate(Vector3.up * Time.deltaTime * maxTurnSpeedBody * horAxis * 5);
    }

    void MoveTower()
    {
        // Находим величину угла отклонения направления камеры от направления башни в условных ед.
        var currentAngle = Vector3.Cross(transform.GetChild(0).transform.forward, cameraTarget.transform.forward);

        var projection = Vector3.Dot(currentAngle, transform.GetChild(0).transform.up);

        // Корректируем положение башни в зависимости от положения камеры
        if (projection* 100 > 1 * deadZoneTower) transform.GetChild(0).transform.Rotate(Vector3.up* Time.deltaTime* maxTurnSpeedTower * 5);

        else if (projection* 100 < -1 * deadZoneTower) transform.GetChild(0).transform.Rotate(Vector3.down* Time.deltaTime* maxTurnSpeedTower * 5);
    }

    void MoveTurret()
    {
        // Находим величину угла отклонения направления камеры от направления пушки в условных ед.
        var projection = Vector3.Dot(transform.GetChild(0).GetChild(0).transform.forward, cameraTarget.transform.up);

        // Корректируем положение пушки в зависимости от положения камеры
        var turretPitch = Vector3.Dot(transform.GetChild(0).GetChild(0).forward, transform.GetChild(0).transform.up);

        if ((projection * 100 > 1 * deadZoneTower) && (turretPitch > -0.2f))
                
                transform.GetChild(0).GetChild(0).transform.Rotate(Vector3.right * Time.deltaTime * maxTurnSpeedTurret * 3);

        else if ((projection * 100 < -1 * deadZoneTower) && ((turretPitch < 0.2f))) 
            
            transform.GetChild(0).GetChild(0).transform.Rotate(Vector3.left * Time.deltaTime * maxTurnSpeedTurret * 3);
    }
}
