using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance { get; private set; }
    private Animator animator;
    private Rigidbody2D rb;
    public bool isPunced = false;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        ChangeAnime();
    }

    private void ChangeAnime()

    {
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("speed", speed);
    }
    public void JumpStarted()
    {
        animator.SetBool("jump", true);
    }
    public void JumpStopped()
    {
        animator.SetBool("jump", false);
        animator.SetTrigger("grounded");
    }
    public void WallStarted()
    {
        animator.SetBool("onWall", true);
    }
    public void WallStopped() 
    {
        animator.SetBool("onWall", false);
    }
    public void PunchAnimationStarted()
    {
        animator.SetBool("punch", true);
        isPunced = true;
    }
    public void PunchAnimationStopped()
    {
        animator.SetBool("punch", false);
        isPunced = false;
    }

}
