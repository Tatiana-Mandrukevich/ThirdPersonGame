using TMPro;
using UnityEngine;

public class CoinStatisticsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsNumber;
    
    private int _totalCoins;

    public void AddCoin()
    {
        _totalCoins++;
    }

    public void ShowActualTotalCoins()
    {
        _coinsNumber.text = _totalCoins.ToString();
    }
}