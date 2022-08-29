using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private float shellSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed * 50, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.tag != "Player")
        {
            if (collision.transform.tag == "CarTarget")
            {
                var textScore = GameObject.FindWithTag("Score"); //GameObject.Find("/Scene2/Canvas/Score");

                var textMesh = textScore.GetComponent<TextMeshProUGUI>();

                int score = int.Parse(textMesh.text);

                score++;

                textMesh.text = score.ToString();

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        
    }


}
