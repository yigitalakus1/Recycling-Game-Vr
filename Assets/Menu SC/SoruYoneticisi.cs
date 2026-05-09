using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SoruBankasi 
{
    public string soruMetni;
    public string[] secenekler = new string[4];
    public int dogruCevapIndex;
}

public class SoruYoneticisi : MonoBehaviour
{
    [Header("UI Panelleri")]
    public GameObject panelTestSecim; 
    public GameObject panelSoruEkrani;
    public GameObject siklarGrubu; 

    [Header("Soru Elemanları")]
    public TMP_Text soruYazisi;
    public Image[] butonResimleri;
    public TMP_Text[] butonYazilari;

    [Header("Soru Listeleri")]
    public List<SoruBankasi> temelSorular = new List<SoruBankasi>();
    public List<SoruBankasi> ortaSorular = new List<SoruBankasi>();
    public List<SoruBankasi> ileriSorular = new List<SoruBankasi>();

    [Header("Süre Ayarları")]
    public TMP_Text sureYazisi; 
    private float gecenSure = 0f;
    private bool sureAkiyor = false;

    private List<SoruBankasi> aktifListe;
    private int mevcutSoruIndex = 0;
    private bool cevapVerildiMi = false;

    void Update()
    {
        if (sureAkiyor)
        {
            gecenSure += Time.deltaTime;
            ZamaniGuncelle();
        }
    }

    void ZamaniGuncelle()
    {
        if (sureYazisi == null) return;
        int dakika = Mathf.FloorToInt(gecenSure / 60);
        int saniye = Mathf.FloorToInt(gecenSure % 60);
        sureYazisi.text = string.Format("Süre: {0:00}:{1:00}", dakika, saniye);
    }

    public void TestiBaslat(int seviye)
    {
        if (seviye == 0) aktifListe = temelSorular;
        else if (seviye == 1) aktifListe = ortaSorular;
        else aktifListe = ileriSorular;

        if (aktifListe == null || aktifListe.Count == 0) return;

        mevcutSoruIndex = 0;
        cevapVerildiMi = false;

        gecenSure = 0f;
        sureAkiyor = true;

        panelTestSecim.SetActive(false); 
        panelSoruEkrani.SetActive(true);
        if (siklarGrubu != null) siklarGrubu.SetActive(true); 
        
        SoruGoster();
    }

    void SoruGoster()
    {
        cevapVerildiMi = false;
        foreach (var img in butonResimleri) if(img != null) img.color = Color.white;

        if (mevcutSoruIndex < aktifListe.Count)
        {
            soruYazisi.text = aktifListe[mevcutSoruIndex].soruMetni;
            for (int i = 0; i < 4; i++)
            {
                if(i < butonYazilari.Length)
                    butonYazilari[i].text = aktifListe[mevcutSoruIndex].secenekler[i];
            }
        }
        else
        {
            // FİREBASE DÜZELTMESİ: Test başarıyla bittiğinde süreyi ÜSTÜNE EKLE (+=)
            sureAkiyor = false;
            KullaniciVerisi.sinavSuresi += gecenSure; 

            soruYazisi.text = "Tebrikler! Testi Başarıyla Bitirdiniz.";
            if (siklarGrubu != null) siklarGrubu.SetActive(false); 
            Invoke("Btn_Kapat_Tikla", 3f); 
        }
    }

    public void CevapVer(int secilenIndex)
    {
        if (cevapVerildiMi || aktifListe == null) return;
        cevapVerildiMi = true;

        int dogruIndex = aktifListe[mevcutSoruIndex].dogruCevapIndex;

        if (secilenIndex == dogruIndex)
        {
            butonResimleri[secilenIndex].color = Color.green;
            AnalizSistemi.toplamDogru++; 
            KullaniciVerisi.dogruSayisi++; 
        }
        else
        {
            butonResimleri[secilenIndex].color = Color.red;
            butonResimleri[dogruIndex].color = Color.green;
            AnalizSistemi.toplamYanlis++; 
            KullaniciVerisi.yanlisSayisi++; 
        }

        StartCoroutine(SonrakiSoruyaGec());
    }

    IEnumerator SonrakiSoruyaGec()
    {
        yield return new WaitForSeconds(2f);
        mevcutSoruIndex++;
        SoruGoster();
    }

    public void Btn_Kapat_Tikla()
    {
        // FİREBASE DÜZELTMESİ: Eğer test bitmeden yarıda çarpıya basılıp çıkılırsa,
        // o ana kadar geçen süreyi yine de çantanın üstüne ekle.
        if (sureAkiyor)
        {
            KullaniciVerisi.sinavSuresi += gecenSure;
            sureAkiyor = false;
        }

        if (panelSoruEkrani != null) panelSoruEkrani.SetActive(false);
        
        if (panelTestSecim != null) 
        {
            panelTestSecim.SetActive(true);
            
            Transform[] cocuklar = panelTestSecim.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in cocuklar)
            {
                t.gameObject.SetActive(true);
            }
        }

        if (siklarGrubu != null) siklarGrubu.SetActive(true); 
        soruYazisi.text = ""; 
        
        Debug.Log("Sistem: Navigasyon tamamlandı, butonlar aktif edildi.");
    }
}