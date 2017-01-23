using System;

public class InMobiInstance
{
    public static InMoBiAds Instance { get; set; }
    public static InMoBiAds GetInstance()
    {
        if (InMobiInstance.Instance == null)
        {
            InMobiInstance.Instance = InMoBi.GetInMoBiAds();
        }
        return InMobiInstance.Instance;
    }
}