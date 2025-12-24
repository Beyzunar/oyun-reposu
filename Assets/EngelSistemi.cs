using UnityEngine;
using System.Collections.Generic;

public class EngelSistemi : MonoBehaviour
{
    public List<GameObject> gemiPrefabları; 
    public GameObject kizKulesiPrefab;      
    public float olusturmaSuresi = 3f;      
    [HideInInspector] public bool olusturmaDursun = false; 
    private float timer;
    private bool kizKulesiCikti = false;

    void Update() {
        if (olusturmaDursun) return;
        timer += Time.deltaTime;
        if (timer >= olusturmaSuresi) {
           
            GameObject secilen = (!kizKulesiCikti && Time.time > 15f) ? kizKulesiPrefab : gemiPrefabları[Random.Range(0, gemiPrefabları.Count)];
            if (secilen == kizKulesiPrefab) kizKulesiCikti = true;

            Instantiate(secilen, transform.position, Quaternion.identity).AddComponent<GemiMover>();
            timer = 0;
        }
    }
}

public class GemiMover : MonoBehaviour {
    void Update() {
        transform.Translate(Vector2.left * 5f * Time.deltaTime);
        if (transform.position.x < -25f) Destroy(gameObject);
    }
}