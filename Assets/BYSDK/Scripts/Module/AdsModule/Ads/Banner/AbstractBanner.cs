using System;
using UnityEngine;

public class AdsEventArgs : EventArgs {
    public string Message {
        get;
        set;
    }
}

public abstract class BannerAds {
    private Vendors m_type;
    public Vendors MType {
        get {
            return m_type;
        } set {
            m_type = value;
        }
    }

    public AdsIndividualData IndividualData;
    public DataCollecter    Collecter;
    abstract public event EventHandler<AdsEventArgs> AdLoaded;
    abstract public event EventHandler<AdsEventArgs> AdFailedToLoad;
    abstract public event EventHandler<AdsEventArgs> AdOpened;
    abstract public event EventHandler<AdsEventArgs> AdClosed;

    abstract public void LoadAndDisplay();

    abstract public void CheckAds();

    abstract public void Hide();

    abstract public void Destroy();
    abstract public void SetIndividualData(long _TimeWind, int count);
}