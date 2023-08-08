using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int _powerupID;
    //ID for powerups
    // 0 = tripleshot
    // 1 = speedbost
    // 2 = shield
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movment();
    }

    void Movment()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShot();
                        break;
                    case 1:
                        player.SpeedBoost();
                        break;
                    case 2:
                        player.Shield();
                        break;
                    default:
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
