using UnityEngine;

public class CopKutusu : MonoBehaviour
{
    // Başına "public" yazmazsak Inspector'da kutucuk çıkmaz
    public string kabulEdilenTag = "organik"; 

    private MutfakYonetici yonetici;

    void Start()
    {
        // Sahnede MutfakYonetici'sini bulur
        yonetici = Object.FindAnyObjectByType<MutfakYonetici>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // İçine giren objenin tag'ini kontrol eder
        if (other.CompareTag(kabulEdilenTag))
        {
            yonetici.CopToplandi(kabulEdilenTag);
            Destroy(other.gameObject); // Çöpü siler
        }
    }
}