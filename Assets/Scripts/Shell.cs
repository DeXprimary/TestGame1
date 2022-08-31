using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private float shellSpeed = 10f;

    private TimeSpan maxLifeTime = TimeSpan.FromSeconds(1);

    private Stopwatch lifeTimer = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        // ������ ��������� ������� �������
        GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed * 200, ForceMode.Impulse);

        // ��������� ������� ������� ����� �������
        lifeTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ������ ���� ������ maxLifeTime, �� ������� ���
        if (lifeTimer.ElapsedMilliseconds > maxLifeTime.TotalMilliseconds) Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.tag != "Player")
        {
            if (collision.transform.tag == "CarTarget")
            {
                // ���� ��������� ��������� �� ����, �� ��������� ���� � ������� ������
                var textScore = GameObject.FindWithTag("Score"); //GameObject.Find("/Scene2/Canvas/Score");

                var textMesh = textScore.GetComponent<TextMeshProUGUI>();

                int score = int.Parse(textMesh.text);

                score++;

                textMesh.text = score.ToString();

                Destroy(gameObject);
            }
            else
            {
                // ���� ��������� ��������� �� ������ ������� �������, �� ������ ������� ������
                Destroy(gameObject);
            }
        }

        
    }


}
