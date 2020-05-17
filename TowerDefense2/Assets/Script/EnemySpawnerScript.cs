using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
//indico cosa deve generare
public GameObject enemyPrefab;
    public float respawnTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyWawe());
    }

    //nuova funzione che si occupa dello spawn

    private void SpawnEnemy()
    {
        //crea un nuovo GameObject basato su enemyPrefab
        GameObject a = Instantiate(enemyPrefab) as GameObject;
    }

    IEnumerator EnemyWawe()
    {

        while (true)
        {
            //Al passare del tempo lo spawn diventa sempre più veloce
             yield return new WaitForSeconds(respawnTime - (Time.time * 0.05f));

            SpawnEnemy();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }


}
