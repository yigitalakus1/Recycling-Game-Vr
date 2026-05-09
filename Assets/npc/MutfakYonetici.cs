using UnityEngine;
using TMPro;

public class MutfakYonetici : MonoBehaviour
{
    public int hedefCopSayisi = 5; 
    private int toplananCop = 0;

    [Header("NPC Bağlantıları")]
    public TextMeshProUGUI npcYazisi; 
    public AudioSource npcSes; // NPC'deki AudioSource'u buraya sürükle

    void Start()
    {
        npcYazisi.text = "Selam! Mutfağımız biraz dağılmış. Şu 5 parça çöpü kutuya atarak bana yardım eder misin?";
    }

    public void CopToplandi(string etiket)
    {
        if (etiket == "organik")
        {
            toplananCop++;
            NpcMesajiniGuncelle();
        }
    }

    void NpcMesajiniGuncelle()
    {
        if (toplananCop < hedefCopSayisi)
        {
            npcYazisi.text = "Harika gidiyorsun! " + toplananCop + ". çöpü de hallettin.";
        }
        else
        {
            // BİTİŞ PARAGRAFI
            npcYazisi.text = "Mükemmel bir iş çıkardın! Mutfağımız senin sayende pırıl pırıl oldu. " +
                             "Çevremizi temiz tutmak sadece görsel bir güzellik değil, geleceğimize olan saygımızdır. " +
                             "Bu küçük adım, dünyayı daha yaşanabilir kılmak için verdiğin büyük bir söz aslında. " +
                             "Yardımların için çok teşekkür ederim, iyi ki varsın!";
            
            // SESİ ÇAL
            if(npcSes != null) npcSes.Play();
        }
    }
}