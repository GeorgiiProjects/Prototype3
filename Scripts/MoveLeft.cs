using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // создаем переменную для скорости перемещения влево бэкграунда и препятствий.
    private float speed = 30;
    // создаем класс PlayerController для получения к нему доступа из MoveLeft скрипта.
    private PlayerController playerControllerScript;
    // создаем переменную обозначающую минусовую границу уничтожения препятствий по оси x, т.е. -x.
    private float leftBound = -15f;

    void Start()
    {
        // GameObject.Find("Player") Ищем объект Player в иерархии unity и получаем доступ к скрипту PlayerController через GetComponent<PlayerController>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // если в скрипте PlayerController игра продолжается
        if (!playerControllerScript.gameOver)
        {
            // используем transform.Translate для перемещения объектов влево
            // используем Vector3.left * speed для того чтобы указать направление влево и скорость передвижения объектов.
            // используем Time.deltaTime для плавности перемещения объекта за 1 секунду на равные расстояния, на любом пк.
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // если позиция obstacle по оси x < -15 и у объекта obstacle есть тэг "Obstacle" (иначе игра сломается)
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            // уничтожаем obstacle.
            Destroy(gameObject);
        }
    }
}
