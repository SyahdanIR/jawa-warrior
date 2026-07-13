using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ButoIjo : Enemy
{
    public static ButoIjo Instance;
    private float stage2Threshold;
    private float stage3Threshold;
    [SerializeField] private float maxHealth;
    [SerializeField] public Transform SideAttackTransform;
    [SerializeField] public Vector2 SideAttackArea;
    public float attackRange;
    public float attackTimer;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI hpText;
    private string currentAction = "";

    [Header("Ground Check Settings:")]
    [SerializeField] private Transform groundCheckPoint; //layer dari ground
    [SerializeField] private float groundCheckY = 0.2f; //seberapa jauh ground dari tanda scr vertikal?
    [SerializeField] private float groundCheckX = 0.5f; //seberapa jauh ground dari tanda scr horizontal
    [SerializeField] private LayerMask whatIsGround; //mengeset ground layer
    [HideInInspector] public bool facingRight;
    int hitCounter;
    bool stunned, canStun;
    bool alive;
    [HideInInspector] public float runSpeed;

    protected override void Awake()
    {
        Instance = this;
        stage2Threshold = maxHealth * 0.7f;
        stage3Threshold = maxHealth * 0.4f;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ChangeState(EnemyStates.Buto_Stage1);
        alive = true;
        UpdateStatusText("Kejar");
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

    // Update is called once per frame
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
        if (alive)
        {
            UpdateEnemyStageByHealth();
            UpdateHPText();
        }
    }
    private void UpdateEnemyStageByHealth()
    {
        if (!alive) return;
        if (health <= stage3Threshold && GetCurrentEnemyState != EnemyStates.Buto_Stage3)
        {
            Debug.Log("Pindah ke Stage 3");
            ChangeState(EnemyStates.Buto_Stage3);
        }
        else if (health > stage3Threshold && health <= stage2Threshold && GetCurrentEnemyState != EnemyStates.Buto_Stage2)
        {
            Debug.Log("Pindah ke Stage 2");
            ChangeState(EnemyStates.Buto_Stage2);
        }
        else if (health > stage2Threshold && GetCurrentEnemyState != EnemyStates.Buto_Stage1)
        {
            Debug.Log("Pindah ke Stage 1");
            ChangeState(EnemyStates.Buto_Stage1);
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
        if (Playercontroller.Instance != null)
        {
            switch (GetCurrentEnemyState)
            {
                case EnemyStates.Buto_Stage1:
                    attackTimer = 0.5f;
                    break;
                case EnemyStates.Buto_Stage2:
                    attackTimer = 1;
                    break;
                case EnemyStates.Buto_Stage3:
                    attackTimer = 1;
                    break;
            }
        }
    }
    protected override void OnCollisionStay2D(Collision2D _other)
    {

    }
    #region attacking
    #region variables
    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown;
    [HideInInspector] public bool damagedPlayer = false;
    [HideInInspector] public bool barrageAttack;
    public GameObject Batu;
    private bool hasUsedBarrage = false;

    #endregion

    #region Control
    private void UpdateStatusText(string action)
    {
        currentAction = action;
        if (statusText != null)
        {
            statusText.text = $"Status: {currentAction}";
        }
    }
    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {Mathf.CeilToInt(health)} / {Mathf.CeilToInt(maxHealth)}";
        }
    }
    public void AttackHandler()
    {
        float presentaseHP = (health / maxHealth) * 100f;
        float distance = Vector2.Distance(Playercontroller.Instance.transform.position, rb.position);

        float fuzzyOutput = FuzzyDecision(presentaseHP, distance);

        if (fuzzyOutput < 1.5f)
            StartCoroutine(Slash()); // Pukul
        else if (fuzzyOutput < 2.5f && !hasUsedBarrage)
            StartCoroutine(Skill()); // Skill
        else if (fuzzyOutput < 3.5f)
            BarrageBendDown(); // Lempar
        else
            UpdateStatusText("Kejar");
        // if (currentEnemyState == EnemyStates.Buto_Stage1)
        // {
        //     if (Vector2.Distance(Playercontroller.Instance.transform.position, rb.position) <= attackRange)
        //     {
        //         StartCoroutine(Slash());
        //     }
        //     else
        //     {
        //         StartCoroutine(Skill());
        //         BarrageBendDown();
        //     }
        // }
    }
    float FuzzyDecision(float hp, float distance)
    {
        // Derajat keanggotaan HP
        float μ_HP_Rendah = Triangular(hp, 0, 0, 30);
        float μ_HP_Sedang = Triangular(hp, 30, 50, 70);
        float μ_HP_Tinggi = Triangular(hp, 70, 100, 100);

        // Derajat keanggotaan Jarak
        float μ_Jarak_Dekat = Triangular(distance, 0, 1, 3);
        float μ_Jarak_Menengah = Triangular(distance, 3, 4.5f, 6);
        float μ_Jarak_Jauh = Triangular(distance, 6, 8.5f, 10);

        // Evaluasi setiap rule (min dari HP dan Jarak)
        float μ_Pukul = Mathf.Max(
            Mathf.Min(μ_HP_Rendah, μ_Jarak_Dekat),
            Mathf.Min(μ_HP_Sedang, μ_Jarak_Dekat),
            Mathf.Min(μ_HP_Tinggi, μ_Jarak_Dekat)
        );

        float μ_Lempar = Mathf.Min(μ_HP_Rendah, μ_Jarak_Jauh);

        float μ_Skill = Mathf.Max(
            Mathf.Min(μ_HP_Rendah, μ_Jarak_Menengah),
            Mathf.Min(μ_HP_Sedang, μ_Jarak_Menengah)
        );

        float μ_Kejar = Mathf.Max(
            Mathf.Min(μ_HP_Sedang, μ_Jarak_Jauh),
            Mathf.Min(μ_HP_Tinggi, μ_Jarak_Menengah),
            Mathf.Min(μ_HP_Tinggi, μ_Jarak_Jauh)
        );

        // Centroid defuzzifikasi (weighted average)
        float numerator = μ_Pukul * 1 + μ_Skill * 2 + μ_Lempar * 3 + μ_Kejar * 4;
        float denominator = μ_Pukul + μ_Skill + μ_Lempar + μ_Kejar;

        if (denominator == 0) return 4f; // default: kejar
        return numerator / denominator;
    }

    float Triangular(float x, float a, float b, float c)
    {
        if (x <= a || x >= c) return 0f; 
        else if (x == b) return 1f; 
        else if (x < b) return (x - a) / (b - a); 
        else return (c - x) / (c - b); 
    }


    public void ResetAllAttacks()
    {
        UpdateStatusText("Kejar");
        attacking = false;
        StopCoroutine(Slash());
        StopCoroutine(Skill());

        barrageAttack = false;
    }
    #endregion
    #region stage1
    IEnumerator Slash()
    {
        UpdateStatusText("SerangDekat");
        attacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Slash");
        yield return new WaitForSeconds(0.35f);
        anim.ResetTrigger("Slash");

        ResetAllAttacks();
    }
    #endregion
    #region stage2
    void BarrageBendDown()
    {
        if (hasUsedBarrage) return;
        attacking = true;
        rb.velocity = Vector2.zero;
        barrageAttack = true;
        hasUsedBarrage = true;
        anim.SetTrigger("BendDown");
    }
    public IEnumerator Barrage()
    {
        UpdateStatusText("Lempar");
        Flip();

        rb.velocity = Vector2.zero;

        float _currentAngle = 30f;
        for (int i = 0; i < 10; i++)
        {
            GameObject _projectile = Instantiate(Batu, transform.position, Quaternion.Euler(0, 0, _currentAngle));
            if (facingRight)
            {
                _projectile.transform.eulerAngles = new Vector3(_projectile.transform.eulerAngles.x, 0, _currentAngle);
            }
            else
            {
                _projectile.transform.eulerAngles = new Vector3(_projectile.transform.eulerAngles.x, 180, _currentAngle);
            }
            _currentAngle += 5f;

            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Throw", false);
        ResetAllAttacks();

    }
    #endregion
    #region stage 3
    IEnumerator Skill()
    {
        UpdateStatusText("Skill");
        Flip();
        attacking = true;

        anim.SetBool("Skill", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Skill", false);
        damagedPlayer = false;

        ResetAllAttacks();
    }
    #endregion
    #endregion

    protected override void Death(float _destroyTime)
    {
        StopAllCoroutines();
        ResetAllAttacks();
        alive = false;
        rb.velocity = new Vector2(rb.velocity.x, -25);
        anim.SetTrigger("Death");
        StartCoroutine(UIManager.Instance.ActivateVictoryScreen());
    }
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
}
