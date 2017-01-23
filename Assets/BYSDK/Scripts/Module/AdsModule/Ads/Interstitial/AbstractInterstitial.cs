using UnityEngine;
using System;

public abstract class InterstitialAds {
    private Vendors m_type;

    public Vendors MType {
        get {
            return m_type;
        } set {
            m_type = value;
        }
    }
    protected AdsIndividualData IndividualData;
    public DataCollecter Collecter;
    abstract public event EventHandler<AdsEventArgs> AdLoaded;
    abstract public event EventHandler<AdsEventArgs> AdFailedToLoad;
    abstract public event EventHandler<AdsEventArgs> AdOpened;
    abstract public event EventHandler<AdsEventArgs> AdClosed;

    abstract public void Load();
    abstract public void Display();
    abstract public void ReLoad();

    abstract public bool IsReady();
    abstract public void CheckAds();
    abstract public void SetIndividualData(long _TimeWind, int count,string tag = "",bool isuse = false);
}