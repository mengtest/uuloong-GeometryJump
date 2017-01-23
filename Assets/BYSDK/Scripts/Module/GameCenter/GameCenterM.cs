using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

namespace BYSDKManage {
public class GameCenterM : object {

    private static GameCenterM _instance;
#if UNITY_IPHONE
    public ILeaderboard leaderBoard;
#endif

    private bool isLoad = false;

    public static GameCenterM GetInstance() {
        if (null == _instance) {
            _instance = new GameCenterM();
        }
        return _instance;
    }

    GameCenterM() {
#if UNITY_IPHONE
        Social.localUser.Authenticate(HandleAuthenticated);
#endif
    }

    private void LoadLeaderBoard() {
#if UNITY_IPHONE
        leaderBoard = Social.CreateLeaderboard();
        leaderBoard.id = Config.Leaderboar;
        leaderBoard.LoadScores(DidLoadLeaderBoard);
#endif
    }

    private void DidLoadLeaderBoard(bool resulte) {
#if UNITY_IPHONE
        if ( resulte) {
            Debug.Log("load leadboard success");
        } else {
            Debug.Log("load leadboard fail");
        }
#endif
    }
// authentication

    private void HandleAuthenticated(bool success) {
#if UNITY_IPHONE
        Debug.Log("*** HandleAuthenticated: success = " + success);
        if (success) {
            LoadLeaderBoard();
            isLoad = true;
        } else {
            isLoad = false;
        }
#endif
    }

    private void HandleFriendsLoaded(bool success) {
#if UNITY_IPHONE
        Debug.Log("*** HandleFriendsLoaded: success = " + success);
        foreach (IUserProfile friend in Social.localUser.friends) {
            Debug.Log("*   friend = " + friend.ToString());
        }
#endif
    }

    private void HandleAchievementsLoaded(IAchievement[] achievements) {
#if UNITY_IPHONE
        Debug.Log("*** HandleAchievementsLoaded");
        foreach (IAchievement achievement in achievements) {
            Debug.Log("*   achievement = " + achievement.ToString());
        }
#endif
    }

    private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions) {
#if UNITY_IPHONE
        Debug.Log("*** HandleAchievementDescriptionsLoaded");
        foreach (IAchievementDescription achievementDescription in achievementDescriptions) {
            Debug.Log("*   achievementDescription = " + achievementDescription.ToString());
        }
#endif
    }

// 成就 achievements

/// <summary>
/// 上传成就
/// </summary>
/// <param name="achievementId">成就ID</param>
/// <param name="progress">成就点数</param>
    public void ReportProgress(string achievementId, double progress) {
#if UNITY_IPHONE
        if (Social.localUser.authenticated) {
            Social.ReportProgress(achievementId, progress, HandleProgressReported);
            Debug.Log("GameCenter ReportResulte: " + achievementId + " " + progress);
        }
#endif
    }

/// <summary>
///  上传成就的回调函数
/// </summary>
/// <param name="success">是否成功</param>
    private void HandleProgressReported(bool success) {
#if UNITY_IPHONE
        Debug.Log("*** HandleProgressReported: success = " + success);
#endif
    }



// 排行榜 leaderboard

/// <summary>
/// 上传分数
/// </summary>
/// <param name="score">分数</param>
    public void ReportScore(long score) {
#if UNITY_IPHONE
        if (Social.localUser.authenticated) {
            Social.ReportScore(score, Config.Leaderboar, HandleScoreReported);
            Debug.Log("GameCenter Score: " + Config.Leaderboar + " " + score);
        }
#endif
    }

/// <summary>
/// 分数发送到GameCenter的回调函数
/// </summary>
/// <param name="success"></param>
    public void HandleScoreReported(bool success) {
#if UNITY_IPHONE
        Debug.Log("*** HandleScoreReported: success = " + success);
#endif
    }

/// <summary>
///  显示排行信息
/// </summary>
    public void ShowLeaderboard(Action handle = null) {
#if UNITY_IPHONE
        if (Social.localUser.authenticated) {
            Debug.Log("显示排行榜");
            Social.ShowLeaderboardUI();
        } else {
            if (null != handle) {
                handle();
            }
        }
#endif
    }

/// <summary>
/// 显示成就页面
/// </summary>
    public void ShowAchievements(Action handle = null) {
#if UNITY_IPHONE
        if (Social.localUser.authenticated) {
            Debug.Log("显示成就");
            Social.ShowAchievementsUI();
        } else {
            if (null != handle) {
                handle();
            }
        }
#endif
    }

}

}
