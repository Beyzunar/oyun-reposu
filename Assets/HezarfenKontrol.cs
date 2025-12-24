using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HezarfenKontrol : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float ziplamaGucu = 5f;
    public float inisHizi = 2f; 
    public float durmaNoktasiX = 5.0f; 
    
    [Header("Sınır Ayarları")]
    public float altSinirY = -4.5f; 
    public float ustSinirY = 4.5f;  
    
    [Header("UI Panelleri")]
    public GameObject tebrikObjesi; 
    public GameObject yenidenDeneObjesi; 
    
    private Rigidbody2D rb;
    private AudioSource muzukKutusu;
    [HideInInspector] public bool meydanInisiBasladi = false;
    private bool oyunBitti = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        muzukKutusu = GetComponent<AudioSource>(); 
        
        rb.gravityScale = 1.2f;
        Time.timeScale = 1; 
        
        
        if(tebrikObjesi != null) tebrikObjesi.SetActive(false);
        if(yenidenDeneObjesi != null) yenidenDeneObjesi.SetActive(false);
    }

    void Update() {
        
        if (oyunBitti && Input.GetKeyDown(KeyCode.R)) {
            Time.timeScale = 1; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (oyunBitti) return; 

        
        if (transform.position.y < altSinirY || transform.position.y > ustSinirY) {
            Yandin();
        }

        
        if (Input.GetKeyDown(KeyCode.Space) && !meydanInisiBasladi) {
            rb.linearVelocity = Vector2.up * ziplamaGucu;
        }

        
        if (meydanInisiBasladi) {
            MeydanInisMantigi();
        } else {
            
            float sinirliX = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
            transform.position = new Vector3(sinirliX, transform.position.y, 0);
        }
    }

    void MeydanInisMantigi() {
        if (transform.position.x < durmaNoktasiX) {
            transform.Translate(new Vector3(1f, -0.6f, 0) * inisHizi * Time.deltaTime);
            rb.gravityScale = 0; 
            rb.linearVelocity = Vector2.zero;
        } else {
            
            transform.position = new Vector3(durmaNoktasiX, transform.position.y, 0);
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            if (tebrikObjesi != null) tebrikObjesi.SetActive(true); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.name.Contains("WhatsApp")) {
            Yandin();
        }
    }

    void Yandin() {
        if (oyunBitti) return; 
        oyunBitti = true;
        
        
        if(muzukKutusu != null) muzukKutusu.Stop(); 
        
        Time.timeScale = 0; 
        if (yenidenDeneObjesi != null) yenidenDeneObjesi.SetActive(true); 
    }
}