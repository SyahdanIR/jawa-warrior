using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float recoilLength;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;
    [SerializeField] protected Playercontroller player;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] protected GameObject orangeBlood;
    protected float recoilTimer;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    public Animator anim;
    protected bool isDead = false;

    protected enum EnemyStates
    {
        //Crawler
        Crawler_Idle,
        Crawler_Flip,

        //Bat
        Bat_Idle,
        Bat_Chase,
        Bat_Stunned,
        Bat_Death,

        //Charger
        Charger_Idle,
        Charger_Suprised,
        Charger_Charge,

        //Goblin
        Goblin_Stage1,
        //Buto
        Buto_Chase,
        Buto_Surprised,
        Buto_Stunned,
        Buto_Death,
        Buto_Attack,
        Buto_Stage1,
        Buto_Stage2,
        Buto_Stage3
    }
    protected EnemyStates currentEnemyState;
    protected virtual EnemyStates GetCurrentEnemyState
    {
        get{return currentEnemyState;}
        set
        {
            if(currentEnemyState != value)
            {
                currentEnemyState = value;

                ChangeCurrentAnimation();
            }
        }
    }
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = Playercontroller.Instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(isRecoiling)
        {
            if(recoilTimer < recoilLength)
            {
                recoilTimer += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilTimer = 0;
            }
        }
        else{
            UpdateEnemyStates();
        }
    }
    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce){
        health -= _damageDone;
        if(!isRecoiling)
        {
            GameObject _orangeBlood = Instantiate(orangeBlood, transform.position, Quaternion.identity);
            Destroy(_orangeBlood, 5.5f);
            rb.velocity = _hitForce * recoilFactor * _hitDirection;
            isRecoiling = true;
        }
    }
    protected void OnTriggerStay2D(Collider2D _other)
    {
        if(_other.CompareTag("Player") && !Playercontroller.Instance.pState.invincible)
        {
            Attack();
            Playercontroller.Instance.HitStopTime(0, 5, 0.5f);
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D _other)
    {
        if(_other.gameObject.CompareTag("Player") && !Playercontroller.Instance.pState.invincible && !Playercontroller.Instance.pState.invincible && health > 0)
        {
            Attack();
            if(Playercontroller.Instance.pState.alive)
            {
                Playercontroller.Instance.HitStopTime(0, 5, 0.5f);
            }
        }
    }
    protected virtual void Death(float _destroyTime)
    {
        Destroy(gameObject, _destroyTime);
    }
    protected virtual void UpdateEnemyStates()
    {

    }
    protected virtual void ChangeCurrentAnimation()
    {

    }
    protected void ChangeState(EnemyStates _newState)
    {
        GetCurrentEnemyState = _newState;
    }
    protected virtual void Attack()
    {
        Playercontroller.Instance.TakeDamage(damage);
    }
}
