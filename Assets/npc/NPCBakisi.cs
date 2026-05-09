using UnityEngine;

public class NPCBakisi : MonoBehaviour
{
    public Transform oyuncu; // XR Origin altındaki Main Camera'yı buraya sürükle

    void Update()
    {
        if (oyuncu == null) return;

        // NPC sadece Y ekseninde (kendi etrafında) oyuncuya dönsün
        Vector3 hedefYon = oyuncu.position - transform.position;
        hedefYon.y = 0; // NPC'nin öne/arkaya yatmasını engeller
        
        if (hedefYon != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(hedefYon);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
        }
    }
}