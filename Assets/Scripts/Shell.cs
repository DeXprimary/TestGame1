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
        // Задаем начальный импульс снаряда
        GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed * 200, ForceMode.Impulse);

        // Запускаем счётчик времени жизни снаряда
        lifeTimer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Если снаряд живёт дольше maxLifeTime, то удаляем его
        if (lifeTimer.ElapsedMilliseconds > maxLifeTime.TotalMilliseconds) Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.tag != "Player")
        {
            if (collision.transform.tag == "CarTarget")
            {
                // Если совершено попадание по цели, то добавляем очко и удаляем снаряд
                var textScore = GameObject.FindWithTag("Score"); //GameObject.Find("/Scene2/Canvas/Score");

                var textMesh = textScore.GetComponent<TextMeshProUGUI>();

                int score = int.Parse(textMesh.text);

                score++;

                textMesh.text = score.ToString();

                Destroy(gameObject);
            }
            else
            {
                // Если совершено попадание по любому другому объекту, то просто удаляем снаряд
                Destroy(gameObject);
            }
        }

        
    }


}
