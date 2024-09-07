using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 5f;  // Arabanın hareket hızı
    private float targetX;  // Arabanın hedef x pozisyonu
    private bool shouldMove = false;  // Arabanın hareket edip etmeyeceğini kontrol eden bayrak

    void Start()
    {
        // Araba başlangıçta X=-3 pozisyonunda duracak
        transform.position = new Vector3(-3f, transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (shouldMove)
        {
            // X ekseninde aracı hedef pozisyona doğru hareket ettir
            float step = speed * Time.deltaTime;  // Her karede ne kadar hareket edeceğini hesapla
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), step);

            // Eğer araç hedef pozisyona ulaştıysa durdur
            if (transform.position.x == targetX)
            {
                shouldMove = false;
            }
        }
    }

    // Arabanın hedef pozisyona doğru hareket etmesini başlatmak için çağırılır
    public void StartMovingToPosition(float newTargetX)
    {
        targetX = newTargetX;  // Hedef pozisyonu belirle
        shouldMove = true;  // Araba hareket etmeye başlar
    }
}
