using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChestLock : MonoBehaviour
{
    [Header("Socket References")]
    public GameObject leftSocket; // Socket izquierdo
    public GameObject rightSocket; // Socket derecho

    [Header("Key Tags")]
    public string leftKeyTag = "Key1"; // Tag de la clave izquierda
    public string rightKeyTag = "Key2"; // Tag de la clave derecha

    [Header("Chest Components")]
    public Animator chestAnimator; // Animator del cofre
    public AudioSource openSound; // Sonido del cofre al abrirse
    public GameObject treasure; // Tesoro dentro del cofre

    private bool isOpen = false; // Para controlar si el cofre ya est� abierto

    void Start()
    {
        // Aseguramos que el tesoro est� desactivado
        if (treasure != null)
        {
            treasure.SetActive(false);
        }

        // Aseguramos que el sonido no est� activo
        if (openSound != null)
        {
            openSound.enabled = false;
        }
    }

    public void CheckCombination()
    {
        if (isOpen) return; // Si ya est� abierto, no hacemos nada

        // Verificamos los objetos en los sockets
        GameObject leftObject = GetObjectInSocket(leftSocket);
        GameObject rightObject = GetObjectInSocket(rightSocket);

        // Comprobamos si ambos objetos tienen los tags correctos
        if (leftObject != null && rightObject != null &&
            leftObject.CompareTag(leftKeyTag) && rightObject.CompareTag(rightKeyTag))
        {
            OpenChest(); // Abrimos el cofre si la combinaci�n es correcta
        }
    }

    private GameObject GetObjectInSocket(GameObject socket)
    {
        // Obtenemos el XRSocketInteractor del socket
        XRSocketInteractor socketInteractor = socket.GetComponent<XRSocketInteractor>();
        if (socketInteractor != null && socketInteractor.hasSelection)
        {
            // Usamos interactablesSelected para obtener el primer objeto seleccionado
            var interactable = socketInteractor.GetOldestInteractableSelected();
            if (interactable != null)
            {
                return interactable.transform.gameObject; // Devolvemos el objeto insertado
            }
        }
        return null; // No hay objeto insertado
    }

    private void OpenChest()
    {
        isOpen = true;

        // Activamos la animaci�n de apertura
        if (chestAnimator != null)
        {
            chestAnimator.SetBool("isOpen", true);
        }

        // Activamos el sonido
        if (openSound != null)
        {
            openSound.enabled = true;
            openSound.Play();
        }

        // Activamos el tesoro
        if (treasure != null)
        {
            treasure.SetActive(true);
        }
    }
}
