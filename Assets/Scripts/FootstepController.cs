using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [Header("Joystick Settings")]
    [Tooltip("Ejes del joystick para detectar movimiento.")]
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    [Header("Audio Settings")]
    [Tooltip("Fuente de audio para las pisadas.")]
    public GameObject footstepSource;

    private bool isWalking = false; // Flag para evitar múltiples activaciones

    void Update()
    {
        // Detecta input del joystick
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        bool isJoystickMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // Control del sonido basado en el input del joystick
        if (isJoystickMoving && !isWalking)
        {
            StartFootsteps();
        }
        else if (!isJoystickMoving && isWalking)
        {
            StopFootsteps();
        }
    }

    private void StartFootsteps()
    {
        isWalking = true; // Cambiar el estado a "caminando"
        if (footstepSource != null && !footstepSource.activeSelf)
        {
            footstepSource.SetActive(true);
        }
    }

    private void StopFootsteps()
    {
        isWalking = false; // Cambiar el estado a "detenido"
        if (footstepSource != null && footstepSource.activeSelf)
        {
            footstepSource.SetActive(false);
        }
    }
}
