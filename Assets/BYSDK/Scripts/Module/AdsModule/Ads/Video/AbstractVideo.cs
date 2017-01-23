using System;
using BYSDKManage;
public abstract class AbstractVideo  {

    /// <summary>
    ///  增加该方法主要是为了在视屏播放时撤销loading界面
    /// </summary>
    abstract public event EventHandler<AdsEventArgs> OnVoidStart;

    /// <summary>
    /// 视屏个性化配置数据
    /// </summary>
    protected AdsIndividualData IndividualData;
    Vendors m_type;
    public Vendors MType {
        get {
            return m_type;
        } set {
            m_type = value;
        }
    }
    public BYVideoResulte videoresulte;
    public DataCollecter Collecter;

    abstract public void Load();
    abstract public void Display(BYVideoResulte resulte = null);
    abstract public void ReLoad();

    abstract public bool IsReady();
    abstract public bool CheckAds();

    abstract public void SetIndividualData(long _TimeWind,int count,string tag = "",bool isuse = false);
}
