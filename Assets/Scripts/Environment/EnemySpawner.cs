using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour

{
    public GameObject skeletonEnemy;
    public GameObject redDragonEnemy;
    public GameObject boss;

    public void SpawnEnemies(int skeletonCount, int redDragonEnemyCount, int difficulty)
    {
        for (int i = 0; i < skeletonCount; i++)
        {
            float rndOffset = Random.Range(-8, 8);
            GameObject enemyInstance = Instantiate(skeletonEnemy, transform.position + new Vector3(rndOffset, 0, rndOffset), Quaternion.identity);
            enemyInstance.GetComponent<SkeletonEnemyStats>().health.IncreaseValue(difficulty);
            enemyInstance.GetComponent<SkeletonEnemyStats>().damage.IncreaseValue(difficulty);
            enemyInstance.GetComponent<SkeletonEnemyStats>().armor.IncreaseValue(difficulty);

            GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().AddEnemy(enemyInstance);
            //enemyInstance.GetComponent<EnemyController>().player = GameObject.FindWithTag("Player").transform;
        }

        for (int i = 0; i < redDragonEnemyCount; i++)
        {
            float rndOffset = Random.Range(-8, 8);
            GameObject enemyInstance = Instantiate(redDragonEnemy, transform.position + new Vector3(rndOffset, 0, rndOffset), Quaternion.identity);
            enemyInstance.GetComponent<RedDragonEnemyStats>().health.IncreaseValue(difficulty);
            enemyInstance.GetComponent<RedDragonEnemyStats>().damage.IncreaseValue(difficulty);
            enemyInstance.GetComponent<RedDragonEnemyStats>().armor.IncreaseValue(difficulty);
            GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().AddEnemy(enemyInstance);
            //enemyInstance.GetComponent<EnemyController>().player = GameObject.FindWithTag("Player").transform;
        }
    }
    public void SpawnBoss()
    {
        GameObject bossInstandce = Instantiate(boss, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("Main Logic").GetComponent<GameLogic>().AddEnemy(bossInstandce);
    }
}
