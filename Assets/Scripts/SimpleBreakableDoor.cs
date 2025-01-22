using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBreakableDoor : MonoBehaviour
{
    public GameObject intactDoor; // Modelo de la puerta entera
    public GameObject brokenPiecesContainer; // Contenedor de las piezas rotas
    public AudioClip impactSound; // Sonido al golpear
    public AudioClip collapseSound; // Sonido durante el derrumbe
    public int hitsToBreak = 6; // Número de golpes necesarios para romper
    public float collapseDuration = 3f; // Duración del derrumbe antes de desactivar piezas

    private int currentHits = 0; // Contador de golpes
    private bool isBroken = false; // Flag para evitar ejecutar varias veces

    private void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        // Detectar si la interacción es válida
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            currentHits++;

            // Reproducir sonido de impacto
            if (impactSound != null)
            {
                AudioSource.PlayClipAtPoint(impactSound, other.transform.position);
            }

            // Verificar si se ha alcanzado el límite de golpes
            if (currentHits >= hitsToBreak)
            {
                BreakDoor();
            }
        }
    }

    private void BreakDoor()
    {
        isBroken = true;

        // Desactivar el modelo de la puerta intacta
        if (intactDoor != null)
        {
            intactDoor.SetActive(false);
        }

        // Activar las piezas rotas
        if (brokenPiecesContainer != null)
        {
            brokenPiecesContainer.SetActive(true);
        }

        // Reproducir sonido de derrumbe
        if (collapseSound != null)
        {
            AudioSource.PlayClipAtPoint(collapseSound, transform.position);
        }

        // Desactivar las piezas tras un tiempo
        StartCoroutine(DisableBrokenPiecesAfterDelay());
    }

    private IEnumerator DisableBrokenPiecesAfterDelay()
    {
        yield return new WaitForSeconds(collapseDuration);

        // Desactivar las piezas rotas
        if (brokenPiecesContainer != null)
        {
            brokenPiecesContainer.SetActive(false);
        }
    }
}
