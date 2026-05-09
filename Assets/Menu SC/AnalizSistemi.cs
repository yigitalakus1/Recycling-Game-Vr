using UnityEngine;
using TMPro;

public class AnalizSistemi : MonoBehaviour
{
    [Header("UI Elemanları")]
    public TMP_Text dogruSayisiText;
    public TMP_Text yanlisSayisiText;
    public TMP_Text basariYuzdesiText;
    public TMP_Text motivasyonText;

    // Verileri diğer scriptlerden ulaşılabilir (static) yapıyoruz
    public static int toplamDogru = 0;
    public static int toplamYanlis = 0;

    // Analiz butonuna basınca bu çalışacak
    public void AnalizPaneliniGuncelle()
    {
        int toplamSoru = toplamDogru + toplamYanlis;
        float yuzde = 0;

        if (toplamSoru > 0)
        {
            yuzde = ((float)toplamDogru / toplamSoru) * 100f;
        }

        dogruSayisiText.text = "Doğru Sayısı: " + toplamDogru;
        yanlisSayisiText.text = "Yanlış Sayısı: " + toplamYanlis;
        basariYuzdesiText.text = "Başarı Yüzdesi: %" + yuzde.ToString("F0");

        // Motivasyon Mesajları
        if (yuzde >= 80) motivasyonText.text = "Harikasın! Tam bir çevre dostusun! 🌿";
        else if (yuzde >= 50) motivasyonText.text = "Güzel gidiyorsun, uzman olabilirsin! 👍";
        else motivasyonText.text = "Vazgeçme! Tekrar deneyerek dünyayı kurtarabilirsin! 💪";
    }

    // Skorları sıfırlamak istersen diye (Opsiyonel)
    public void VerileriSifirla()
    {
        toplamDogru = 0;
        toplamYanlis = 0;
        AnalizPaneliniGuncelle();
    }
}