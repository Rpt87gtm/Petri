using Assets.Scripts.Player.PlayerModel.Buffs;
using UnityEngine;

public class FoodEater : MonoBehaviour
{
    [SerializeField] EatenFoodBuff _foodCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food = collision.GetComponent<Food>();
        
        if (food != null)
        {
            _foodCount.AddFood(food.Eat());
        }
    }
}
