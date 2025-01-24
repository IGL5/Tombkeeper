using UnityEngine;
using UnityEngine.Audio;

public class BucketController : MonoBehaviour
{
    [Header("Animator Reference")]
    public Animator bucketAnimator; // Referencia al Animator del cubo
    public AudioClip clipToPlay;
    public GameObject ring;

    private bool isFilled = true; // Estado del cubo (lleno o vacío)
    private bool firstTime = true;

    public void FillBucket()
    {
        isFilled = true; // Marcamos el cubo como lleno

        if (bucketAnimator != null)
        {
            bucketAnimator.SetBool("fill", true);
            isFilled = true; // Marcamos el cubo como lleno
        }
        else
        {
            Debug.LogWarning("No hay animator.");
        }

        if (firstTime)
        {
            ring.SetActive(true);
            firstTime = false;
        }
    }

    public void EmptyBucket(AudioSource audioSource)
    {
        if (bucketAnimator != null)
        {
            bucketAnimator.SetBool("fill", false);
            if (isFilled)
            {
                audioSource.clip = clipToPlay;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("No hay animator.");
        }
    }

    public bool IsFilled() { return isFilled; }

    public void SetEmpty() { isFilled = false; }
}
