using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoverResize : MonoBehaviour
{
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1.5f); // Tama�o durante el hover
    private Vector3 originalScale;  // Tama�o original del objeto
    private XRGrabInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            // Guardamos el tama�o original del objeto
            originalScale = transform.localScale;

            // Asignamos los m�todos a los eventos de hover
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // Cambiamos el tama�o al hacer hover
        transform.localScale = hoverScale;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        // Restauramos el tama�o original al salir del hover
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
