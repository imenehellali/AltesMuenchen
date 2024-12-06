using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WareWolfEnemy : EnemyComponent
{
   // EnemyComponent.Enemy _wareWolfEnemy;
    public WareWolfEnemy(int _lifeCount, int _attackDamage, int _resistanceLevel, int _enemyType)
    {
        new EnemyComponent.Enemy(_lifeCount, _attackDamage, _resistanceLevel, _enemyType);
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    public override void Resist()
    {
        //Decrease Received attack damage
    }

    public override void TakeDamage()
    {
        //call Resist and onkly apply remaining damage to lifeSpan
        //if Lifespan ==0 --> destroy
        //apply animation of receiving attack 
    }

    public override void Update()
    {
       
    }

}
