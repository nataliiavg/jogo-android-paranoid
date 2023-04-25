using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectDestructiveBehaviour : DestructiveBase{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.G)){
            ApplyDamage(3);
        }
    }

    public override void OnDestroyed(){

        Destroy(gameObject);
    }

    public override void OnApplyDamage()
    {

    }
}
