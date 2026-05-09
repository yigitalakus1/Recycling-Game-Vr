using UnityEngine;
using TMPro;

public class GorevYoneticisi : MonoBehaviour
{
    public static GorevYoneticisi instance;

    [Header("UI Elemanlari")]
    public GameObject baslangicButonu;
    public TextMeshProUGUI puanText1; 
    public TextMeshProUGUI puanText2;

    [Header("Gorev Tikleri")]
    public GameObject tikGorev1; 
    public GameObject tikGorev2;

    [Header("Puan Ayarlari")]
    public int mevcutPuan = 0;
    public int hedefPuan = 100;
    private bool ajandayaHaberVerildi = false;

    [HideInInspector]
    public bool gorevBasladi = false;
    private Rigidbody[] tumCopler;

    void Awake() 
    { 
        if (instance == null) instance = this; 
    }

    void Start()
    {
        mevcutPuan = 0;
        ajandayaHaberVerildi = false;
        PuanYazisiniGuncelle();

        GameObject[] copObjs = GameObject.FindGameObjectsWithTag("cop");
        tumCopler = new Rigidbody[copObjs.Length];
        for (int i = 0; i < copObjs.Length; i++)
        {
            if (copObjs[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                tumCopler[i] = rb;
                rb.isKinematic = true;
            }
        }
    }

    public void GoreviBaslat()
    {
        gorevBasladi = true;
        if(baslangicButonu) baslangicButonu.SetActive(false);
        PuanYazisiniGuncelle();
        foreach (Rigidbody rb in tumCopler) 
        { 
            if (rb != null) rb.isKinematic = false; 
        }
    }

    public void PuanArtir(int miktar)
    {
        if (!gorevBasladi) return;
        
        mevcutPuan += miktar;
        PuanYazisiniGuncelle();
        
        if (mevcutPuan >= hedefPuan && !ajandayaHaberVerildi)
        {
            ajandayaHaberVerildi = true;
            if (tikGorev1) tikGorev1.SetActive(true);
        }
    }

    void PuanYazisiniGuncelle()
    {
        string text = "PUAN: " + mevcutPuan.ToString();
        if(puanText1) puanText1.text = text;
        if(puanText2) puanText2.text = text;
    }
}