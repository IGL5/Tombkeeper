using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisController : MonoBehaviour
{
    private Animator animator;

    // Flags para controlar estados
    private bool isActivated = false;

    // Start: Inicializa el estado inicial
    void Start()
    {
        animator = GetComponent<Animator>();

        // Inicia en estado "desactivado"
        animator.SetBool("isActive", false);
    }

    public void DoorOpen()
    {
        if (isActivated)
        {
            // Cambiar a la animación de idle
            animator.SetBool("doorOpen", true);
        }
    }

    // Método para activar a Anubis cuando se accione el trigger
    public void ActivateAnubis()
    {
        if (!isActivated)
        {
            isActivated = true;

            // Cambiar a la animación de pointing
            animator.SetBool("isActive", true);
        }
    }

    // Método para iniciar la secuencia de "historia del rayo"
    public void StartProtectionSequence()
    {
        StartCoroutine(ProtectionSequence());
    }

    // Secuencia del rayo
    private IEnumerator ProtectionSequence()
    {
        // Paso 1: Activar la animación de protegerse
        animator.SetTrigger("startProtect");
        // yield return new WaitForSeconds(GetAnimationLength("Protect"));

        // Paso 2: Quedarse en idle de protección unos segundos
        yield return new WaitForSeconds(3.0f);

        // Paso 3: Activar la animación de "power-up"
        animator.SetTrigger("powerUp");
        // yield return new WaitForSeconds(GetAnimationLength("PowerUp"));

        // Paso 4: Activar la animación de lanzar el hechizo
        animator.SetTrigger("castSpell");
        // yield return new WaitForSeconds(GetAnimationLength("CastSpell"));

        // Paso 5: Desactivar de nuevo a Anubis
        animator.SetBool("isActive", false);
    }

    // Método auxiliar para obtener la duración de una animación
    private float GetAnimationLength(string animationName) // YA MANEJADO DESDE EL ANIMATOR
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
                return clip.length;
        }
        return 0f;
    }

    // AÚN POR HACER ANIMACIÓN
    /*
    // Método para activar/desactivar el diálogo (boca animada)
    public void StartDialogue(bool isTalking)
    {
        animator.SetBool("isExplaining", isTalking);

        if (isTalking && dialogueClip != null)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = dialogueClip;
                audioSource.Play();
            }
        }
    }*/
}
