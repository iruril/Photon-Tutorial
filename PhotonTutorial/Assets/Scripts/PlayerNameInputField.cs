using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// �÷��̾� �̸� �Է� ����. ������ ���� �̸��� �Է��ϰ� �ϸ�, ���� �� �÷��̾� ��ܿ� ǥ�õǰ� �Ѵ�.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        #region Private Constants
        //�÷��̾��� �̸��� �����Ѵ�.
        const string playerNamePrefKey = "PlayerName";
        #endregion

        #region MonoBehaviour Callbacks
        /// <summary>
        /// MonoBehaviour �޼ҵ尡 �ʱ� �ʱ�ȭ�� ����Ƽ�� ���� �ҷ�����.
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
        /// �÷��̾��� �̸��� �����Ѵ�. �׸��� �̷� ������ ���� PlayerPrefs�� �����Ѵ�.
        /// </summary>
        /// <param name="value">�÷��̾��� �̸�r</param>
        public void SetPlayerName(string value)
        {
            // #�߿�!!
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value; //��Ʈ��ũ ���� �÷��̾� �̸��� �����Ѵ�.
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}
