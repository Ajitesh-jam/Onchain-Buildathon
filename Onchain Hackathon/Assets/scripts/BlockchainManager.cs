
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class BlockchainManager : MonoBehaviour
{
    public UnityEvent<string> OnLoggedIn;

    public string Address { get; private set; }

    public static BlockchainManager Instance { get; private set; }


    public Button claimTokens;
    public TextMeshProUGUI claimText;
    public string numOfToken ;

    private void Awake()//only one instant of this script.
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public async void Login(string authProvider)
    {
        AuthProvider provider = AuthProvider.Google;
        switch (authProvider)
        {
            case "google":
                provider = AuthProvider.Google;
                break;
            case "apple":
                provider = AuthProvider.Apple;
                break;
            case "facebook":
                provider = AuthProvider.Facebook;
                break;
        }

        var connection = new WalletConnection(
            provider: WalletProvider.SmartWallet,
            chainId: 84532,
            personalWallet: WalletProvider.InAppWallet,
            authOptions: new AuthOptions(authProvider: provider)
        );
        string Address = await ThirdwebManager.Instance.SDK.Wallet.Connect(connection);//W capital hai doc me galat likha hai
        //Address = await ThirdwebManager.Instance.SDK.Connect(connection);

        OnLoggedIn?.Invoke(Address);

        //iske baad game start kar saktey 
    }

    public async void ClaimScore()
    {
        claimText.text = "Clamming...";
        claimTokens.interactable = false;
        var sdk = ThirdwebManager.Instance.SDK;
        var contract = sdk.GetContract("0xdFadC341C78Ff6Ec91c1789f4A92bad2ADF2BE06");
        Debug.Log("contract : ");Debug.Log( contract);
        var result = await contract.ERC20.ClaimTo(Address, numOfToken);
        Debug.Log("result" + result);
        claimText.text = "Done";



    }

    internal async Task SubmitScore(float distanceTravelled)
    {
        Debug.Log($"Submitting score of {distanceTravelled} to blockchain for address {Address}");
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x9d9a1f4c1a685857a5666db45588aa3d5643af9f",
            "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"ScoreAdded\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"}],\"name\":\"getRank\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"rank\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"submitScore\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]"
        );
        await contract.Write("submitScore", (int)distanceTravelled);
    }

    internal async Task<int> GetRank()
    {
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x9d9a1f4c1a685857a5666db45588aa3d5643af9f",
            "[{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"ScoreAdded\",\"type\":\"event\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"player\",\"type\":\"address\"}],\"name\":\"getRank\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"rank\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"submitScore\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]"
        );
        var rank = await contract.Read<int>("getRank", Address);
        Debug.Log($"Rank for address {Address} is {rank}");
        return rank;
    }
}