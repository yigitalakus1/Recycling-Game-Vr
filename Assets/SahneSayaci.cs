using UnityEngine;

public class SahneSayaci : MonoBehaviour
{
    [Header("Sayac Ayari")]
    [Tooltip("Eğer tikliyse, sahne açılır açılmaz süre başlar (Fabrika/Mutfak gibi). Tikli değilse butona basmanı bekler (Bahçe gibi).")]
    public bool baslarkenOtomatikSay = false; 

    private float gecenSure = 0f;
    private bool sayacAktif = false;

    void Start()
    {
        if (baslarkenOtomatikSay)
        {
            SayaciBaslat();
        }
    }

    void Update()
    {
        if (sayacAktif)
        {
            gecenSure += Time.deltaTime;
        }
    }

    public void SayaciBaslat()
    {
        gecenSure = 0f;
        sayacAktif = true;
        Debug.Log(gameObject.name + " sahnesinde süre sayımı başladı!");
    }

    // --- GÖREVLER BİTTİĞİNDE ÇAĞRILACAK KAYIT FONKSİYONLARI ---

    public void SinifBitti() // YENİ EKLENDİ
    {
        sayacAktif = false;
        KullaniciVerisi.sinifSuresi = gecenSure;
        Debug.Log("Sınıf tamamlandı! Süre: " + gecenSure);
    }

    public void BahceBitti()
    {
        sayacAktif = false;
        KullaniciVerisi.bahceSuresi = gecenSure; 
        Debug.Log("Bahçe tamamlandı! Süre: " + gecenSure);
    }

    public void FabrikaBitti()
    {
        sayacAktif = false;
        KullaniciVerisi.fabrikaSuresi = gecenSure; 
        Debug.Log("Fabrika tamamlandı! Süre: " + gecenSure);
    }

    public void MutfakBitti()
    {
        sayacAktif = false;
        KullaniciVerisi.mutfakSuresi = gecenSure; 
        Debug.Log("Mutfak tamamlandı! Süre: " + gecenSure);
    }
}