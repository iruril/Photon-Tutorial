using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region Private Fields
        [SerializeField]
        private float directionDampTime = 0.25f;
        #endregion

        #region MonoBehaviour Callbacks

        private Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayAnimatorManager가 Animator 컴포넌트가 없습니다.", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if(stateInfo.IsName("Base Layer.Run"))
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            if (!animator)
            {
                return;
            }
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if(v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

        #endregion
    }
}
