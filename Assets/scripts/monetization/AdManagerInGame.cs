using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManagerInGame : MonoBehaviour
{
    // Start is called before the first frame update]

    private AdStore AdStore;
    private GameObject gameManager;
    private adRewaards rewards;

    private void Start() {
        this.AdStore = this.GetComponent<AdStore>();
        rewards = this.GetComponent<adRewaards>();

        AdStore.RequestRewardedAd();
        AdStore.RequestInterstitial();

    }
    public bool RewardedIsLoaded() => AdStore.RewardedIsLoaded();

    public void ShowBanner() => AdStore.ShowBanner();

    public void KillBaner() => AdStore.KillBanner();

    public void ShowRewardVideoForReveive() => AdStore.ShowRewardedAd(rewards.EarnedRevive);

    public void ShowRewardVideoForDimonds() => AdStore.ShowRewardedAd(rewards.EarnedDimonds);

    public void ShowRewardVideoForGold() => AdStore.ShowRewardedAd(rewards.EarnedGold);

    public void ShowInterstitial() => AdStore.ShowInterstitial();

}
