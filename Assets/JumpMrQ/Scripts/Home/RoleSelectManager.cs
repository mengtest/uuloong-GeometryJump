using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Collections.Generic;

namespace BaiyiGame.JumpMrQ
{
    public class RoleSelectManager : MonoBehaviour
    {
        // 角色被选中 移到屏幕中间 回调
        public delegate void RoleSelected(string role);
        public static event RoleSelected OnRoleSelected;

        // 屏幕中间的角色 点击回调
        public delegate void RoleSelectedClick();
        public static event RoleSelectedClick OnRoleSelectedClick;

        // 屏幕中间的角色 点击回调
        public delegate void UnLockClick(GameObject role);
        public static event UnLockClick OnUnLockClick;

        private static RoleSelectManager Instance;

        // 界面上显示的角色
        [SerializeField]
        private List<Image> Roles;

        // 所有的角色
        public Sprite[] AllSprite;

        // 未解锁的角色
        [SerializeField]
        private Sprite[] AllLockeds;

        // 动画时间
        [SerializeField]
        private float AnimatTime;

        private int preIndex;
        private int nextIndex;

        void Awake()
        {
            Instance = this;
        }

        void OnEnable()
        {
            InputTouch.OnSlideLeft += OnSlideLeft;
            InputTouch.OnSlideRight += OnSlideRight;
            MakeCenter(getAllSpriteIndex(PlayerData.CurRole));
        }

        void OnDisable()
        {
            InputTouch.OnSlideLeft -= OnSlideLeft;
            InputTouch.OnSlideRight -= OnSlideRight;
        }

        /// <summary>
        /// 解锁角色
        /// </summary>
        /// <param name="go"></param>
        public static void UnLockRole(GameObject go)
        {
            //未解锁的角色的名称
            string roleName = go.GetComponent<Image>().sprite.name;
            //string role = go.GetComponent<RoleItem>().RoleName;
            if (!string.IsNullOrEmpty(roleName))
            {
                PlayerData.CurRole = Instance.AllSprite[Instance.getAllLockedIndex(roleName)].name;
                PlayerData.UnLockRole(PlayerData.CurRole);
                var image = go.GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = Instance.AllSprite[Instance.getAllLockedIndex(roleName)];
                }
            }
        }

        public void OnItemClick(GameObject go)
        {
            string role = go.GetComponent<RoleItem>().RoleName;
            if (!PlayerData.IsRoleLock(role))
            {
                // 角色解锁后自动设置中间的角色为当前使用的角色
                PlayerData.CurRole = role;
                if (null != OnRoleSelectedClick)
                {
                    OnRoleSelectedClick();
                }
            }
            else
            {
                MakeCenter(getAllSpriteIndex(role));
            }

            //if (PlayerData.IsRoleLock(role))
            //{
            //    // 屏幕中间的角色被点击
            //    string centerRole = Roles[Roles.Count / 2].GetComponent<RoleItem>().RoleName;
            //    if (centerRole == role && OnUnLockClick != null)
            //    {
            //        //Debug.Log("UnLock:" + role);
            //        //OnUnLockClick(go);
            //        return;
            //    }
            //}
            //else
            //{
            //    // 角色解锁后自动设置中间的角色为当前使用的角色
            //    PlayerData.CurRole = role;

                //    Debug.Log("PlayerData.CurRole   --- >  " + PlayerData.CurRole);
                //    if (OnRoleSelected != null)
                //    {
                //        OnRoleSelected(role);
                //    }

                //    // 屏幕中间的角色被点击
                //   string centerRole = Roles[Roles.Count / 2].GetComponent<RoleItem>().RoleName;
                //   if (centerRole == role && OnRoleSelectedClick != null)
                //    {
                //        OnRoleSelectedClick();
                //        return;
                //    }
                //}
                //Debug.Log("UnLock:" + role);
                //
        }

        /// <summary>
        /// 得到未解锁角色的下标
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int getAllLockedIndex(string role)
        {
            int index = 0;
            int len = AllLockeds.Length;
            for (int i = 0; i < len; i++)
            {
                if (AllLockeds[i].name.ToLower().Equals(role.ToLower()))
                {
                    return i;
                }
            }
            return index;
        }

        /// <summary>
        /// 得到解锁角色的下标
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
         int getAllSpriteIndex(string role)
        {
            int index = 0;
            int len = AllSprite.Length;
            for (int i = 0; i < len; i++)
            {
                if (AllSprite[i].name.ToLower().Equals(role.ToLower()))
                {
                    return i;
                }
            }
            return index;
        }
        void MakeCenter(int centerIndex)
        {
            //int centerIndex = getAllSpriteIndex(role);
            preIndex = centerIndex - 1;
            nextIndex = centerIndex + 1;
            int RolesIndex = 0;
            for (int i = preIndex; i != nextIndex + 1; i++)
            {
                int index = 0;
                if (i < 0)
                {
                    index = AllSprite.Length - Mathf.Abs(i % AllSprite.Length);
                    if (index >= AllSprite.Length)
                    {
                        index = 0;
                    }
                }
                else
                {
                    index = Mathf.Abs(i % AllSprite.Length);
                }
                // 循环显示Sprite
                if (PlayerData.IsRoleLock(AllSprite[index].name))
                {
                    Roles[RolesIndex].sprite = AllLockeds[index]; 
                }
                else
                {
                    Roles[RolesIndex].sprite = AllSprite[index];
                }
                Roles[RolesIndex].GetComponent<RoleItem>().RoleName = AllSprite[index].name;
                RolesIndex++;
            }
        }

        void OnSlideLeft()
        {
            if (DOTween.IsTweening(Roles[0].transform))
            {
                return;
            }
            // 从右到左
            int len = Roles.Count;
            Vector3 position = Roles[len - 1].rectTransform.localPosition;
            Vector3 scale = Roles[len - 1].rectTransform.localScale;
            for (int i = len - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    Roles[i].transform.DOLocalMove(Roles[i - 1].transform.localPosition, AnimatTime);
                    Roles[i].transform.DOScale(Roles[i - 1].transform.localScale, AnimatTime);
                }
                else
                {
                    preIndex++;
                    nextIndex++;

                    int index = 0;
                    if (nextIndex < 0)
                    {
                        index = AllSprite.Length - Mathf.Abs(nextIndex % AllSprite.Length);
                       
                        if (index == AllSprite.Length)
                        {
                            index = 0;
                        }
                    }
                    else
                    {
                        index = Mathf.Abs(nextIndex % AllSprite.Length);
                    }

                    // 循环显示Sprite
                    if (PlayerData.IsRoleLock(AllSprite[index].name))
                    {
                        Roles[i].sprite = AllLockeds[index];
                    }
                    else
                    {
                        Roles[i].sprite = AllSprite[index];
                    }
                    Roles[i].GetComponent<RoleItem>().RoleName = AllSprite[index].name;
                    Roles[i].DOFade(0, 0);
                    Roles[i].rectTransform.localScale = Vector3.zero;
                    Roles[i].rectTransform.localPosition = position;
                    Roles[i].transform.DOScale(scale, AnimatTime);
                    Roles[i].DOFade(1, AnimatTime).OnComplete(delegate
                    {
                        var tmp = Roles[0];
                        Roles.RemoveAt(0);
                        Roles.Add(tmp);
                        SetCurSelect();
                    });
                }
            }
        }

        void OnSlideRight()
        {
            if (DOTween.IsTweening(Roles[0].transform))
            {
                return;
            }
            // 从左到右
            int len = Roles.Count;
            Vector3 position = Roles[0].rectTransform.localPosition;
            Vector3 scale = Roles[0].rectTransform.localScale;
            for (int i = 0; i < len; i++)
            {
                if (i != len - 1)
                {
                    Roles[i].transform.DOLocalMove(Roles[i + 1].transform.localPosition, AnimatTime);
                    Roles[i].transform.DOScale(Roles[i + 1].transform.localScale, AnimatTime);
                }
                else
                {
                    preIndex--;
                    nextIndex--;
                    // 循环显示Sprite
                    int index = 0;
                    if (preIndex < 0)
                    {
                        index = AllSprite.Length - Mathf.Abs(preIndex % AllSprite.Length);
                        if (index == AllSprite.Length)
                        {
                            index = 0;
                        }
                    }
                    else
                    {
                        index = Mathf.Abs(preIndex % AllSprite.Length);
                    }

                    if (PlayerData.IsRoleLock(AllSprite[index].name))
                    {
                        Roles[i].sprite = AllLockeds[index];
                    }
                    else
                    {
                        Roles[i].sprite = AllSprite[index];
                    }
                   
                    Roles[i].GetComponent<RoleItem>().RoleName = AllSprite[index].name;
                    Roles[i].DOFade(0, AnimatTime);
                    Roles[i].rectTransform.localScale = Vector3.zero;
                    Roles[i].rectTransform.localPosition = position;
                    Roles[i].transform.DOScale(scale, AnimatTime);
                    Roles[i].DOFade(1, AnimatTime).OnComplete(delegate
                    {
                        var tmp = Roles[len - 1];
                        Roles.RemoveAt(len - 1);
                        Roles.Insert(0, tmp);
                        SetCurSelect();
                    });
                }
            }
        }

        void SetCurSelect()
        {
            string selectRole = Roles[Roles.Count / 2].GetComponent<RoleItem>().RoleName;
            if (!PlayerData.IsRoleLock(selectRole))
            {
                PlayerData.CurRole = selectRole;
                if (OnRoleSelected != null)
                {
                    OnRoleSelected(selectRole);
                }
            }
        }
    }
}
