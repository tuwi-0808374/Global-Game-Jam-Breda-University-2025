using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StockMarket : MonoBehaviour
{
    [System.Serializable]
    public class Stock
    {
        public string Name;
        public string Description;
        public float CurrentPrice;
        public float ChangeRate;
        public int PlayerShares;

        public Stock(string name, float price, float changeRate)
        {
            Name = name;
            CurrentPrice = price;
            ChangeRate = changeRate;
            PlayerShares = 0;
        }
    }

    public List<Stock> Stocks = new List<Stock>();
    public float PlayerMoney = 100.0f;

    public GameObject StockItemPrefab; // Prefab for a stock item in the UI
    public Transform StockListContainer; // Parent container for stock items
    public TextMeshProUGUI PlayerMoneyText; // Text element to display player's money

    void Start()
    {
        // Initialize stocks
        Stocks.Add(new Stock("BubbleTea", 8.0f, 0.8f));
        Stocks.Add(new Stock("Health", 10.0f, 1.0f));
        Stocks.Add(new Stock("Crypto", 20.0f, 2.0f));
        Stocks.Add(new Stock("Defense", 15.0f, 1.5f));
        Stocks.Add(new Stock("Education", 20.0f, 2.0f));
        Stocks.Add(new Stock("Prison", 12.0f, 1.2f));
        Stocks.Add(new Stock("Kawai Merch", 20.0f, 2.0f));
        Stocks.Add(new Stock("Theme Parks", 20.0f, 2.0f));

        // Populate the stock list UI
        PopulateStockList();

        // Start updating stock prices
        InvokeRepeating("UpdateStockPrices", 2.0f, 5.0f);
    }

    void PopulateStockList()
    {
        foreach (var stock in Stocks)
        {
            GameObject stockItem = Instantiate(StockItemPrefab, StockListContainer);

            // Update stock UI elements
            stockItem.transform.Find("StockName").GetComponent<TextMeshProUGUI>().text = stock.Name;
            stockItem.transform.Find("StockPrice").GetComponent<TextMeshProUGUI>().text = $"${stock.CurrentPrice:F2}";
            stockItem.transform.Find("PlayerShares").GetComponent<TextMeshProUGUI>().text = $"x{stock.PlayerShares}";

            // Add Buy button functionality
            Button buyButton = stockItem.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => BuyStock(Stocks.IndexOf(stock), 1));

            // Add Sell button functionality
            Button sellButton = stockItem.transform.Find("SellButton").GetComponent<Button>();
            sellButton.onClick.AddListener(() => SellStock(Stocks.IndexOf(stock), 1));
        }
    }

    void UpdateStockPrices()
    {
        foreach (var stock in Stocks)
        {
            stock.CurrentPrice *= 1 + Random.Range(-stock.ChangeRate, stock.ChangeRate);
            stock.CurrentPrice = Mathf.Max(stock.CurrentPrice, 0.1f); // Prevent negative prices
        }

        UpdateUI();
    }

    public void BuyStock(int stockIndex, int quantity)
    {
        Stock stock = Stocks[stockIndex];
        float cost = stock.CurrentPrice * quantity;

        if (PlayerMoney >= cost)
        {
            PlayerMoney -= cost;
            stock.PlayerShares += quantity;
        }
        else
        {
            Debug.LogWarning("Not enough money to buy shares!");
        }

        UpdateUI();
    }

    public void SellStock(int stockIndex, int quantity)
    {
        Stock stock = Stocks[stockIndex];
        if (stock.PlayerShares >= quantity)
        {
            float earnings = stock.CurrentPrice * quantity;
            PlayerMoney += earnings;
            stock.PlayerShares -= quantity;
        }
        else
        {
            Debug.LogWarning("Not enough shares to sell!");
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Update player money display
        PlayerMoneyText.text = $"Cash: ${PlayerMoney:F2}  Stock portfolio: {CalculateNetWorth():F2}";

        // Update stock list
        for (int i = 0; i < StockListContainer.childCount; i++)
        {
            var stockItem = StockListContainer.GetChild(i);
            var stock = Stocks[i];

            var priceText = stockItem.Find("StockPrice").GetComponent<TextMeshProUGUI>();
            float previousPrice = float.Parse(priceText.text.Split('$')[1]);
            priceText.text = $"${stock.CurrentPrice:F2}";

            // Change color based on price movement
            if (stock.CurrentPrice > previousPrice)
                priceText.color = Color.green;
            else if (stock.CurrentPrice < previousPrice)
                priceText.color = Color.red;
            else
                priceText.color = Color.white;

            stockItem.Find("PlayerShares").GetComponent<TextMeshProUGUI>().text = $"Shares: {stock.PlayerShares}";
        }
    }

    float CalculateNetWorth()
    {
        float netWorthStocks = 0;
        foreach (var stock in Stocks)
        {
            netWorthStocks += stock.PlayerShares * stock.CurrentPrice;
        }
        return netWorthStocks;
    }
}
