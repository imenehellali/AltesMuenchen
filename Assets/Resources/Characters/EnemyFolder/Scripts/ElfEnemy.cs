using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfEnemy : EnemyComponent
{
    //EnemyComponent.Enemy _elfEnemy;
    public ElfEnemy(int _lifeCount, int _attackDamage, int _resistanceLevel, int _enemyType) 
    {
       new EnemyComponent.Enemy(_lifeCount,_attackDamage,_resistanceLevel, _enemyType);
    }

    public override void Attack()
    {
        
    }

    public override void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    public override void Resist()
    {
       //will decrease the received damage attack 
    }

    public override void TakeDamage()
    {
        //Take Damage will call Resist and only apply 
        //remaining amount to _lifeCount
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

}
