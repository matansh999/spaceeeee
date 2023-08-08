using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private float _wave = 0;
    [SerializeField]
    private GameObject _Container;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] Powerup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!_stopSpawning)
        {
            Debug.Log("wave " + _wave);
            _wave++;
            yield return new WaitForSeconds(5);
            Debug.Log("wave " + _wave);
            for (int i = 0; i <= _wave * 2; i++)
            {
                if(_stopSpawning)
                {
                    break;
                }
                Vector3 pos = new Vector3(Random.Range((-9.4f), (9.4f)), 4.03f, 0f);
                GameObject newEnemy = Instantiate(_enemy, pos, Quaternion.identity);
                newEnemy.transform.parent = _Container.transform;
                newEnemy.transform.name = "Wave: " + _wave + "Enemy: " + i;
                 yield return new WaitForSeconds(Random.Range(0F,0.2f));
            }

        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning)
        {
            Vector3 pos = new Vector3(Random.Range((-9.4f), (9.4f)), 4.03f, 0f);
            GameObject newTripleShotPowerup = Instantiate(Powerup[Random.Range(0,3)], pos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
