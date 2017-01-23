using UnityEngine;
using System.Collections;
using System.Text;
using System;

namespace BaiyiGame.JumpMrQ
{
    public class PlayerData
    {
        public delegate void _SetDiamond(int diamond);
        public static event _SetDiamond OnSetDiamond;

        private static string curRole = null;
        public static string CurRole
        {
            get
            {
                if (string.IsNullOrEmpty(curRole))
                {
                    curRole = PlayerPrefs.GetString(Constants.PlayerPrefs.SELECT_ROLE);
                    if (string.IsNullOrEmpty(curRole))
                    {
                        curRole = Constants.DEFAULE_ROLE;
                        PlayerPrefs.SetString(Constants.PlayerPrefs.SELECT_ROLE, curRole);
                    }
                }
                Debug.Log("Get CurRole :" + curRole);
                return curRole;
            }

            set
            {
                Debug.Log("Set CurRole :" + value);
                PlayerPrefs.SetString(Constants.PlayerPrefs.SELECT_ROLE, value);
                curRole = value;
            }
        }

        public static bool IsRoleLock(string role)
        {
            string key = role.ToLower() + "_Lock";
            int value = PlayerPrefs.GetInt(key, -1);
            return value == 0 ? false : true;
        }

        public static void UnLockRole(string role)
        {
            string key = role.ToLower() + "_Lock";
            Debug.Log("current buy role key is : "+key);
            PlayerPrefs.SetInt(key, 0);
        }

        public static int diamond
        {
            get
            {
                int l = PlayerPrefs.GetInt("DIAMOND", 0);

                if (l < 0)
                {
                    l = 0;
                    PlayerPrefs.SetInt("DIAMOND", l);
                    PlayerPrefs.Save();

                    if (OnSetDiamond != null)
                        OnSetDiamond(l);
                }

                return l;
            }

            set
            {
                PlayerPrefs.SetInt("DIAMOND", value);
                PlayerPrefs.Save();

                if (OnSetDiamond != null)
                    OnSetDiamond(value);
            }
        }

        public static int AddDiamond(int n)
        {
            diamond += n;
            return diamond;
        }

        internal static void SetComment(int value)
        {
            PlayerPrefs.SetInt("Comment", value);
        }
        /// <summary>
        /// 是否已经评论
        /// </summary>
        /// <returns></returns>
        internal static bool IsCommented()
        {
            return PlayerPrefs.GetInt("Comment", 0) == 1;
        }
    }
}
