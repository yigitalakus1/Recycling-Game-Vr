using UnityEngine;

public class BahceSistemYoneticisi : MonoBehaviour
{
    public static BahceSistemYoneticisi instance;

    [Header("Panel ve Başlangıç Ayarları")]
    public GameObject baslangicPaneli; 
    public GameObject puanTablasi1;     
    public GameObject puanTablasi2;     
    public GameObject ajandaPaneli; 

    [Header("Görev 1 (Geri Dönüşüm) Ayarları")]
    public int mevcutPuan = 0;
    public int hedefPuan = 100; // Kaç puanda tik yansın?
    public GameObject tikGeriDonusum; // Checkmark1 objesi
    private bool gorev1Bitti = false;

    [Header("Görev 2 Ayarları")]
    public GameObject tikGorev2; // Checkmark2 objesi
    private bool gorev2Bitti = false;

    [HideInInspector]
    public bool oyunBasladi = false;
    private Rigidbody[] tumCopRigidbodileri;

    void Awake()
    {
        // Singleton kurulumu
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Başlangıç değerlerini sıfırla
        mevcutPuan = 0;
        gorev1Bitti = false;
        gorev2Bitti = false;
        oyunBasladi = false;

        // Panelleri ayarla
        if(baslangicPaneli) baslangicPaneli.SetActive(true);
        if(puanTablasi1) puanTablasi1.SetActive(false);
        if(puanTablasi2) puanTablasi2.SetActive(false);
        
        // Tikleri başlangıçta gizle
        if(tikGeriDonusum) tikGeriDonusum.SetActive(false);
        if(tikGorev2) tikGorev2.SetActive(false);

        // Sahnedeki çöpleri dondur
        GameObject[] copler = GameObject.FindGameObjectsWithTag("cop");
        tumCopRigidbodileri = new Rigidbody[copler.Length];
        for (int i = 0; i < copler.Length; i++)
        {
            if (copler[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                tumCopRigidbodileri[i] = rb;
                rb.isKinematic = true; 
            }
        }
    }

    public void GoreviBaslat()
    {
        oyunBasladi = true;
        if(baslangicPaneli) baslangicPaneli.SetActive(false);
        if(puanTablasi1) puanTablasi1.SetActive(true);
        if(puanTablasi2) puanTablasi2.SetActive(true);

        foreach (Rigidbody rb in tumCopRigidbodileri)
        {
            if (rb != null) rb.isKinematic = false;
        }
    }

    // ARTIK HATA VEREN KISIM BURASIYLA DEĞİŞTİ
    public void PuanArtir(int miktar)
    {
        if (!oyunBasladi || gorev1Bitti) return;

        mevcutPuan += miktar;
        Debug.Log("Puan: " + mevcutPuan);

        if (mevcutPuan >= hedefPuan && !gorev1Bitti)
        {
            gorev1Bitti = true;
            // Dışarıdan script çağırmıyoruz, direkt kendi içindeki tiki açıyoruz
            if (tikGeriDonusum != null) 
            {
                tikGeriDonusum.SetActive(true);
                Debug.Log("<color=green>GÖREV 1 TAMAMLANDI!</color>");
            }
        }
    }

    // 2. Görev için fonksiyon (İleride kullanmak için hazır)
    public void Gorev2Tamamla()
    {
        if (!gorev2Bitti && tikGorev2 != null)
        {
            gorev2Bitti = true;
            tikGorev2.SetActive(true);
        }
    }
}