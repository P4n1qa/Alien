using System;
using UnityEngine;

namespace Root.Enemy.Scripts.BodySystem
{
    public interface IDamageable
    {
        Transform MouthPlayer{ get; set; }
        bool IsDied { get; }
        
        event Action OnDied;
        void ApplyDamage(float amount);
    }
}