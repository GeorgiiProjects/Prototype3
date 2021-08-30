using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // создаем GameObject obstaclePrefab, в который поместим префаб obstacle (препятствие), в инспекторе.
    public GameObject obstaclePrefab;
    // создаем координаты x,y,z для появления префаба в них.
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    // создаем переменную для задержки появления препятствия.
    private float startDelay = 2f;
    // создаем переменную для интервала появления препятствия.
    private float repeatRate = 2f;
    // создаем класс PlayerController для получения к нему доступа из SpawnManager скрипта.
    private PlayerController playerControllerScript;

    // Происходит при старте игры
    void Start()
    {
        // создаем метод InvokeRepeating для того чтобы препятствия появлялись с одной и той же периодичностью.
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        // GameObject.Find("Player") Ищем объект Player в иерархии unity и получаем доступ к скрипту PlayerController через GetComponent<PlayerController>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // создаем кастомный метод SpawnObstacle, для того чтобы его можно было вызывать сколько понадобится раз.
    void SpawnObstacle()
    {
        // если в скрипте PlayerController игра продолжается.
        if (!playerControllerScript.gameOver)
        {
            // создаем клоны префаба, используя оригинальный GameObject obstaclePrefab, его координаты, и поворот.
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }         
    }
}
