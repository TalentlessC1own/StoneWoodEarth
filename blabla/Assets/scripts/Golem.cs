using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem :Unit
{
    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private GameObject healthBarObject;

    private bool die = false;

    private bool attacking = false;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 10F;


    private bool isGrounded = false;

    public bool IsGrounded
    { get { return isGrounded; } }

    public GolemStates State
    {
        get { return (GolemStates)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }

    }

    private Animator animator;
    new private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;

    public void Run(float axis)
    {
        if (!attacking)
        {
            if (isGrounded) State = GolemStates.Run;
            Vector3 direction = transform.right * axis * PlayerFlip();
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
            if (direction.x < 0)
                transform.localScale = new Vector3(PlayerFlip() * -1.0f, 1.0f, 1.0f);
            if (direction.x > 0)
                transform.localScale = new Vector3(PlayerFlip(), 1.0f, 1.0f);
        }

    }

    private float PlayerFlip()
    {
        if (GetComponent<GolemController>().controller == Controller.player_2) 
            return -1.0f;
        else
            return 1.0f;
    }

    private void Start()
    {
        healthBar.SetMaxHealth(lives);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnBecameInvisible()
    {
        gameObject.GetComponent<Golem>().Die();
        Destroy(gameObject);
    }
    private IEnumerator AttackCd()
    {
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public void OnHealthBar()
    {
        healthBarObject.SetActive(true);
    }
    public void Jump()
    {
        if(attacking) return;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void GolemRotate()
    {
       gameObject.transform.Rotate(0, 180, 0);
    }

    public void Attack()
    {
        if (isGrounded && !attacking)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCd());
        }

    }

    private void FixedUpdate()
    { 
        CheckGround();
        animator.SetBool("isGrounded", isGrounded);
    }

    private void Update()
    {
        if (lives <= 0) Die();
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5F);

        isGrounded = colliders.Length > 1;

    }

    public void Die()
    {
        die = true;
        healthBar.Off();
        animator.SetBool("Die", true);
        rigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<GolemController>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    public bool IsDie()
    {
        return die;
    }
    
    override public void ReciveDamage()
    {
        
        animator.SetTrigger("TakeDamage");
        lives--;
        healthBar.SetHealth(lives);
        Debug.Log(lives);
    }

}

public enum GolemStates
{
    Idle,
    Run
}
