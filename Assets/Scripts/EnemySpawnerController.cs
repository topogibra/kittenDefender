using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
  public GameObject enemyPrefab;

  private void spawn(){
    foreach (Transform child in transform) {
      Instantiate(enemyPrefab, child.position, Quaternion.identity);
    }
  }

}
