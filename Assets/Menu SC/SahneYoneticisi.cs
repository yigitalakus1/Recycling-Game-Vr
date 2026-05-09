using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneYoneticisi : MonoBehaviour
{
    // Butona Unity üzerinden sahne ismini elle yazabilmemizi sağlar
    public void SahneyeGit(string sahneAdi)
    {
        SceneManager.LoadScene(sahneAdi);
    }
}