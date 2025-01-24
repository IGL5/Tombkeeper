using UnityEngine;

public class MoldedMetalController : MonoBehaviour
{
    [SerializeField] private GameObject moltenMetalObject; // Objeto que contiene el metal fundido
    [SerializeField] private GameObject moldedObject;     // Objeto que ser� activado tras la fundici�n

    public void CheckMetalState(GameObject other)
    {
        // Comprobar si el objeto que entra tiene el script MetalState
        MetalState metalState = other.GetComponent<MetalState>();
        if (metalState != null)
        {
            // Comprobar si el metal est� caliente
            if (metalState.IsHot)
            {
                // Activar el objeto moldeado
                moldedObject.SetActive(true);

                // Desactivar el objeto que contiene el metal fundido
                moltenMetalObject.SetActive(false);
            }
            else
            {
                Debug.Log("El metal no est� caliente. No se puede interactuar.");
            }
        }
    }
}
