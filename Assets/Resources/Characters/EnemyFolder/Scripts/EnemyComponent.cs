using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour
{
    public struct Enemy
    {

        public int _lifeCount;
        public int _attackDamage;
        public int _resistanceLevel;
        public int _enemyType;
        public Enemy(int _lifeCount, int _attackDamage, int _resistanceLevel, int _enemyType)
        {
            this._lifeCount = _lifeCount;
            this._attackDamage = _attackDamage;
            this._enemyType = _enemyType;
            this._resistanceLevel = _resistanceLevel;
        }
    }

    

    public abstract void Update();
    public abstract void OnDestroy();
    public abstract void Attack();
    public abstract void Resist();
    public abstract void TakeDamage();
}
