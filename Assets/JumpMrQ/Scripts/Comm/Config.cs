using UnityEngine;
using System.Collections;

namespace BaiyiGame.JumpMrQ
{
    public class Config
    {
        // 角色跳到柱子上之后 柱子和角色的下沉距离
        public static readonly float SINK_DISTANCE = 0.07f;

        // 动画时间
        public static readonly float ANIM_TIME_SHORT = 0.1f;
        public static readonly float ANIM_TIME_MID = 0.8f;
        public static readonly float ANIM_TIME_LONG = 1.2f;

        // 继续游戏消耗的钻石数量
        public static readonly int DIAMOND_TO_CONTINUE= 100;

        // 解锁人物花费的钻石
        public static readonly int DIAMOND_TO_UNLOCK = 100; 

        // 钻石出现的概率为1/30
        public static readonly int DIAMOND_PROBABILITY = 30;

		// 钻石首页奖励数量
		public static readonly int DIAMOND_HOMECOUNT = 10;
		// 钻石游戏奖励数量
		public static readonly int DIAMOND_GAMECOUNT = 20;
    }
}