using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _dmg = 1;
    private Player _player;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("animator is NULL");
        }
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
            transform.position = new Vector3(Random.Range((-10.7f), (10.7f)), 5f, transform.position.z);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage(this._dmg);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.tag == "Laser")
        {
            if(other.transform.parent)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            Destroy(this.gameObject, 2.8f);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5, 11));
            }
        }
    }

    public void ChangeDamage(int Damage)
    {
        this._dmg = Damage;
    }
}
