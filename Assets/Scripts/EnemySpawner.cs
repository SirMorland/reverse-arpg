using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemy;

    void Start()
    {
        for(int i = 0; i < 90; i++)
		{
			GameObject newEnemy = Instantiate(enemy);
			newEnemy.transform.position = new Vector3(Random.Range(-350f, -50f), 0f, 0f);
		}
    }
}
