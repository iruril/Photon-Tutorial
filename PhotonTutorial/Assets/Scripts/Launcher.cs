using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [Tooltip("각 방에 할당된 최대 플레이어 수 입니다. 방이 다 차면, 신규 유저를 접속할 수 없으며, 새로운 방이 생성됩니다.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
        #endregion


        #region Private Fields
        /// <summary>
        ///이 클라이언트의 게임 버젼을 나타낸다. 각 유저들은 gameVersion에 따라 분할된다.
        /// </summary>
        string gameVersion = "1";
        bool isConnecting; //현재 프로세스를 추적한다.
        #endregion

        #region Public Fields
        [Tooltip("닉네임을 입력하고 연결 및 플레이 할 수 있게 하는 UI패널입니다.")]
        [SerializeField]
        private GameObject controlPanel;
        [Tooltip("진행상황을 알려주는 UI 프로그래스 라벨입니다.")]
        [SerializeField]
        private GameObject progressLabel;
        #endregion

        #region MonoBehaviourPunCallbacks CallBacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("Photon Tutorial : PUN에 의해 OnConnectedToMaster()가 호출되었습니다.");
            // 중요!! 우린 일단 존재하는 방에 접속을 시도한다.
            // 성공하면, 좋다.
            // 실패하면, OnJoinRandomFailed()로 다시 호출된다.
            if (isConnecting) //방에 들어가길 원치 않을때는 아무것도 안해야 한다.
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            //base.OnDisconnected(cause);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("Photon Tutorial : {0}의 이유로 PUN에 의해 OnDisconnected()가 호출되었습니다.");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Photon Tutorial : OnJoinRandomFailed()가 PUN에 의해 호출되었습니다." +
                " 유효한 방이 없으며, 방을 하나 생성합니다. \n호출: PhotonNetwork.CreateRoom");

            // 중요!! 랜덤한 방에 입장하는것을 실패했다.(다 찼거나, 없다.) 하지만 걱정말자. 하나 만들면 된다.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Photon Tutorial : OnJoinedRoom()이 PUN에 의해 호출되었습니다. 이제 이 클라이언트는 방에 접속해있습니다.");
            ///첫 번째 플레이어인 경우에만 로드하고 그렇지 않은 경우에는
            ///Photon Network.AutomaticallySyncScene을 사용하여 인스턴스 장면을 동기화한다.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Room for 1을 로드합니다.");

                //방 레벨을 로드한다.
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }

        /// <summary>
        /// MonoBehaviour 메소드가 초기 초기화시 유니티에 의해 불러진다.
        /// </summary>
        private void Awake()
        {
            // #중요!!
            // 이렇게 하면 마스터 클라이언트(방장)에서 PhotonNetwork.LoadLevel()을 사용할 수 있다.
            // 또한 같은 방에 있는 모든 클라이언트가 자동으로 레벨을 동기화한다.
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// 초기화 단계에서 유니티에 의해 MonoBehaviour 메소드가 게임오브젝트에서 불러진다.
        /// </summary>
        void Start()
        {
            //Connect(); //하단에서 구현
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// 연결 프로세스를 진행한다.
        /// 이미 연결되었다면, 랜덤한 방에 접속하기를 시도한다.
        /// 아니라면, 이 어플리케이션을 포톤 클라우드 네트워크에 연결시킨다.
        /// </summary>
        public void Connect()
        {
            isConnecting = true; //연결하고 있는지 여부를 계속해서 체크한다.
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                //#중요!! 여기서 랜덤한 방에 접속하기를 시도한다.
                //이 때 실패하면, OnJoinRandomFailed()에서 알림이 생성된다.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                //#중요!! 우선 포톤 온라인 서버에 연결해야 한다.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        #endregion
    }
}
