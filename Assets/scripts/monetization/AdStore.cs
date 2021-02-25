using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdStore : MonoBehaviour
{
    private RewardedAd RewardedAd;
    private InterstitialAd InterstitialView;
    private BannerView BannerView;
    private AdRequest Request;
    // Start is called before the first frame update
    void Start()
    {
        string appId = "ca-app-pub-4586176489276901~7997205751";
        MobileAds.Initialize(appId);
        GetNewRequest();
    }

    public void ShowBanner()
    {
        string bannerAdId = "ca-app-pub-3940256099942544/6300978111";
        BannerView = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);
       
        BannerView.OnAdFailedToLoad += HandleOnBannerFailedToLoad;
        BannerView.OnAdClosed += HandleOnBannerClosed;
        BannerView.LoadAd(Request);
    }

    public void KillBanner()
    {
        BannerView.Destroy();
    }
    public void RequestInterstitial()
    {
        GetNewRequest();
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        InterstitialView = new InterstitialAd(adUnitId);
        InterstitialView.LoadAd(Request);
    }

    public void ShowInterstitial()
    {
        if(InterstitialView != null && InterstitialView.IsLoaded())
        {
            InterstitialView.Show();
        }
        else
        {
            RequestInterstitial();
        }
    }

    public void KillInterstitial()
    {
        InterstitialView.Destroy();
        RequestInterstitial();
    }

    public bool RewardedIsLoaded() => RewardedAd.IsLoaded();

    public void RequestRewardedAd()
    {
        GetNewRequest();
        string rewardedAD= "ca-app-pub-3940256099942544/5224354917";
        RewardedAd = new RewardedAd(rewardedAD);
        RewardedAd.LoadAd(Request);
    }

    public void ShowRewardedAd(System.EventHandler<GoogleMobileAds.Api.Reward> earnedReward)
    {
        if (this.RewardedAd.IsLoaded())
        {
            this.RewardedAd.Show();
            RewardedAd.OnUserEarnedReward += earnedReward;
        }
        else
        {
            RequestRewardedAd();
        }
    }

    private void GetNewRequest()
    {
        Request = new AdRequest.Builder().AddKeyword("game").Build();
    }
    private void HandleOnBannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        ShowBanner();
    }

    private void HandleOnBannerClosed(object sender, EventArgs args)
    {
        ShowBanner();
    }

    private void HandleOnInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
    }

    private void HandleOnInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    private void HandleOnRewardedFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestRewardedAd();
    }

    private void HandleOnRewardedClosed(object sender, EventArgs args)
    {
        RequestRewardedAd();
    }
}
