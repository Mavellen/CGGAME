using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public RectTransform healthbar;
    public Light _light;

    protected float baseHealth = 10f;
    protected float currentHealth = 10f;
    private bool Activated;
    protected float multiplier = 1f;

    public virtual void Receive(float DMG)
    {
        currentHealth -= DMG;
        if (currentHealth <= 0) onDestruction();
        healthbar.sizeDelta = new Vector2(currentHealth*multiplier, healthbar.sizeDelta.y);
    }

    public bool isActivated()
    {
        return Activated;
    }
    public void setActivated(bool b)
    {
        Activated = b;
    }
    protected abstract void onDestruction();
}
