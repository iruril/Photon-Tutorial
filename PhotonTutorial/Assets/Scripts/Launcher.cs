using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [Tooltip("�� �濡 �Ҵ�� �ִ� �÷��̾� �� �Դϴ�. ���� �� ����, �ű� ������ ������ �� ������, ���ο� ���� �����˴ϴ�.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
        #endregion


        #region Private Fields
        /// <summary>
        ///�� Ŭ���̾�Ʈ�� ���� ������ ��Ÿ����. �� �������� gameVersion�� ���� ���ҵȴ�.
        /// </summary>
        string gameVersion = "1";
        bool isConnecting; //���� ���μ����� �����Ѵ�.
        #endregion

        #region Public Fields
        [Tooltip("�г����� �Է��ϰ� ���� �� �÷��� �� �� �ְ� �ϴ� UI�г��Դϴ�.")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("�����Ȳ�� �˷��ִ� UI ���α׷��� ���Դϴ�.")]
        [SerializeField]
        private GameObject progressLabel;
        #endregion

        #region MonoBehaviourPunCallbacks CallBacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Photon Tutorial : PUN�� ���� OnConnectedToMaster()�� ȣ��Ǿ����ϴ�.");
            // �߿�!! �츰 �ϴ� �����ϴ� �濡 ������ �õ��Ѵ�.
            // �����ϸ�, ����.
            // �����ϸ�, OnJoinRandomFailed()�� �ٽ� ȣ��ȴ�.
            if (isConnecting) //�濡 ���� ��ġ �������� �ƹ��͵� ���ؾ� �Ѵ�.
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            //base.OnDisconnected(cause);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("Photon Tutorial : {0}�� ������ PUN�� ���� OnDisconnected()�� ȣ��Ǿ����ϴ�.");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Photon Tutorial : OnJoinRandomFailed()�� PUN�� ���� ȣ��Ǿ����ϴ�." +
                " ��ȿ�� ���� ������, ���� �ϳ� �����մϴ�. \nȣ��: PhotonNetwork.CreateRoom");

            // �߿�!! ������ �濡 �����ϴ°��� �����ߴ�.(�� á�ų�, ����.) ������ ��������. �ϳ� ����� �ȴ�.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Photon Tutorial : OnJoinedRoom()�� PUN�� ���� ȣ��Ǿ����ϴ�. ���� �� Ŭ���̾�Ʈ�� �濡 �������ֽ��ϴ�.");
            ///ù ��° �÷��̾��� ��쿡�� �ε��ϰ� �׷��� ���� ��쿡��
            ///Photon Network.AutomaticallySyncScene�� ����Ͽ� �ν��Ͻ� ����� ����ȭ�Ѵ�.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Room for 1�� �ε��մϴ�.");

                //�� ������ �ε��Ѵ�.
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        /// <summary>
        /// MonoBehaviour �޼ҵ尡 �ʱ� �ʱ�ȭ�� ����Ƽ�� ���� �ҷ�����.
        /// </summary>
        private void Awake()
        {
            // #�߿�!!
            // �̷��� �ϸ� ������ Ŭ���̾�Ʈ(����)���� PhotonNetwork.LoadLevel()�� ����� �� �ִ�.
            // ���� ���� �濡 �ִ� ��� Ŭ���̾�Ʈ�� �ڵ����� ������ ����ȭ�Ѵ�.
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// �ʱ�ȭ �ܰ迡�� ����Ƽ�� ���� MonoBehaviour �޼ҵ尡 ���ӿ�����Ʈ���� �ҷ�����.
        /// </summary>
        void Start()
        {
            //Connect(); //�ϴܿ��� ����
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// ���� ���μ����� �����Ѵ�.
        /// �̹� ����Ǿ��ٸ�, ������ �濡 �����ϱ⸦ �õ��Ѵ�.
        /// �ƴ϶��, �� ���ø����̼��� ���� Ŭ���� ��Ʈ��ũ�� �����Ų��.
        /// </summary>
        public void Connect()
        {
            isConnecting = true; //�����ϰ� �ִ��� ���θ� ����ؼ� üũ�Ѵ�.
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                //#�߿�!! ���⼭ ������ �濡 �����ϱ⸦ �õ��Ѵ�.
                //�� �� �����ϸ�, OnJoinRandomFailed()���� �˸��� �����ȴ�.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                //#�߿�!! �켱 ���� �¶��� ������ �����ؾ� �Ѵ�.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        #endregion
    }
}
