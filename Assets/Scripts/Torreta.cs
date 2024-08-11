using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject jugador; // El objeto del jugador
    public GameObject proyectilPrefab; // El prefab del proyectil
    public float tiempoEntreDisparos = 2f; // Tiempo entre disparos
    public float velocidadProyectil = 10f; // Velocidad del proyectil
    public AudioClip sonidoDisparo; // El sonido de disparo
    private AudioSource audioSource; // Fuente de audio

    private float tiempoDisparo = 0f;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verificar si el jugador sigue existiendo
        if (jugador == null) return;

        // Apunta hacia el jugador
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));

        // Disparar
        tiempoDisparo += Time.deltaTime;
        if (tiempoDisparo >= tiempoEntreDisparos)
        {
            Disparar(direccion);
            tiempoDisparo = 0f;
        }
    }

    void Disparar(Vector2 direccion)
    {
        // Reproducir el sonido de disparo
        audioSource.PlayOneShot(sonidoDisparo);

        // Crear el proyectil y rotarlo hacia la dirección del disparo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.Euler(0, 0, angulo));

        // Aplicar velocidad al proyectil
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        rb.velocity = direccion * velocidadProyectil;
    }
}