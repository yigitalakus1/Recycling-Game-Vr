using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System.IO;
using System.Text; // Encoding işlemleri için gerekli

public class OyunBitirici : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Debug.Log("Oyun durduruldu! Veriler işleniyor...");

        SahneSayaci mutfakSayaci = FindObjectOfType<SahneSayaci>();
        if (mutfakSayaci != null) mutfakSayaci.MutfakBitti(); 

        FinalKaydiniYap();
    }

    private string SureyiFormataCevir(float toplamSaniye)
    {
        int dakika = Mathf.FloorToInt(toplamSaniye / 60);
        int saniye = Mathf.FloorToInt(toplamSaniye % 60);
        return string.Format("{0:00}:{1:00}", dakika, saniye);
    }

    public void FinalKaydiniYap()
    {
        if (string.IsNullOrEmpty(KullaniciVerisi.aktifKullaniciID))
        {
            Debug.LogWarning("Kullanıcı ID bulunamadı, kayıt atlanıyor.");
            return;
        }

        KullaniciVerisi.ToplamSureyiHesapla();

        // --- 1. FIREBASE GÜNCELLEME ---
        DatabaseReference dbRef = FirebaseDatabase.GetInstance("https://vregitimtest-fa87b-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        string yol = "Kullanicilar/" + KullaniciVerisi.aktifKullaniciID;

        Dictionary<string, object> guncelVeriler = new Dictionary<string, object>();
        guncelVeriler["test_dogru"] = KullaniciVerisi.dogruSayisi;
        guncelVeriler["test_yanlis"] = KullaniciVerisi.yanlisSayisi;
        guncelVeriler["test_suresi"] = SureyiFormataCevir(KullaniciVerisi.sinavSuresi);
        guncelVeriler["bahce_suresi"] = SureyiFormataCevir(KullaniciVerisi.bahceSuresi);
        guncelVeriler["fabrika_suresi"] = SureyiFormataCevir(KullaniciVerisi.fabrikaSuresi);
        guncelVeriler["mutfak_suresi"] = SureyiFormataCevir(KullaniciVerisi.mutfakSuresi);
        guncelVeriler["sinif_suresi"] = SureyiFormataCevir(KullaniciVerisi.sinifSuresi);
        guncelVeriler["toplam_oyun_suresi"] = SureyiFormataCevir(KullaniciVerisi.toplamOyunSuresi);

        dbRef.Child(yol).UpdateChildrenAsync(guncelVeriler).ContinueWith(task => {
            if (task.IsCompleted) Debug.Log("<color=green>Firebase Güncellendi!</color>");
        });

        // --- 2. CSV DOSYASI OLUŞTURMA ---
        CSVKaydet();
    }

    void CSVKaydet()
    {
        string dosyaAdi = KullaniciVerisi.aktifKullaniciID + "_Rapor.csv";
        string klasörYolu = Application.persistentDataPath;
        string tamYol = Path.Combine(klasörYolu, dosyaAdi);

        // EXCEL DÜZELTME: sep=, satırı Excel'e ayırıcının virgül olduğunu söyler
        StringBuilder csvIcerik = new StringBuilder();
        csvIcerik.AppendLine("sep=,"); 
        csvIcerik.AppendLine("Ogrenci,Dogru,Yanlis,Test Sure,Bahce Sure,Fabrika Sure,Mutfak Sure,Sinif Sure,Toplam Sure");
        
        csvIcerik.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
            KullaniciVerisi.ogrenciIsmi,
            KullaniciVerisi.dogruSayisi,
            KullaniciVerisi.yanlisSayisi,
            SureyiFormataCevir(KullaniciVerisi.sinavSuresi),
            SureyiFormataCevir(KullaniciVerisi.bahceSuresi),
            SureyiFormataCevir(KullaniciVerisi.fabrikaSuresi),
            SureyiFormataCevir(KullaniciVerisi.mutfakSuresi),
            SureyiFormataCevir(KullaniciVerisi.sinifSuresi),
            SureyiFormataCevir(KullaniciVerisi.toplamOyunSuresi)
        );

        try 
        {
            // UTF8Encoding(true) -> Excel'in Türkçe karakterleri tanıması için gereken BOM işaretini ekler
            File.WriteAllText(tamYol, csvIcerik.ToString(), new UTF8Encoding(true));
            
            Debug.Log("<color=cyan>Düzgün CSV Oluşturuldu: </color>" + tamYol);

            // Klasörü otomatik açar
            Application.OpenURL("file://" + klasörYolu);
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSV Yazma Hatası: " + e.Message);
        }
    }
}