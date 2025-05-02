using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy3 : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    [Header("Movement")]
    public float speed = 5f;
    private float originalSpeed;
    private float slowMultiplier = 1f;

    [Header("Enemy Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    // UI references
    private Button gameOverButton;
    private TextMeshProUGUI gameOverText;
    private Button nextWaveButton;

    void Start()
    {
        originalSpeed = speed;
        currentHealth = maxHealth;
        target = Waypoints_script.points[0];

        // Find inactive children under Canvas
        Transform canvas = GameObject.Find("Canvas")?.transform;
        if (canvas != null)
        {
            Transform panel = canvas.Find("GameOverPanel");
            if (panel != null) gameOverButton = panel.GetComponent<Button>();

            Transform text = canvas.Find("GameOverText");
            if (text != null) gameOverText = text.GetComponent<TextMeshProUGUI>();

            Transform waveBtn = canvas.Find("NextWaveButton");
            if (waveBtn != null) nextWaveButton = waveBtn.GetComponent<Button>();
        }

        // Debug warnings
        if (gameOverButton == null) Debug.LogWarning("GameOverPanel (Button) not found.");
        if (gameOverText == null) Debug.LogWarning("GameOverText (TextMeshProUGUI) not found.");
        if (nextWaveButton == null) Debug.LogWarning("NextWaveButton (Button) not found.");
    }

    void Update()
    {
        float currentSpeed = originalSpeed * slowMultiplier;
        MoveToNextWaypoint(currentSpeed);
        slowMultiplier = 1f;
    }

    void MoveToNextWaypoint(float currentSpeed)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        wavepointIndex++;
        if (wavepointIndex >= Waypoints_script.points.Length)
        {
            GameManager.instance.EnemyReachedEnd();
            Die();
            return;
        }

        target = Waypoints_script.points[wavepointIndex];
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.instance.EarnMoney(5);

        if (GameManager.instance.enemiesAllowedThrough > 0)
        {
            Debug.Log("Victory! Final boss defeated and player survived.");

            if (gameOverButton != null)
                gameOverButton.gameObject.SetActive(true);

            if (gameOverText != null)
                gameOverText.text = "You Win!";
                gameOverText.gameObject.SetActive(true);

            if (nextWaveButton != null)
            {
                nextWaveButton.interactable = true;
                TextMeshProUGUI buttonText = nextWaveButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                    buttonText.text = "Victory!";
            }
        }

        Destroy(gameObject);
    }

    public void ApplySlow(float factor)
    {
        slowMultiplier = Mathf.Min(slowMultiplier, factor);
    }
}
