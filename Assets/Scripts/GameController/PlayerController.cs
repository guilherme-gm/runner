using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Estados de pulo
    /// </summary>
    private enum JumpState
    {
        Grounded,
        Prepare,
        Jumping
    }

    #region Animation Var
    /// <summary>Player animator</summary>
    private Animator anim;
    /// <summary>Hash de Sleep para o animator</summary>
    private int animSleepHash = Animator.StringToHash("Root.Sleep");
    /// <summary>Tempo de Idle antes de começar (em segs)</summary>
    private float animIdleTimer = 1f;
    #endregion

    #region Player Object Data
    /// <summary>Força do Movimento</summary>
    public float speed = 1f;
    /// <summary>Força do pulo</summary>
    public float jumpForce = 1f;

    private Rigidbody2D rigidBody;
    private JumpState jumpState = JumpState.Grounded;
    #endregion

    private void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
        this.rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsSleeping())
        {
            if (Input.GetButtonDown("Action"))
            {
                this.anim.SetBool(Const.Player.animIsSleeping, false);
                this.anim.SetFloat(Const.Player.animSpeed, 0f);
            }
        }
        else if (animIdleTimer > 0)
        {
            this.animIdleTimer -= Time.deltaTime;
            if (this.animIdleTimer <= 0)
                this.animIdleTimer = 0;
        }
        else
        {
            if (Input.GetButtonDown("Action") && this.jumpState == JumpState.Grounded)
                this.jumpState = JumpState.Prepare;
        }
    }

    private void FixedUpdate()
    {
        if (this.animIdleTimer == 0)
        {
            this.anim.SetFloat(Const.Player.animSpeed, speed);
            this.rigidBody.AddForce(Vector2.right * speed);
            this.animIdleTimer = -1;
        }

        if (this.jumpState == JumpState.Prepare)
        {
            this.rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            this.jumpState = JumpState.Jumping;
        }
    }

    private bool IsSleeping()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.fullPathHash == animSleepHash;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == Const.Tags.Platform)
            this.jumpState = JumpState.Grounded;
    }
}
