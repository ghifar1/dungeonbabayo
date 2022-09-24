using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    // use this for initialization
    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("Spider::Damage()");
        Health--;
        if(Health < 1)
        {
            isDead = true;
            //Destroy(this.gameObject);
            anim.SetTrigger("Death");
            //spawn a diamond
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            //change value of diamond to whatever my gem count is.
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        //sit still
    }

    public void Attack()
    {
        //instantiate the acid effect
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }

    /*private Vector3 _currentTarget;
    private Animator _anim;
    private SpriteRenderer _spiderSprite;*/
    /*private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _spiderSprite = GetComponentInChildren<SpriteRenderer>();
    }*/

    /*public override void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        Movement();
    }*/

    /*void Movement()
    {
        // flip sprite
        if (_currentTarget == pointA.position)
        {
            _spiderSprite.flipX = true;
        }
        else
        {
            _spiderSprite.flipX = false;
        }

        ///if current pos == point A
        if (transform.position == pointA.position)
        {
            // move to Point B
            _currentTarget = pointB.position;
            _anim.SetTrigger("Idle");
        }
        // else if current pos == point B
        else if (transform.position == pointB.position)
        {
            // move to point A
            _currentTarget = pointA.position;
            _anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);
        
    }*/
}
