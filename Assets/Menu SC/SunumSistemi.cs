using UnityEngine;
using UnityEngine.UI;

public class SunumSistemi : MonoBehaviour
{
    public Image tahtaResmi; // Resmin görüneceği yer
    public Sprite[] slaytlar; // 1, 2, 3... diye giden resimlerin
    private int suankiSayfa = 0;

    public void SonrakiSayfa()
    {
        suankiSayfa++;
        
        // Eğer son sayfayı geçtiysek başa dön
        if (suankiSayfa >= slaytlar.Length) 
            suankiSayfa = 0;

        tahtaResmi.sprite = slaytlar[suankiSayfa];
    }
}