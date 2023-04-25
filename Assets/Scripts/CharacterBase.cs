using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicStats{

    // Atributos Básicos -- não deu tempo de implementar tudo...so sad =/
    public float startLife;
    public float startMana;
    public int strenght;
    public int inteligence;
    public int agility;
    public int baseDefense;
    public int baseAttack;
    public float baseRange;
    public float baseStamina;
}

// Herda do Destructive Base
public abstract class  CharacterBase : DestructiveBase {
    
    // Atributos Básicos
    public int currentLevel;
    public BasicStats basicStats;


    protected void Start(){
        currentLife = basicStats.startLife;
        
    }
       
     protected void Update(){
        
    }

    public override void OnDestroyed(){
        //Debug.Log(gameObject.name + "Está Morto");
    }

    public override void OnApplyDamage()
    {
        
    }
}
