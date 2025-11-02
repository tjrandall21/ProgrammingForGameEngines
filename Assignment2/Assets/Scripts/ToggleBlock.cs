using NUnit.Framework;
using UnityEngine;

public class ToggleBlock : MonoBehaviour
{
    private Rigidbody2D rigidbody = null;
    private SpriteRenderer sprite = null;

    [SerializeField]
    private Color enabledColor = new Color(1f,1f,1f,1f);
    private Color disabledColor;
    private bool isEnabled;
    [SerializeField]
    private bool enabledByDefault = true;
    [SerializeField]
    private ButtonSwitch button = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        isEnabled = enabledByDefault;
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        disabledColor = enabledColor;
        disabledColor.a = 0.2f;
        updateComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (button.IsPressed() && isEnabled == enabledByDefault)
        {
            isEnabled = !enabledByDefault;
            updateComponents();
        }
        else if (!button.IsPressed() && isEnabled != enabledByDefault)
        {
            isEnabled = enabledByDefault;
            updateComponents();
        }
    }

    void updateComponents()
    {
        if (isEnabled)
        {
            rigidbody.simulated = true;
            sprite.color = enabledColor;
        }
        else
        {
            rigidbody.simulated = false;
            sprite.color = disabledColor;
        }
    }
}
