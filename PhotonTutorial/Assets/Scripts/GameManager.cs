using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Photon Callbacks
        /// �����÷��̾ ���� ������ ȣ��ȴ�. Launcher ���� �ε��ؾ� �Ѵ�.
        public override void OnLeftRoom()
        {
            //base.OnLeftRoom();
            SceneManager.LoadScene(0);
        }
        #endregion

        #region Public Methods
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion
    }
}
