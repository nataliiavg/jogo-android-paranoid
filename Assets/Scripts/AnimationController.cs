using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Animações Char
public enum AnimationStates{
    WALK,
    SIDE_WALK,
    RUN,
    IDDLE,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    NONE = 4
}
public class AnimationController : MonoBehaviour{
    public Animator animator;

    public void PlayAnimation(AnimationStates stateAnimation){

        switch (stateAnimation){
            case AnimationStates.IDDLE:{
                    StopAnimations();
                    animator.SetBool("inIddle", true);
            }
            break;
            case AnimationStates.WALK:{
                    StopAnimations();
                    animator.SetBool("inWalk", true);
            }
            break;

            case AnimationStates.SIDE_WALK:{
                    StopAnimations();
                    animator.SetBool("sideWalk", true);
            }
            break;

            case AnimationStates.RUN:{
                    StopAnimations();
                    animator.SetBool("inRun", true);
            }
            break;
            case AnimationStates.ATTACK1:{
                    StopAnimations();
                    animator.SetBool("inAttack", true);
            }
            break;
            case AnimationStates.ATTACK2:{
                    StopAnimations();
                    animator.SetBool("inAttack", true);
            }
            break;
            case AnimationStates.ATTACK3:{
                    StopAnimations();
                    animator.SetBool("inAttack", true);
            }
            break;
        }        
    }

    public void CallAttackAnimation(int indiceAnimation)
    {
        if (indiceAnimation == 0)
            PlayAnimation(AnimationStates.ATTACK1);
        else if (indiceAnimation == 1)
            PlayAnimation(AnimationStates.ATTACK2);
        else if (indiceAnimation == 2)
            PlayAnimation(AnimationStates.ATTACK3);

        animator.SetInteger("CurrentAttack", indiceAnimation);


    }

    void StopAnimations() {
        animator.SetBool("inWalk", false);
        animator.SetBool("sideWalk", false);
        animator.SetBool("inRun", false);
        animator.SetBool("inIddle", false);
        animator.SetBool("inAttack", false);
    }

}

