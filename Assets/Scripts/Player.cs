using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _hp = 3;
    private SpawnManager _spawnManager;
    private bool _tripleShot = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _tripleShotTimer = 0f;
    [SerializeField]
    private float _speedboostTimer = 0f;
    [SerializeField]
    private float _shield_Hp = 0f;
    [SerializeField]
    private float _Shield_HpTimer = 0f;
    [SerializeField]
    private GameObject _shield;
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private bool GodMod = false;


    // Start is called before the first frame update



    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Spawn manager is null on Player");
        }
        if (_uiManager == null)
        {
            Debug.Log("UI manager is null on Player");
        }
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovment();
        fireLaser();
    }

    void CalculateMovment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), 0);
        if (transform.position.x >= 10.75f)
        {
            transform.position = new Vector3(-10.75f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.75f)
        {
            transform.position = new Vector3(10.75f, transform.position.y, 0);
        }
    }

    void fireLaser()
    {
        if (Input.GetKey(KeyCode.Space) && (Time.time > _canFire))
        {
            _canFire = Time.time + _fireRate;
            if (_tripleShot)
            {
                Instantiate(_tripleShotPrefab, new Vector3(transform.position.x + -0.43f, transform.position.y + +0.35f, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.36f, transform.position.z), Quaternion.identity);
            }
        }
    }

    public void Damage(int dmg)
    {
        if (!GodMod)
        {
            if (_shield_Hp > 0f)
            {
                _shield_Hp--;
                if (_shield_Hp == 0)
                {
                    _shield.SetActive(false);
                    _Shield_HpTimer = 0f;
                    // Debug.Log("Shield is deactiveted");
                }
            }
            else
            {
                this._hp -= dmg;
                if (this._hp <= 0f)
                {
                    Debug.Log("Player is dead!");
                    Destroy(this.gameObject);
                    _uiManager.GameOver();
                    _spawnManager.onPlayerDeath();
                }
                _uiManager.UpdateLives(this._hp);
            }
        }
        else
        {
            Debug.Log("Player is hit");
        }

    }

    public void TripleShot()
    {
        _tripleShot = true;
        _tripleShotTimer += 5;
        StartCoroutine(TripleShotRoutine());
    }


    private IEnumerator TripleShotRoutine()
    {
        while (_tripleShotTimer > 0)
        {
            //Debug.Log("triple Shot is activeted for " + _tripleShotTimer);
            _tripleShotTimer--;
            yield return new WaitForSeconds(1);
        }
        _tripleShot = false;
        //Debug.Log("triple Shot is deactiveted");

    }

    public void SpeedBoost()
    {
        _speed = 30f;
        _speedboostTimer += 5;
        StartCoroutine(SpeedBoostRoutine());
    }

    private IEnumerator SpeedBoostRoutine()
    {
        while (_speedboostTimer > 0)
        {
            Debug.Log("Speed boost is activeted for " + _speedboostTimer);
            _speedboostTimer--;
            yield return new WaitForSeconds(1);
        }
        _speed = 10f;
        Debug.Log("Speed boost is deactiveted");
    }

    public void Shield()
    {
        _shield_Hp = 3f;
        _Shield_HpTimer += 15;
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        while (_Shield_HpTimer > 0)
        {
            _shield.SetActive(true);
            Debug.Log("Shield is activeted for " + _speedboostTimer);
            _Shield_HpTimer--;
            yield return new WaitForSeconds(1);
        }
        _shield.SetActive(false);
        _shield_Hp = 0f;
        Debug.Log("Shield is deactiveted");
    }
    public void AddScore(int NewScore)
    {
        this._score += NewScore;
        _uiManager.UpdateScore(this._score);
    }


}