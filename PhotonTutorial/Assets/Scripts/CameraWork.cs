using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("XZ평면에서의 타겟과의 거리")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("타겟을 기준으로 한 카메라의 높이")]
        [SerializeField]
        private float height = 3.0f;

        [Tooltip("카메라가 수직으로 오프셋된다. 지면을 더 적게 볼 수 있음.")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("시작한 직후 무조건 이 플레이어를 주시하게 한다")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("카메라 움직임 해상도")]
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