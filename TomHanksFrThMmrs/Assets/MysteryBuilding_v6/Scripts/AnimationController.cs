using UnityEngine;
using System.Collections;
using Pathfinding.RVO;
using Pathfinding.Util;
using Pathfinding;

using AF; 
namespace AF{
	
	[RequireComponent (typeof (Animator))]
	public class AnimationController : MonoBehaviour {
		public Vector2 targetVelocity ;
		private Animator animator;

		public float run = 6f;
		//	public Vector2 targetVelocity;
		public bool canWalk = true;
		AIPath path;

		void Start () {
			animator = GetComponent<Animator>();
			path = GetComponentInParent<AIPath> ();
		}
		void Update(){


		}

		void FixedUpdate () {
			if (canWalk){
				 targetVelocity = path.rotDir;
//				Debug.Log (targetVelocity + path.rotDir);
		
				if (targetVelocity != Vector2.zero)
					//animator.Play ("idle");
					//	else
				{
                    if (targetVelocity.x > 0)
                    {
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        animator.Play("Right");
                    }
                    else
                    {
                        gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                        animator.Play("Left");
                    }
                }
                else
                {
                    animator.Play("Idle");
                }

			}
		}






	}
}