using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUI : MonoBehaviour {
    public Transform lane;
    public GameObject enemyTypeStat;
    public SpawnEnemies spawnEnemies;

	void Update () {
		//this.transform.position = Camera.main.WorldToScreenPoint(lane.transform.position) - new Vector3(0f, 40f, 0f);
    }

    public void Create()
    {
        /*foreach (KeyValuePair<GameObject, int> occurringEnemy in spawnEnemies.occurringEnemies)
        {
            var item = Instantiate(enemyTypeStat, transform);
            item.GetComponentInChildren<Image>().sprite = occurringEnemy.Key.GetComponent<BaseEnemy>().icon;
            item.GetComponentInChildren<Text>().text = occurringEnemy.Value.ToString(); 
        }*/
    }
}