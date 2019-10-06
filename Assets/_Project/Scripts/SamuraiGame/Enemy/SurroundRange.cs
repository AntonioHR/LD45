using System;
using UnityEngine;

namespace SamuraiGame.Enemy
{
    public class SurroundRange
    {
        [Serializable]
        public class Setup
        {
            public float min;
            public float max;
        }

        private Transform unit;
        private Setup setup;
        private float min{ get => setup.min; }
        private float max{ get => setup.max; }
        

        public SurroundRange(Transform unit, Setup setup)
        {
            this.unit = unit;
            this.setup = setup;
        }
        


        public float RangeCoordinates(Vector2 target)
        {
            float m = ((Vector2)unit.position - target).magnitude;
            return (m - min) / (max - min);
            
        }
    }
}