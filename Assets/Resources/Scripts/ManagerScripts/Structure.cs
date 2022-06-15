using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Structure : MonoBehaviour
{
    public RectTransform healthbar;

    protected float baseHealth = 10f;
    protected float currentHealth = 10f;
    [System.NonSerialized] public bool Activated = false;
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

    protected virtual void onDestruction()
    {
        Debug.Log("Game Over");
        LevelGenerator.removeMesh();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
