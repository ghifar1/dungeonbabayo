using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    // use for initialize
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    public void Damage()
    {
        if (isDead == true)
            return;

        Debug.Log("MossGiant::Damage()");
        //subtract 1 from health
        Health--;
        /*Health = Health - 1;
        Health -= 1;*/
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        //if health is less than 1
        if (Health < 1)
        {
            isDead = true;
            //Destroy(this.gameObject);
            anim.SetTrigger("Death");
            //spawn a diamond
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            //change value of diamond to whatever my gem count is.
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
        //destroy the object
    }

    /*private bool _switch;*/
    /*private Vector3 _currentTarget;
    private Animator _anim;
    private SpriteRenderer _mossSprite;*/
    /*private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _mossSprite = GetComponentInChildren<SpriteRenderer>();
    }*/

    /*public override void Update()
    {
        Movement();
        //if current pos == point A
        if (transform.position == pointA.position)
        {
            // move to Point B
            Debug.Log("PointA");
            _switch = false;
        }
        // else if current pos == point B
        else if (transform.position == pointB.position)
        {
            // move to point A
            Debug.Log("PointB");
            _switch = true;
        }

        if (_switch == false)
        {
            // move right
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            // move left
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }*/

    /*void Movement()
    {
        if (_switch == false)
        {
            // move right
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            // move left
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        }
    }*/
}
