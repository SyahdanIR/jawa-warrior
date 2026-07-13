using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;

public class goblin : Enemy
{
    public static goblin Instance;
    [SerializeField] public Transform SideAttackTransform;
    [SerializeField] public Vector2 SideAttackArea;
    public float attackRange;
    public float attackTimer;
    [HideInInspector] public bool facingRight;

    [Header("Ground Check Settings:")]
    [SerializeField] private Transform groundCheckPoint; //point at which ground check happens
    [SerializeField] private float groundCheckY = 0.2f; //how far down from ground chekc point is Grounded() checked
    [SerializeField] private float groundCheckX = 0.5f; //how far horizontally from ground chekc point to the edge of the player is
    [SerializeField] private LayerMask whatIsGround; //sets the ground layer
    int hitCounter;
    bool alive;
    public float chaseRange = 10f;
    [HideInInspector] public float runSpeed;
    protected override void Awake()
    {
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ChangeState(EnemyStates.Goblin_Stage1);
        alive = true;
    }
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
    }
    protected override void Update()
    {
        base.Update();
        if (health <= 0 && alive)
        {
           alive = false;
           Death(0);
        } 
        if (!attacking)
           {
               attackCountdown -= Time.deltaTime;
           }
    }
    public void Flip()
    {
        if (Playercontroller.Instance.transform.position.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 180);
            facingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector2(transform.eulerAngles.x, 0);
            facingRight = true;
        }
    }
    protected override void UpdateEnemyStates()
    {
        switch (GetCurrentEnemyState)
        {
            case EnemyStates.Goblin_Stage1:
                break;
        }
    }
    protected override void OnCollisionStay2D(Collision2D _other)
    {

    }
    #region attacking
    #region variables
    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown = 1;

    #endregion

    #region Control
    public void AttackHandler()
    {
        if (currentEnemyState == EnemyStates.Goblin_Stage1)
        {
            if (Vector2.Distance(Playercontroller.Instance.transform.position, rb.position) <= 3)
            {
                StartCoroutine(Slash());
            }
            else
            {
                return;
            }
        }
    }
    public void ResetAllAttacks()
    {
        attacking = false;
        StopCoroutine(Slash());
    }
    #endregion

    #region Stage1
    IEnumerator Slash()
    {
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Slash");
        yield return new WaitForSeconds(0.15f);
        yield return new WaitForSeconds(0.5f);

        ResetAllAttacks();
    }
    #endregion
    #endregion
    protected override void Death(float _destroyTime)
    {
        base.Death(_destroyTime);
        FindObjectOfType<ScoreManager>().AddScore(150);
    }
}
