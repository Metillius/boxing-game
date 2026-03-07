using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    // ─────────────────────────────────────────
    // INSPECTOR'DAN AYARLANABİLİR DEĞERLER
    // ─────────────────────────────────────────

    [Header("Stamina Settings")]
    [Tooltip("Oyun başındaki maksimum stamina (tüm zamanların tavanı)")]
    public float maxStamina = 100f;
    public float StaminaPercent => _currentStamina / maxStamina;


    [Tooltip("Max stamina'nın düşebileceği en alt sınır")]
    public float minMaxStamina = 10f;

    [Tooltip("Saniyede ne kadar stamina yenilensin")]
    public float staminaRegenPerSecond = 2f;

    [Header("Stamina Costs")]
    public float punchCost = 3f;
    public float jumpCost = 6f;
    public float damageStaminaCost = 10f;      
    public float damageMaxStaminaReduction = 4f; 

    [Header("UI References")]
    [Tooltip("Sarı bar Image bileşeni — current stamina'yı gösterir")]
    public Image yellowFillImage;

    [Tooltip("Gri bar Image bileşeni — ulaşılabilir max kapasiteyi gösterir")]
    public Image greyCapacityImage;



    public float _currentStamina;      
    public float _currentMaxStamina;   
    public float CurrentStamina    => _currentStamina;
    public float CurrentMaxStamina => _currentMaxStamina;



    void Awake()
    {
        _currentMaxStamina = maxStamina;
        _currentStamina    = maxStamina;
    }

    void Update()
    {
        if (_currentStamina <= 0)
        {
            GameManager.Instance.PlayerDied(gameObject.tag);
            Destroy(gameObject);
        }
        // stamina regen
        _currentStamina += staminaRegenPerSecond * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _currentMaxStamina);
        RefreshUI();
    }



    public void UsePunchStamina()
    {
        _currentStamina -= punchCost;
        _currentStamina  = Mathf.Max(_currentStamina, 0f); 
    }


    public void UseJumpStamina()
    {
        _currentStamina -= jumpCost;
        _currentStamina  = Mathf.Max(_currentStamina, 0f);
    }

    public void ReceiveDamageStamina()
    {
        _currentStamina -= damageStaminaCost;
        _currentStamina  = Mathf.Max(_currentStamina, 0f);

        _currentMaxStamina -= damageMaxStaminaReduction;
        _currentMaxStamina  = Mathf.Max(_currentMaxStamina, minMaxStamina);

        _currentStamina = Mathf.Min(_currentStamina, _currentMaxStamina);
    }



    void RefreshUI()
    {
        if (yellowFillImage != null)
            yellowFillImage.fillAmount = _currentStamina / maxStamina;


        if (greyCapacityImage != null)
            greyCapacityImage.fillAmount = _currentMaxStamina / maxStamina;
    }
}