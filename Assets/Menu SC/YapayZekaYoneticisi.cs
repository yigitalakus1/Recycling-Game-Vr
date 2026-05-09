using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI; // ScrollRect ve Canvas için gerekli

public class YapayZekaYoneticisi : MonoBehaviour
{
    [Header("UI Elemanları")]
    public TMP_InputField soruInput;
    public TMP_Text cevapText; 
    public ScrollRect scrollRect; // Inspector'dan Scroll View'ı buraya sürükle

    private string apiKey = "AIzaSyBxf3qEFCpzgjh4gWKdBY_zGFkfth4kzDQ";
    private string apiURL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3-flash-preview:generateContent?key=";

    public void SoruyuGonder()
    {
        if (!string.IsNullOrEmpty(soruInput.text))
        {
            string soru = soruInput.text;
            
            // Mesajı sağa yaslı ekle
            cevapText.text += "\n\n<align=right><color=#00FF00><b>Sen:</b></color> " + soru + "</align>";
            
            StartCoroutine(PostToGemini(soru));
            soruInput.text = "";
            
            // Kullanıcı mesajından hemen sonra aşağı kaydır
            ScrollEnAsagi();
        }
    }

    IEnumerator PostToGemini(string soru)
    {
        string jsonPayload = "{\"contents\":[{\"parts\":[{\"text\":\"" + soru + "\"}]}]}";

        using (UnityWebRequest request = new UnityWebRequest(apiURL + apiKey, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                string temizCevap = ParseGeminiResponse(responseText);
                
                // Gemini cevabını sola yaslı ekle
                cevapText.text += "\n\n<align=left><color=#FFFF00><b>Gemini:</b></color> " + temizCevap + "</align>";
            }
            else
            {
                cevapText.text += "\n\n<color=red>Hata: Gemini'ye ulaşılamadı!</color>";
            }
            
            // Cevap geldikten sonra en aşağı kaydır
            ScrollEnAsagi();
        }
    }

    // B kısmındaki otomatik kaydırma fonksiyonu
    void ScrollEnAsagi()
    {
        // Unity'nin UI elemanlarını yeniden hesaplaması için bir kare bekletiyoruz
        Canvas.ForceUpdateCanvases();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    string ParseGeminiResponse(string json)
    {
        try {
            string aranan = "\"text\": \"";
            int start = json.IndexOf(aranan) + aranan.Length;
            int end = json.IndexOf("\"", start);
            return json.Substring(start, end - start).Replace("\\n", "\n").Replace("\\\"", "\"");
        } catch { return "Cevap ayrıştırılamadı."; }
    }
}