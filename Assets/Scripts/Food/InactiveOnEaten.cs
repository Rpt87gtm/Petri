using UnityEngine;

[RequireComponent (typeof(Food))]
public class InactiveOnEaten : MonoBehaviour
{
    private Food _food;
    void Start()
    {
        _food = GetComponent<Food>();
        _food.Eaten += () => gameObject.SetActive(false);
    }
}
