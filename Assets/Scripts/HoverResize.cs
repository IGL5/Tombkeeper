using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverResize : MonoBehaviour
{
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1.5f); // Tamaño durante el hover
    private Vector3 originalScale;  // Tamaño original del objeto
    private XRGrabInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            // Guardamos el tamaño original del objeto
            originalScale = transform.localScale;

            // Asignamos los métodos a los eventos de hover
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // Cambiamos el tamaño al hacer hover
        transform.localScale = hoverScale;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        // Restauramos el tamaño original al salir del hover
        transform.localScale = originalScale;
    }

    private void OnDestroy()
    {
        // Removemos los eventos para evitar errores si el objeto se destruye
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEnter);
            interactable.hoverExited.RemoveListener(OnHoverExit);
        }
    }
}
