using System;

namespace SamuraiGame.Player
{
    public class Health
    {
        public event Action<int> ValueChanged;
        public event Action Died;

        public int max{ get; private set; }
        public int Current{ get; private set; }
        public bool CanHeal { get => Current < max; }

        public Health(int max)
        {
            this.max = max;
            Current = max;
        }



        public void Hit(int damage = 1)
        {
            Current--;

            OnValueChanged();

            if(Current == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if(Died!=null)
                Died();
        }

        public void Heal(int amount = 1)
        {
            if(CanHeal)
                Current++;
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            if(ValueChanged!= null)
                ValueChanged(Current);
        }

    }
}