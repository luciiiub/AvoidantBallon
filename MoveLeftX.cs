using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float speed;
    private PlayerControllerX playerControllerScript;
    private float leftBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
    }

    void Update()
{
    // Solo mover si el juego NO ha terminado
    if (!playerControllerScript.gameOver)
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    // Si el objeto no es el fondo y sale de la pantalla, destruirlo
    if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
    {
        Destroy(gameObject);
    }
}
}
