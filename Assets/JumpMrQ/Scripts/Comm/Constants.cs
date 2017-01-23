using UnityEngine;
using System.Collections;

namespace BaiyiGame.JumpMrQ
{
    public class Constants
    {
        public class Resources
        {
            public static readonly string Cauliflower = "Prefabs/Roles/Cauliflower";
            public static readonly string Corpse = "Prefabs/Roles/Corpse";
            public static readonly string Dog = "Prefabs/Roles/Dog";
            public static readonly string Glassesman = "Prefabs/Roles/Glassesman";
            public static readonly string Greenxx = "Prefabs/Roles/Greenxx";
            public static readonly string Indian = "Prefabs/Roles/Indian";
            public static readonly string King = "Prefabs/Roles/King";
            public static readonly string Penguin = "Prefabs/Roles/Penguin";
            public static readonly string Pig = "Prefabs/Roles/Pig";
            public static readonly string Pirate = "Prefabs/Roles/Pirate";
            public static readonly string Redman = "Prefabs/Roles/Redman";
            public static readonly string Sportman = "Prefabs/Roles/Sportman";
        }

        public class PlayerPrefs
        {
            public static readonly string SELECT_ROLE = "SELECT_ROLE";
        }

        // 默认的角色
        public static readonly string DEFAULE_ROLE = "1";

        //选中的角色
        public static string SELECT_ROLE_NAME = DEFAULE_ROLE;
    }
}
