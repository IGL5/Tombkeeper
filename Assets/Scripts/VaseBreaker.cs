using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaseBreaker : MonoBehaviour
{
    [Header("Settings")]
    public float requiredBreakSpeed = 2.0f; // Velocidad mínima para romper el jarrón
    public LayerMask collisionLayers; // Capas permitidas para la colisión que rompe el jarrón

    [Header("References")]
    public GameObject intactVase; // Referencia al modelo del jarrón intacto
    public GameObject brokenVase; // Referencia al modelo del jarrón roto
    public GameObject playerRing;
    public GameObject talisman;
    public XRGrabInteractable grabInteractable;

    private bool isBroken = false; // Flag para controlar si el jarrón ya está roto

    void Start()
    {
        // Aseguramos que el jarrón intacto está activo y el roto está desactivado
        intactVase.SetActive(true);
        brokenVase.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken) return; // Si ya está roto, no hacemos nada más

        // Verificamos que la colisión no sea con el layer "Player"
        if (((1 << collision.gameObject.layer) & collisionLayers) != 0)
        {
            // Calculamos la velocidad relativa en el momento de la colisión
            float collisionSpeed = collision.relativeVelocity.magnitude;

            if (collisionSpeed >= requiredBreakSpeed)
            {
                BreakVase(); // Llamamos al método para romper el jarrón
            }
        }
    }

    private void BreakVase()
    {
        isBroken = true; // Marcamos el jarrón como roto
        intactVase.SetActive(false); // Desactivamos el modelo intacto
        brokenVase.SetActive(true); // Activamos el modelo roto
        talisman.SetActive(true);
        playerRing.SetActive(true);

        grabInteractable.enabled = false;
    }
}
