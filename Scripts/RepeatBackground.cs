using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    // создаем структуру Vector 3, чтобы инициализировать начальную позицию background.
    private Vector3 startPos;
    // создаем переменную для инициализации boxcollider background.
    private float repeatWidth;

    void Start()
    {
        // начальной позиции присваиваем значение позицию по оcям x,y,z background.
        startPos = transform.position;
        // получаем доступ к BoxCollider background через GetComponent, далее длину BoxCollider 112 делим на 2.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // если значение текущей позиции background по оси x меньше значения позиции startPos.x - repeatWidth.
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // значение позиции по оcям x,y,z background (текущая позиция координат background) сбрасывается на значения по умолчанию.
            transform.position = startPos;
        }
    }
}
