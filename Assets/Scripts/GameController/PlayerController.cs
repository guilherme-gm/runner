using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Animation Var
    /// <summary>Player animator</summary>
    private Animator anim;
    /// <summary>Hash de Sleep para o animator</summary>
    private int animSleepHash = Animator.StringToHash("Root.Sleep");
    /// <summary>Tempo de Idle antes de começar (em segs)</summary>
    private float animIdleTimer = 1f;
    #endregion

    #region Player Object Data
    /// <summary>Velocidade de Movimento</summary>
    public float speed = 1f;
    public float jumpForce = 1f;

    private Rigidbody2D rigidBody;
    #endregion

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsSleeping())
        {
            if (Input.GetButtonDown("Action"))
            {
                anim.SetBool(Const.Player.animIsSleeping, false);
                anim.SetFloat(Const.Player.animSpeed, 0f);
            }
        }
        else if (animIdleTimer > 0)
        {
            animIdleTimer -= Time.fixedDeltaTime;
        }
        else
        {
            animIdleTimer = 0;
            anim.SetFloat(Const.Player.animSpeed, speed);
            //rigidBody.MovePosition(gameObject.transform.position + new Vector3(1, 0, 0) * speed * Time.deltaTime);
            rigidBody.AddForce(new Vector2(1, 0) * speed);

            if (Input.GetButtonDown("Action") && CanJump())
            {
                rigidBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            }
        }
    }

    private bool IsSleeping()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.fullPathHash == animSleepHash;
    }

    private bool CanJump()
    {
        return true;
    }
}
