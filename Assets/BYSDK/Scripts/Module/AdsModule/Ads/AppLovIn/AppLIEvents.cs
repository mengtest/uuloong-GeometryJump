using UnityEngine;
using System.Collections;
using System;

public class AppLIEvents  {

    #region Interstitial ads
    /// <summary>
    ///  interstitial ads load successful
    /// </summary>
    public const string INTERLOAD_SUCCESS   = "LOADEDINTER";

    /// <summary>
    /// play interstitial ads
    /// </summary>
    public const string DISPLAY_INTER = "DISPLAYEDINTER";

    /// <summary>
    ///  close the Interstitial ads
    /// </summary>
    public const string HIDDEN_INTER = "HIDDENINTER";

    /// <summary>
    ///  interstitial ads load failed
    /// </summary>
    public const string INTERLOAD_FAILED    = "LOADINTERFAILED";

    #endregion


    #region Rewarded Video event
    /// <summary>
    ///  rewarded video load successful
    /// </summary>
    public const string VIDEOLOAD_SUCCESS   = "LOADEDREWARDED";

    /// <summary>
    /// rewarded video load failed
    /// </summary>
    public const string VIDEOLOAD_FAILED    = "LOADREWARDEDFAILED";

    /// <summary>
    /// play rewarded video ads
    /// </summary>
    public const string DISPLAY_VIDEO       = "DISPLAYEDREWARDED";


    /// <summary>
    ///  close rewarded video ads ,need to reload ads
    /// </summary>
    public const string HIDDEN_VIDEO        = "HIDDENREWARDED";

    /// <summary>
    /// rewarded video been closed by user before the video not stop. need to reload ads
    /// </summary>
    public const string SKIP_VIDEO          = "USERCLOSEDEARLY";

    /// <summary>
    ///  begin to play rewarded video
    /// </summary>
    public const string VIDEO_PLAY          = "VIDEOBEGAN";

    /// <summary>
    /// stop to play rewarded video
    /// </summary>
    public const string VIDEO_STOPED        = "VIDEOSTOPPED";

    /// <summary>
    /// the service timeout: network error
    /// </summary>
    public const string VIDEO_TIMEOUT       = "REWARDTIMEOUT";

    #endregion
}
