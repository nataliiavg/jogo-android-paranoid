using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pai do SimpleEnemyBehaviour
public abstract class DestructiveBase : MonoBehaviour{

    
    public float currentLife;
    protected bool isDead;

    protected void Start(){
       
    }

    
    protected void Update(){
        
    }

    public void ApplyDamage(int damage){
        if (isDead) return;

        currentLife -= damage;

        if (currentLife <= 0){
            isDead = true;
            OnDestroyed();
        }

        OnApplyDamage();
    }

    public abstract void OnDestroyed();
    public abstract void OnApplyDamage();

}
