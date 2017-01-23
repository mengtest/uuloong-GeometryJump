using DG.Tweening;
using UnityEngine;

namespace BaiyiGame.JumpMrQ
{
    public class MoveLevel : MonoBehaviour
    {
        [SerializeField]
        private float stepLength;
        public float StepLength
        {
            get { return stepLength; }
            set { stepLength = value; }
        }

        [SerializeField]
        private float switchingPosX;

        [SerializeField]
        private bool isRepeat = true;

        private bool canMove = true;
        public bool CanMove
        {
            set { canMove = value; }
            get { return canMove; }
        }

        private Vector3 ZeroX = new Vector3(0, 1, 1);

        void Start()
        {
        }

        void OnEnable()
        {
            PlayerManager.OnPlayerJump += DoMove;
        }

        void OnDisable()
        {
            PlayerManager.OnPlayerJump -= DoMove;
        }

        void DoMove(bool isSmall, float animTime, float distX, float distY, float jumpHeight)
        {
            if (!canMove)
            {
                return;
            }

            float moveLen = isSmall ? StepLength : 2 * StepLength;
            transform.DOLocalMoveX(transform.localPosition.x + moveLen, animTime).OnComplete(delegate ()
            {
                if (transform.localPosition.x < -switchingPosX)
                {
                    if (isRepeat)
                    {
                        float posX = switchingPosX - Mathf.Abs(switchingPosX + transform.localPosition.x);
                        // 设置修正后的posX
                        transform.localPosition = transform.localPosition
                        - (Vector3.right * transform.localPosition.x)
                        + (Vector3.right * posX);
                    }
                }
            });
        }
    }
}