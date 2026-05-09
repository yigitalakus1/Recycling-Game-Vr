using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database; 

public class KullaniciVerisi : MonoBehaviour
{
    [Header("Giriş Alanları")]
    public TMP_InputField adInput;
    public TMP_InputField soyadInput;
    public TMP_InputField yasInput;

    [Header("Panel Yönetimi")]
    public GameObject girisPaneli;
    public GameObject anaMenu;
    public TMP_Text profilIsimText; 

    [Header("Mevcut Skorlar ve Süreler")]
    public static int dogruSayisi = 0;
    public static int yanlisSayisi = 0;
    public static float sinavSuresi = 0f;
    public static float bahceSuresi = 0f;
    public static float fabrikaSuresi = 0f;
    public static float mutfakSuresi = 0f;
    public static float sinifSuresi = 0f; // YENİ EKLENDİ
    public static float toplamOyunSuresi = 0f; 
    
    public static string aktifKullaniciID = ""; 
    public static string ogrenciIsmi = ""; 

    public static float baslangicZamani = 0f; 

    DatabaseReference dbReference;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        dbReference = FirebaseDatabase.GetInstance("https://vregitimtest-fa87b-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        
        if (aktifKullaniciID != "")
        {
            girisPaneli.SetActive(false); 
            anaMenu.SetActive(true); 
            if (profilIsimText != null) profilIsimText.text = "Öğrenci: " + ogrenciIsmi;
        }
        else
        {
            girisPaneli.SetActive(true);
            anaMenu.SetActive(false);
        }
    }

    public void VerileriKaydet()
    {
        string ad = adInput.text;
        string soyad = soyadInput.text;
        string yas = yasInput.text;

        aktifKullaniciID = ad + "_" + soyad;
        ogrenciIsmi = ad + " " + soyad; 

        baslangicZamani = Time.time; 

        // Sınıf süresi de eklendi
        UserData yeniKullanici = new UserData(ad, soyad, yas, dogruSayisi, yanlisSayisi, sinavSuresi, bahceSuresi, fabrikaSuresi, mutfakSuresi, sinifSuresi, toplamOyunSuresi);
        string json = JsonUtility.ToJson(yeniKullanici);
        dbReference.Child("Kullanicilar").Child(aktifKullaniciID).SetRawJsonValueAsync(json);

        profilIsimText.text = "Öğrenci: " + ogrenciIsmi; 
        girisPaneli.SetActive(false); 
        anaMenu.SetActive(true);      
    }

    public static void ToplamSureyiHesapla()
    {
        toplamOyunSuresi = Time.time - baslangicZamani;
    }
}

[System.Serializable]
public class UserData {
    public string ad;
    public string soyad;
    public string yas;
    public int test_dogru;
    public int test_yanlis;
    public float test_suresi;
    public float bahce_suresi;
    public float fabrika_suresi;
    public float mutfak_suresi;
    public float sinif_suresi; // YENİ EKLENDİ
    public float toplam_oyun_suresi;

    public UserData(string ad, string soyad, string yas, int dogru, int yanlis, float testSure, float bahceSure, float fabrikaSure, float mutfakSure, float sinifSure, float toplamSure) {
        this.ad = ad;
        this.soyad = soyad;
        this.yas = yas;
        this.test_dogru = dogru;
        this.test_yanlis = yanlis;
        this.test_suresi = testSure;
        this.bahce_suresi = bahceSure;
        this.fabrika_suresi = fabrikaSure;
        this.mutfak_suresi = mutfakSure;
        this.sinif_suresi = sinifSure; // YENİ EKLENDİ
        this.toplam_oyun_suresi = toplamSure;
    }
}