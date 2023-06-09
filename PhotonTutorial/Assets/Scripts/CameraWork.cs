using UnityEngine;

namespace Com.MyCompany.MyGame
{
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("XZ��鿡���� Ÿ�ٰ��� �Ÿ�")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("Ÿ���� �������� �� ī�޶��� ����")]
        [SerializeField]
        private float height = 3.0f;

        [Tooltip("ī�޶� �������� �����µȴ�. ������ �� ���� �� �� ����.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("������ ���� ������ �� �÷��̾ �ֽ��ϰ� �Ѵ�")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("ī�޶� ������ �ػ�")]
        [SerializeField]
        private float smoothSpeed = 0.125f;

        Transform cameraTransform;

        bool isFollowing;

        Vector3 cameraOffset = Vector3.zero;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            if (followOnStart)
            {
                OnStartFollowing();
            }
        }


        void LateUpdate()
        {
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }

           
            if (isFollowing)
            {
                Follow();
            }
        }

        #endregion

        #region Public Methods

        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            
            Cut();
        }

        #endregion

        #region Private Methods

        void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);

            cameraTransform.LookAt(this.transform.position + centerOffset);
        }


        void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

            cameraTransform.LookAt(this.transform.position + centerOffset);
        }
        #endregion
    }
}