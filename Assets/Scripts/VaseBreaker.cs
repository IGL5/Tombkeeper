using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaseBreaker : MonoBehaviour
{
    [Header("Settings")]
    public float requiredBreakSpeed = 2.0f; // Velocidad m�nima para romper el jarr�n
    public LayerMask collisionLayers; // Capas permitidas para la colisi�n que rompe el jarr�n

    [Header("References")]
    public GameObject intactVase; // Referencia al modelo del jarr�n intacto
    public GameObject brokenVase; // Referencia al modelo del jarr�n roto
    public GameObject playerRing;
    public GameObject talisman;
    public XRGrabInteractable grabInteractable;

    private bool isBroken = false; // Flag para controlar si el jarr�n ya est� roto

    void Start()
    {
        // Aseguramos que el jarr�n intacto est� activo y el roto est� desactivado
        intactVase.SetActive(true);
        brokenVase.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken) return; // Si ya est� roto, no hacemos nada m�s

        // Verificamos que la colisi�n no sea con el layer "Player"
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            // Calculamos la velocidad relativa en el momento de la colisi�n
            float collisionSpeed = collision.relativeVelocity.magnitude;

            if (collisionSpeed >= requiredBreakSpeed)
            {
                BreakVase(); // Llamamos al m�todo para romper el jarr�n
            }
        }
    }

    private void BreakVase()
    {
        isBroken = true; // Marcamos el jarr�n como roto
        intactVase.SetActive(false); // Desactivamos el modelo intacto
        brokenVase.SetActive(true); // Activamos el modelo roto
        talisman.SetActive(true);
        playerRing.SetActive(true);

        grabInteractable.enabled = false;
    }
}
