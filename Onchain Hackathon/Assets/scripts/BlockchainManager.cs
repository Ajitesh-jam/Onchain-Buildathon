
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

    string abi = "[{\"type\":\"function\",\"name\":\"getRank\",\"inputs\":[{\"type\":\"address\",\"name\":\"_player\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"playerScores\",\"inputs\":[{\"type\":\"address\",\"name\":\"\",\"internalType\":\"address\"}],\"outputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"players\",\"inputs\":[{\"type\":\"uint256\",\"name\":\"\",\"internalType\":\"uint256\"}],\"outputs\":[{\"type\":\"address\",\"name\":\"playerAddress\",\"internalType\":\"address\"},{\"type\":\"uint256\",\"name\":\"score\",\"internalType\":\"uint256\"}],\"stateMutability\":\"view\"},{\"type\":\"function\",\"name\":\"submitScore\",\"inputs\":[{\"type\":\"uint256\",\"name\":\"_score\",\"internalType\":\"uint256\"}],\"outputs\":[],\"stateMutability\":\"nonpayable\"}]";



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
         Address = await ThirdwebManager.Instance.SDK.Wallet.Connect(connection);//W capital hai doc me galat likha hai
        //Address = await ThirdwebManager.Instance.SDK.Connect(connection);

        OnLoggedIn?.Invoke(Address);
       // InvokeLogIn();

        //iske baad game start kar saktey 
    }

    //public void InvokeLogIn()
    //{
    //    OnLoggedIn.Invoke(Address);
    //    GetTokenBalance();
    //}

    public async void ClaimScore()
    {
        claimText.text = "Clamming...";
        claimTokens.interactable = false;
        var sdk = ThirdwebManager.Instance.SDK;
        var contract = sdk.GetContract("0xdFadC341C78Ff6Ec91c1789f4A92bad2ADF2BE06");
        Debug.Log("contract : ");
        Debug.Log( contract);
        var result = await contract.ERC20.ClaimTo(Address,numOfToken);
        Debug.Log("result   :  " + result);
        claimText.text = "Done";
        GetTokenBalance();


    }

    public async void SubmitScore(int distanceTravelled)
    {
        Debug.Log($"Submitting score of {distanceTravelled} to blockchain for address {Address}");
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x7161636060D3f7692a3CF2ED395A29d05763b2e4", abi);
        await contract.Write("submitScore", distanceTravelled);

        GetRank();
    }

    public async void GetRank()
    {
        Debug.Log("Get Rank");
        var contract = ThirdwebManager.Instance.SDK.GetContract(
            "0x7161636060D3f7692a3CF2ED395A29d05763b2e4", abi);
        var rank = await contract.Read<int>("getRank", Address);
        Debug.Log($"Rank for address {Address} is {rank}");
     
   }

    public async void GetTokenBalance()
    {
        Debug.Log("Get Token balanace");
        Debug.Log(Address);
        var sdk = ThirdwebManager.Instance.SDK;
        var contract = sdk.GetContract("0xdFadC341C78Ff6Ec91c1789f4A92bad2ADF2BE06");
        var balance = await contract.ERC20.BalanceOf(Address);
        claimText.text = "Balance : " + balance.displayValue;

    }
}