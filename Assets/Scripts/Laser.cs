using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 6.07)
        {
            if (transform.parent)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}