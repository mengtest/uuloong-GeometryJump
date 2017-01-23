using UnityEngine.UI;
using BaiyiGame.JumpMrQ;
using UnityEngine;
using DG.Tweening;

public class BuyRoles : MonoBehaviour {

    [SerializeField]
    private Button btnBuyRole;
    [SerializeField]
    private GameObject rateDiamondManager;
    private SoundManager soundManager;
    [SerializeField]
    private GameObject go;

    // Use this for initialization
    void Awake () {
        btnBuyRole.onClick.AddListener(BuyRole);
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }
    void Start()
    { }
    void OnEnable()
    {
    }

    void OnDisable()
    {
    }
  
    public void BuyRole()
    {
        int diamondCount = PlayerPrefs.GetInt("DIAMOND", 0);
        if (diamondCount < BaiyiGame.JumpMrQ.Config.DIAMOND_TO_UNLOCK)
        {
            //钻石不够
            rateDiamondManager.gameObject.SetActive(true);
            Tweener tw = rateDiamondManager.GetComponent<CanvasGroup>().DOFade(0.8f, 1f);
        }
        else
        {
            int diamond = PlayerPrefs.GetInt("DIAMOND", 0);
            diamond -= BaiyiGame.JumpMrQ.Config.DIAMOND_TO_UNLOCK;
            PlayerPrefs.SetInt("DIAMOND", diamond);
            PlayerPrefs.Save();
            this.gameObject.SetActive(false);
            RoleSelectManager.UnLockRole(go);
            soundManager.PalyUnlockRoleFX();
        }
    }
}
