using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver; 

    public float floatForce; 
    public float gravityModifier = 1.0f; // Multiplicador de gravedad
    private Rigidbody playerRb; 

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle; 

    private AudioSource playerAudio; 
    public AudioClip moneySound; 
    public AudioClip explodeSound; 

    private float upperLimit = 15f; // Altura maxima permitida

    void Start()
    {
        // Ajusta la gravedad global
        Physics.gravity *= gravityModifier;

        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Aplica una pequena fuerza hacia arriba al inicio del juego
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    void Update()
    {
        // Mientras se presiona espacio, el juego no ha terminado y no se ha alcanzado la altura maxima
        if (Input.GetKey(KeyCode.Space) && !gameOver && transform.position.y < upperLimit)
        {
            playerRb.AddForce(Vector3.up * floatForce); // Aplica fuerza hacia arriba
        }

        // Si el jugador supera la altura maxima, lo reposiciona y detiene su movimiento
        if (transform.position.y > upperLimit)
        {
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
            playerRb.Sleep(); // Detiene el Rigidbody para evitar que siga subiendo
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si el jugador choca con una bomba
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play(); // Reproduce particula de explosion
            playerAudio.PlayOneShot(explodeSound, 1.0f); // Reproduce sonido de explosion
            gameOver = true; // Marca el juego como terminado
            Debug.Log("Game Over!");
            Destroy(other.gameObject); // Destruye la bomba
        }
        // Si choca con dinero
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.transform.position = transform.position; // Linea nueva,hace que el efecto siga al globo
            fireworksParticle.Play(); // Reproduce particula de fuegos artificiales
            playerAudio.PlayOneShot(moneySound, 1.0f); // Reproduce sonido de dinero
            Destroy(other.gameObject); // Destruye el objeto de dinero
        }
    }
}
