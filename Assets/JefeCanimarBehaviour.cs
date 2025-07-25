using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeCanimarBehaviour : StateMachineBehaviour
{
    private boss Boss;
    private Rigidbody2D rb2D;
    [SerializeField] private float VelocidadDelBoss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Boss = animator.GetComponent<boss>();
       rb2D = Boss.rb2d;
      Boss.MirarJugador();
        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
      rb2D.velocity =  new Vector2(VelocidadDelBoss  ,rb2D.velocity.y ) * animator.transform.right;
    
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb2D.velocity =  new Vector2(0  ,rb2D.velocity.y );
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
