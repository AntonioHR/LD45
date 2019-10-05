using System;
using Common.StateMachines;

namespace SamuraiGame.Player
{
    public class PlayerStateBase : State<PlayerController, PlayerStateBase>
    {
        protected PlayerController Player { get => Context; }

        public virtual void Update() { }

    }
}