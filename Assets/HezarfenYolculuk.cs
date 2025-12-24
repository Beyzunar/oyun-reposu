using UnityEngine;

public class HazarfenYolculuk : MonoBehaviour
{
    public float kaymaHizi = 0.5f;
    public float durmaNoktasiX = -12f;
    public EngelSistemi engelSistemi;
    public HezarfenKontrol hezarfen;

    void Update() {
        if (transform.position.x > durmaNoktasiX) {
            transform.Translate(Vector3.left * kaymaHizi * Time.deltaTime);
        } else {
            if (engelSistemi) engelSistemi.olusturmaDursun = true;
            if (hezarfen) hezarfen.meydanInisiBasladi = true;
        }
    }
}