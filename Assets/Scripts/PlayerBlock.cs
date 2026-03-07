using UnityEngine;
using UnityEngine.UI;

public class PlayerBlock : MonoBehaviour
{
    [Header("Animasyon")]
    public Animator handRAnimator;
    public Animator handLAnimator;
    public string blockBoolName = "IsBlocking";

    [Header("Blok Ayarları")]
    public float maxBlockHP = 40f;
    public float blockRegenPerSecond = 4f;

    [Header("UI")]
    public Image blockBarFill;
    public CanvasGroup blockBarGroup;

    private float _currentBlockHP;
    private bool _isBlocking = false;
    public bool IsBlocking => _isBlocking;

    void Awake()
    {
        _currentBlockHP = maxBlockHP;
    }

    void Update()
    {
        if (!_isBlocking)
        {
            _currentBlockHP += blockRegenPerSecond * Time.deltaTime;
            _currentBlockHP = Mathf.Min(_currentBlockHP, maxBlockHP);

            float targetAlpha = (_currentBlockHP >= maxBlockHP - 0.1f)
                ? 0f
                : 0.4f;

            blockBarGroup.alpha = Mathf.Lerp(
                blockBarGroup.alpha, targetAlpha, Time.deltaTime * 5f);
        }
        else
        {
            blockBarGroup.alpha = Mathf.Lerp(
                blockBarGroup.alpha, 1f, Time.deltaTime * 5f);
        }

        if (blockBarFill != null)
            blockBarFill.fillAmount = _currentBlockHP / maxBlockHP;
    }

    public void SetBlocking(bool blocking)
    {
        _isBlocking = blocking;

        if (handRAnimator != null)
            handRAnimator.SetBool(blockBoolName, blocking);
        if (handLAnimator != null)
            handLAnimator.SetBool(blockBoolName, blocking);
    }

    public void AbsorbHit(float damage)
    {
        _currentBlockHP -= damage;
        _currentBlockHP = Mathf.Max(_currentBlockHP, 0f);

        if (_currentBlockHP <= 0f)
            SetBlocking(false);
    }
}