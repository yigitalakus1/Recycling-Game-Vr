using UnityEngine;

public class TruckController : MonoBehaviour
{
    public WasteType acceptedType;

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDistance = 5f;

    [Header("Audio")]
    public AudioSource engineAudio;

    private bool hasLeft = false;
    private bool isMoving = false;
    private Vector3 targetPosition;

    public bool HasLeft()
    {
        return hasLeft;
    }

    void Start()
    {
        targetPosition = transform.position + transform.forward * moveDistance;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void SendTruck()
    {
        if (!hasLeft)
        {
            hasLeft = true;
            isMoving = true;

            if (engineAudio != null)
            {
                engineAudio.Stop(); // Eğer hali hazırda çalıyorsa durdurup baştan başlatır
                engineAudio.Play();
                
                // YENİ: Sesi 2 saniye sonra durdurması için zamanlayıcıyı kuruyoruz
                Invoke("SesiKapat", 2f);
            }

            Debug.Log(gameObject.name + " truck moving. Sound will stop in 2 seconds.");
        }
    }

    // YENİ: Sesi durduracak yardımcı fonksiyon
    void SesiKapat()
    {
        if (engineAudio != null)
        {
            engineAudio.Stop();
            Debug.Log(gameObject.name + " truck sound stopped after 2 seconds.");
        }
    }
}