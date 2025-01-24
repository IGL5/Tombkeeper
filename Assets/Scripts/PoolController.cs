using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class PoolController : MonoBehaviour
{
    [Header("Water Level Settings")]
    public Transform waterTransform; // Objeto que contiene el shader del agua
    public float initialY = 0.38f; // Nivel inicial del agua
    public float finalY = 1.165f; // Nivel final del agua (completamente lleno)
    public int maxBuckets = 3; // Máximo número de cubos necesarios para llenar la piscina
    public float fillDuration = 1.0f; // Tiempo de interpolación para llenar el agua
    public GameObject ring;
    public XRGrabInteractable ankh;

    private int currentBuckets = 0; // Cubos usados hasta ahora
    private bool isFilling = false; // Para evitar múltiples llamadas simultáneas
    private float currentY; // Nivel actual del agua

    void Start()
    {
        // Inicializamos el nivel del agua
        currentY = initialY;
        waterTransform.position = new Vector3(waterTransform.position.x, currentY, waterTransform.position.z);
    }

    public void AddWater(BucketController bucket)
    {
        if (bucket.IsFilled() && currentBuckets < maxBuckets && !isFilling)
        {
            currentBuckets++;
            bucket.SetEmpty(); // Marcamos el cubo como vacío
            StartCoroutine(FillWater());

            if (currentBuckets == maxBuckets)
            {
                ring.SetActive(true);
                ankh.enabled = true;
            }
        }
        else
        {
            bucket.SetEmpty(); // Marcamos el cubo como vacío
        }
    }

    private IEnumerator FillWater()
    {
        isFilling = true;

        // Calculamos el nivel objetivo en función del número de cubos actuales
        float targetY = initialY + ((finalY - initialY) / maxBuckets) * currentBuckets;

        // Interpolamos el movimiento del agua
        float elapsedTime = 0f;
        float startY = currentY; // Inicia desde el nivel actual
        while (elapsedTime < fillDuration)
        {
            currentY = Mathf.Lerp(startY, targetY, elapsedTime / fillDuration); // Actualizamos currentY
            waterTransform.position = new Vector3(waterTransform.position.x, currentY, waterTransform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Aseguramos que el agua esté exactamente en el nivel final calculado
        currentY = targetY;
        waterTransform.position = new Vector3(waterTransform.position.x, currentY, waterTransform.position.z);

        isFilling = false;
    }
}
