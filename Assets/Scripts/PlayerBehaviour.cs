using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCharacter
{
    Guerreiro = 0,
    Mago = 1,
    Arqueiro = 2
}
public enum PlayerStates
{
    Movement
}

public class PlayerBehaviour : CharacterBase
{
    private TypeCharacter type;
    private AnimationController animationController;

    // StateMachine
    private PlayerStates currentState = PlayerStates.Movement;

    // Movimentação
    private float speed = 3.0f;
    public float speedRun;
    public float runStaminaCost;
    public float speedWalk;
    public float speedRotation;
    public float rotateSpeed = 3.0f;
    private CharacterController controller;
    public Transform focusCamera;
    private float horizontal;
    private float vertical;
    private float currentStamina;
    private float maxStamina;
    public float staminaRecover;


    //Attack
    private int currentAttack = 0;
    public float attackRate;
    public int totalAttackAnimations;
    private float currentAttackRate;
    private float rangeAttack;

    // UI
    public UIController UI;
    protected new void Start()
    {

        PlayerStatsController.SetTypeCharacter(TypeCharacter.Guerreiro);
        currentLevel = PlayerStatsController.GetCurrentLevel();
        type = PlayerStatsController.GetTypeCharacter();

        basicStats = PlayerStatsController.intance.GetBasicStats(type);

        animationController = GetComponent<AnimationController>();
        speed = speedWalk;
        controller = GetComponent<CharacterController>();

        currentAttack = basicStats.baseAttack;

        base.Start();

        maxStamina = basicStats.baseStamina * basicStats.agility;
        currentStamina = maxStamina;
    }

    private void LookAt()
    {
        Vector3 positionCamera = new Vector3(focusCamera.position.x, transform.position.y, focusCamera.position.z);
        Quaternion newRotation = Quaternion.LookRotation(positionCamera - transform.position);

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speedRotation);
        }
    }

    protected new void Update()
    {
        base.Update();

        // Teste de Dano
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentLife -= Random.Range(1, 30);
        }
        //Setar Vida e Stamina
        UI.SetLife(basicStats.startLife, currentLife);
        UI.SetStamina(maxStamina, currentStamina);

        //Estado de Movimento
        switch (currentState)
        {
            case PlayerStates.Movement:
                {
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");

                    if (Input.GetKey(KeyCode.LeftShift) && vertical != 0 && currentStamina > 5)
                    {
                        speed = speedRun;
                        currentStamina -= runStaminaCost; //gasto de Stamina ao correr
                        animationController.PlayAnimation(AnimationStates.RUN);
                    }
                    else
                    {
                        speed = speedWalk;
                        if (vertical != 0)
                            animationController.PlayAnimation(AnimationStates.WALK);
                        else if (horizontal != 0)
                        {
                            animationController.PlayAnimation(AnimationStates.SIDE_WALK);
                        }

                        else
                            animationController.PlayAnimation(AnimationStates.IDDLE);
                    }

                    currentStamina += staminaRecover * Time.deltaTime;
                    currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

                    // Movimentação Câmera
                    Vector3 cameraForward = Vector3.Scale(focusCamera.forward, new Vector3(1, 0, 1)).normalized;
                    Vector3 moveDirection = vertical * cameraForward + horizontal * focusCamera.right;

                    moveDirection.y = Physics.gravity.y; //Parar com o Wingardium Leviosa :D
                    controller.Move(moveDirection * speed * Time.deltaTime);
                    LookAt();

                    // Attack
                    if (Input.GetButton("Fire1"))
                    {
                        Attack();
                    }
                    currentAttackRate += Time.deltaTime;

                    controller.Move(moveDirection * speed * Time.deltaTime);
                }
                break;
        }

    }

    private void Attack()
    {
        if (currentAttackRate >= attackRate)
        {
            currentAttackRate = 0;
            animationController.CallAttackAnimation(currentAttack);
            currentAttack++; //esquema de combo

            if (currentAttack > totalAttackAnimations)
            {
                currentAttack = 0;
            }
            //Calcula a distância de ataque
            Ray rayAttack = new Ray(transform.position, transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            rangeAttack = basicStats.baseRange;

            // Colisão
            if (Physics.Raycast(rayAttack, out hitInfo, rangeAttack))
            {
                if (hitInfo.collider.GetComponent<DestructiveBase>() != null)
                {
                    if (hitInfo.collider != GetComponent<Collider>())
                    {
                        hitInfo.collider.GetComponent<DestructiveBase>().ApplyDamage(currentAttack);
                    }
                }
            }
        }

    }
}


