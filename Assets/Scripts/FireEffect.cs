using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class FireEffect : MonoBehaviour
{
    public float waitTime = 0.5f;
    public float heatDuration = 2.0f;
    public Material heatedMaterial;
    public Color startColor = new Color(1.0f, 0.776f, 0.0f); // FFC700
    public Color endColor = new Color(1.0f, 0.529f, 0.0f);   // FF8700
    public float vibrationIntensityMax = 0.5f;

    private Coroutine heatingCoroutine;
    private Coroutine vibrationCoroutine;
    private float elapsedTime = 0f;
    private float currentWaveScale = 0f;
    private Color currentColor;
    private string _DeepColorName = "Color_7D9A58EC";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            GameObject content = other.transform.Find("content")?.gameObject;
            if (content == null)
            {
                Debug.LogWarning("El objeto metal no tiene un hijo llamado 'content'.");
                return;
            }

            if (heatingCoroutine == null)
            {
                heatingCoroutine = StartCoroutine(HeatMetal(other.gameObject, content));
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor != null && vibrationCoroutine == null)
            {
                vibrationCoroutine = StartCoroutine(HandleVibration(interactor));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            if (heatingCoroutine != null)
            {
                StopCoroutine(heatingCoroutine);
                heatingCoroutine = null;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor != null && vibrationCoroutine != null)
            {
                StopCoroutine(vibrationCoroutine);
                vibrationCoroutine = null;
            }
        }
    }

    private IEnumerator HeatMetal(GameObject metal, GameObject content)
    {
        MetalState metalState = metal.GetComponent<MetalState>();
        if (metalState == null)
        {
            Debug.LogWarning("El objeto metal no tiene un componente MetalState.");
            yield break;
        }

        currentColor = startColor;

        // Wait for the initial delay
        yield return new WaitForSeconds(waitTime);

        content.GetComponent<Renderer>().material = heatedMaterial;

        // Gradual heating process
        while (elapsedTime < heatDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / heatDuration);

            // Exponential progression for wave scale
            float exponentialProgress = Mathf.Pow(progress, 2f);

            currentWaveScale = exponentialProgress;
            currentColor = Color.Lerp(startColor, endColor, progress);

            heatedMaterial.SetFloat("_WaveScale", currentWaveScale);
            heatedMaterial.SetColor(_DeepColorName, currentColor);

            yield return null;
        }

        // Finalize heating
        heatedMaterial.SetFloat("_WaveScale", 1f);
        heatedMaterial.SetColor(_DeepColorName, endColor);

        metalState.HeatMetal();
        heatingCoroutine = null;
    }

    private IEnumerator HandleVibration(XRDirectInteractor interactor)
    {

        float vibrationElapsed = 0f;

        while (true)
        {
            vibrationElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(vibrationElapsed / heatDuration);

            // Linearly increase vibration intensity
            float vibrationIntensity = Mathf.Lerp(0, vibrationIntensityMax, progress);
            interactor.SendHapticImpulse(vibrationIntensity, Time.deltaTime);

            yield return null;
        }
    }
}
