using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BaiyiGame.JumpMrQ
{
    public class RoleItem : MonoBehaviour
    {
        [SerializeField]
        private string roleName;
        public GameObject other;

        void Start ()
        {
            SetState();
        }

        public string RoleName
        {
            get
            {
                return roleName;
            }

            set
            {
                roleName = value;
                SetState();
            }
        }

        void SetState ()
        {
            bool state = PlayerData.IsRoleLock(RoleName);

            if (state) {
                other.SetActive(true);
            } else
            {
                other.SetActive(false);
            }
        }
    }
}
