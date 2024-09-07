using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;  // Düşman prefab'ı
    public int enemyCount = 5;  // Başlangıçta oluşturulacak düşman sayısı
    public GameObject winPanel;  // Kazandınız mesajını içeren panel
    public CarController carController;  // Araba kontrolcüsü
    private int remainingEnemies;  // Kalan düşman sayısı

    void Start()
    {
        remainingEnemies = enemyCount;
        winPanel.SetActive(false);  // Başlangıçta kazanma panelini gizle

        // İlk yüklemede düşmanları oluştur
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().enemyManager = this;  // EnemyManager referansını ver
        }
    }

    // Düşman yenildiğinde çağrılacak fonksiyon
    public void EnemyDefeated()
    {
        remainingEnemies--;
        if (remainingEnemies <= 0)
        {
            Win();
        }
    }

    // Kazandığınızda çağrılacak fonksiyon
    void Win()
    {
        winPanel.SetActive(true);  // Kazandınız panelini göster
        carController.StartMovingToPosition(0f);  // Arabayı X=0'a hareket ettir
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(-0.5f, 0.5f);
        float randomY = Random.Range(-0.5f, 0.5f);
        return new Vector2(randomX, randomY);
    }
}
