using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    //variable for amounts the diamond
    public int diamonds;

    // get reference to rigidbody
    private Rigidbody2D _rigid;

    // variable for jumpForce
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    /*[SerializeField]
    private LayerMask _groundedLayer;*/

    /*private bool resetJumpNeeded = false;*/
    // use this for initialization

    // handle
    private bool _grounded = false;
    // handle to PlayerAnimation
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        // assign handle to PlayerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();
        // assign handle of SpriteRenderer
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        // assign handle of SwordArcSprite
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        /*CheckGrounded();*/

        // if left click && isGrounded
        //if(Input.GetMouseButtonDown(0) && isGrounded() == true)
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded() == true)
        {
            _playerAnim.Attack();
        }
        // Attack
    }

    void Movement()
    {
        // horizontal input for left/right
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
        _grounded = isGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        //if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButton("B_Button")) && isGrounded() == true)
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && isGrounded() == true)
        {
            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            // tell Animator to Jump
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);

        /*// if space key && grounded == true
        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            // current velocity = new velocity (current X, jumpForce) Jump !
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            // grounded = false
            _grounded = false;
            // breath
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
        }
        // current velocity = new velocity (horizontal input,current velocity.y);
        _rigid.velocity = new Vector2(move, _rigid.velocity.y);*/
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 6);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if(hitInfo.collider != null)
        {
            if(_resetJump == false)
            {
                // set Animator bool to false
                // Debug.Log("Grounded");
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Flip(bool facingRight)
    {
        if(facingRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if(facingRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }
        Debug.Log("Player::Damage()");
        // remove 1 health
        Health--;
        // update UI Display
        UIManager.Instance.UpdateLives(Health);
        // check for dead
        if(Health < 1)
        {
            _playerAnim.Death();
        }
        // play death animation
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    /*void CheckGrounded()
    {
        // 2D raycast to the ground
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 6);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, _groundedLayer.value);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        // if hitInfo != null
        if (hitInfo.collider != null)
        {
            // grounded = true
            Debug.Log("Hit: " + hitInfo.collider.name);
            if (resetJumpNeeded == false)
                _grounded = true;
        }
    }*/

    /*IEnumerator ResetJumpNeededRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }*/
}
