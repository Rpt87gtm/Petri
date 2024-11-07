using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerModel.Buffs
{
    public class EatenFoodBuff : NetworkBehaviour, IBuff
    {
        [SerializeField] private bool _canEat = true;
        [SyncVar]
        private int _food = 0;

        public void StopEat()
        {
            _canEat = false;
        }

        public void StartEat()
        {
            _canEat = true;
        }

        public int GetEatenFood() => _food;

        public int AddFood()
        {
            if (!_canEat) return _food; 
            _food++;
            return _food; 
        }

        public int AddFood(int food)
        {
            if (!_canEat || food < 0) return _food;
            _food += food;
            return _food;
        }

        public int RemoveFood()
        {
            _food = Mathf.Max(_food - 1, 0); 
            return _food;
        }

        public int RemoveFood(int foodToRemove)
        {
            if (foodToRemove < 0) return _food; 
            _food = Mathf.Max(_food - foodToRemove, 0);
            return _food;
        }

        public PlayerCellStats ApplyBuff(PlayerCellStats cellStats)
        {
            return cellStats.WithMass(cellStats.Mass + _food); 
        }
    }
}
