using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarFeedbackUI : MonoBehaviour
{
    private Image hpFillImage;

    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;

    [SerializeField] private float fillDuration = 0.35f;
    [SerializeField] private float colorDuration = 0.1f;

    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color damageColor = Color.red;

    [SerializeField] private float testDamage = 10f;

    private Tween fillTween;
    private Sequence colorSequence;

    private void Awake()
    {
        hpFillImage = GetComponent<Image>();

        hpFillImage.type = Image.Type.Filled;
        hpFillImage.fillMethod = Image.FillMethod.Horizontal;

        hpFillImage.color = normalColor;
        hpFillImage.fillAmount = currentHP / maxHP;
    }

    public void SetHP(float hp)
    {
        currentHP = Mathf.Clamp(hp, 0f, maxHP);

        float ratio = currentHP / maxHP;
        ratio = Mathf.Clamp01(ratio);

        fillTween?.Kill();

        fillTween = hpFillImage
            .DOFillAmount(ratio, fillDuration)
            .SetEase(Ease.OutQuad);
    }
    public void Damage()
    {
        SetHP(currentHP - testDamage);
        PlayDamageEffect();
    }

    public void Heal()
    {
        SetHP(currentHP + testDamage);
    }

    private void PlayDamageEffect()
    {
        colorSequence?.Kill();

        hpFillImage.color = normalColor;

        colorSequence = DOTween.Sequence();

        colorSequence.Append(
            hpFillImage.DOColor(damageColor, colorDuration)
        );

        colorSequence.Append(
            hpFillImage.DOColor(normalColor, colorDuration)
        );
    }

    private void OnDestroy()
    {
        fillTween?.Kill();
        colorSequence?.Kill();
    }
}