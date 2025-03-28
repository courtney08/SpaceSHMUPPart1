using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(EnemyShield))]
public class Enemy_4 : Enemy
{
    private EnemyShield[] allShields;
    private EnemyShield thisShield;

    
    void Start()
    {
        allShields = GetComponentsInChildren<EnemyShield>();
        thisShield = GetComponent<EnemyShield>();
    }

    public override void Move(){
        //
        //
    }


    void OnCollisionEnter(Collision coll){
        GameObject otherGO = coll.gameObject;

        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if(p != null){
            Destroy(otherGO);

            if(bndCheck.isOnScreen){
                GameObject hitGO = coll.contacts[0].thisCollider.gameObject;
            

            float dmg = Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;

            bool shieldFound = false;
            foreach(EnemyShield es in allShields){
                if(es.gameObject == hitGO){
                    es.TakeDamage(dmg);
                    shieldFound = true;
                }
            }
            if(!shieldFound) thisShield.TakeDamage(dmg);

            if(thisShield.isActive) return;

            if(!calledShipDestroyed){
                Main.SHIP_DESTROYED(this);
                calledShipDestroyed = true;
            }

            
                Destroy(gameObject);
            }
        }
        else{
            Debug.Log("Enemy_4 hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
