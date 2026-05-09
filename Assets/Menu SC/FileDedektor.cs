using UnityEngine;

public class FileDedektor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Eğer file suya attığımız "su_copu" etiketli bir şeye değerse
        if (other.CompareTag("su_copu"))
        {
            // Çöpü fileye "yapıştır" (Parent yap)
            // Böylece oyuncu fileyi çekince çöp de onunla gelir
            other.transform.SetParent(this.transform);
            
            // Çöpün kendi hareketini/fiziğini durdur ki fileden düşmesin
            if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            
            Debug.Log("Çöp yakalandı! Şimdi sepete götür.");
        }
    }
}