using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private CoinStatisticsUI _coinStatisticsUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            var coin = other.transform.parent.GetComponent<Coin>();
            coin.DoSmall();
            Destroy(other.gameObject);

            _coinStatisticsUI.AddCoin();
            _coinStatisticsUI.ShowActualTotalCoins();
        }
    }
}
