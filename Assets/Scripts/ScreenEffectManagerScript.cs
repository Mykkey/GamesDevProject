using UnityEngine;

public class ScreenEffectManagerScript : MonoBehaviour
{
    public CanvasGroup panel;

    public float maxOpacity = 1;
    public float fadeSpeed = 4;

    private void Start()
    {
        panel.alpha = 0;
    }

    private void Update()
    {
        if (panel.alpha > 0)
        {
            panel.alpha -= fadeSpeed * Time.deltaTime;
        }
    }

    public void ApplyHitEffect()
    {
        panel.alpha = maxOpacity;
    }
}
