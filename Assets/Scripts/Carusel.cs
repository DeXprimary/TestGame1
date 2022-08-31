using UnityEngine;

public class Carusel : MonoBehaviour
{
    public float rotationSpeed = 20f;

    void Start()
    {
        
    }

    void Update()
    {
        // ¬ращаем машину
        transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);

        // ѕровер€ем угол наклона машины, если превышает допустимый, то прерываем вращение
        var crossProduct = Vector3.Cross(transform.GetChild(0).transform.up, Vector3.up).sqrMagnitude;

        if (crossProduct > 0.5f) rotationSpeed = 0;
    }
}
