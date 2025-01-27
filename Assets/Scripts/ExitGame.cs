using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        // Esto es para el editor de Unity
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Esto es para el build del juego
            Application.Quit();
#endif
    }
}
