using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovment();
    }

    void CalculateMovment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction*speed*Time.deltaTime);
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,3.8f),0);
        if (transform.position.x >= 15f)
        {
            transform.position = new Vector3(-13f,transform.position.y,0);
        }
        else if (transform.position.x  <=-15f)
        {
            transform.position = new Vector3(15f,transform.position.y,0);
        }
    }
}




