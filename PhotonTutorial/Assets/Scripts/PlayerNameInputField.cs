using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// 플레이어 이름 입력 공간. 유저가 직접 이름을 입력하게 하며, 게임 중 플레이어 상단에 표시되게 한다.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants
        //플레이어의 이름을 저장한다.
        const string playerNamePrefKey = "PlayerName";
        #endregion

        #region MonoBehaviour Callbacks
        /// <summary>
        /// MonoBehaviour 메소드가 초기 초기화시 유니티에 의해 불러진다.
        /// </summary>
        void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if(_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 플레이어의 이름을 세팅한다. 그리고 미래 세션을 위해 PlayerPrefs에 저장한다.
        /// </summary>
        /// <param name="value">플레이어의 이름r</param>
        public void SetPlayerName(string value)
        {
            // #중요!!
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value; //네트워크 상의 플레이어 이름을 설정한다.
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}
