using UnityEngine;

public class HavuzGorevTakip : MonoBehaviour
{
    [Header("1. Görev: Bahçe (Tag: cop)")]
    public int gerekenBahceCopu = 5;
    private int toplananBahceCopu = 0;
    public GameObject tikGorev1; // Ajandadaki 1. Tik (Checkmark)

    [Header("2. Görev: Havuz (Tag: su_copu)")]
    public int gerekenSuCopu = 1;
    private int toplananSuCopu = 0;
    public GameObject tikGorev2; // Ajandadaki 2. Tik (Checkmark)

    private void OnTriggerEnter(Collider other)
    {
        // --- BAHÇE ÇÖPLERİ İÇİN ---
        if (other.CompareTag("cop"))
        {
            Destroy(other.gameObject);
            toplananBahceCopu++;
            
            Debug.Log("Bahçe Çöpü Toplandı! (" + toplananBahceCopu + "/" + gerekenBahceCopu + ")");

            if (toplananBahceCopu >= gerekenBahceCopu && tikGorev1 != null)
            {
                tikGorev1.SetActive(true); // 1. Görevi tamamla
                Debug.Log("BAHÇE TEMİZLİĞİ BİTTİ!");
            }
        }
        
        // --- HAVUZ ÇÖPLERİ İÇİN ---
        else if (other.CompareTag("su_copu"))
        {
            Destroy(other.gameObject);
            toplananSuCopu++;
            
            Debug.Log("Havuz Çöpü Toplandı! (" + toplananSuCopu + "/" + gerekenSuCopu + ")");

            if (toplananSuCopu >= gerekenSuCopu && tikGorev2 != null)
            {
                tikGorev2.SetActive(true); // 2. Görevi tamamla
                Debug.Log("HAVUZ TEMİZLİĞİ BİTTİ!");
            }
        }
    }
}