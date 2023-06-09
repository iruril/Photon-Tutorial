using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields

        [Tooltip("The Beams GameObject to Control")]
        [SerializeField]
        private GameObject beams;
        bool IsFiring;
        #endregion

        #region Public Fields
        [Tooltip("The current Health of our player")]
        public float Health = 1f;
        #endregion

        #region Monobehaviour Callbacks
        void Awake()
        {
            if(beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
        }

        void Update()
        {
            ProcessInputs();
            if (photonView.IsMine)
            {
                ProcessInputs();
                if(Health <= 0f)
                {
                    GameManager.Instance.LeaveRoom();
                }
            }
            if(beams != null&&IsFiring != beams.activeInHierarchy)
            {
                beams.SetActive(IsFiring);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beams"))
            {
                return;
            }
            Health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beams"))
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }
        #endregion

        #region Custom
        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
        }
        #endregion
    }
}