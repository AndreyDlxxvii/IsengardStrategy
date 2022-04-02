using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public void SetAvailableToInstanr(bool available)
    {
        if (available)
        {
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = Color.red;
        }
    }

    public void SetNormalColor()
    {
        _renderer.material.color = Color.white;
    }
}