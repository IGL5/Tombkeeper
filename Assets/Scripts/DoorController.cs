using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Variables p�blicas configurables
    public float raiseAmount = 3f; // Cu�nto subir� la puerta
    public float raiseSpeed = 2f;  // Velocidad de subida

    private Vector3 initialPosition; // Posici�n inicial de la puerta
    private bool isOpening = false; // Si la puerta se est� abriendo

    private void Start()
    {
        // Guardar la posici�n inicial de la puerta
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Acceder al GameManager para comprobar la condici�n
            /*if (GameManager.Instance != null && GameManager.Instance.CanOpenDoor())
            {
                // Iniciar la apertura de la puerta
                StartCoroutine(OpenDoor());
            }*/
        }
    }

    private IEnumerator OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;

            // Calcular la posici�n final
            Vector3 targetPosition = initialPosition + new Vector3(0, raiseAmount, 0);

            // Mover la puerta hacia arriba hasta alcanzar la posici�n objetivo
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * raiseSpeed);
                yield return null;
            }

            // Asegurarse de que la puerta est� exactamente en la posici�n final
            transform.position = targetPosition;
        }
    }
}
