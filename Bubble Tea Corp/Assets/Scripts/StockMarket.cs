using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using static StockMarket;

public class StockMarket : MonoBehaviour
{
    [System.Serializable]
    public class Stock
    {
        public string Name;
        public string Description;
        public float CurrentPrice;
        public float ChangeRate;
        public float ChangeRateDelta;
        public int PlayerShares;

        public Stock(string name, float price, float changeRate)
        {
            Name = name;
            CurrentPrice = price;
            ChangeRate = changeRate;
            ChangeRateDelta = 1f;
            PlayerShares = 0;
        }
    }

    public List<Stock> Stocks = new List<Stock>();
    public float PlayerMoney = 100.0f;
 
    public enum StockMods
    {
        Add,
        Overwrite,
        Multiply,
        Ignore
    }
    public List<List<float>> StockHistory = new List<List<float>> ();

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

        //Initialize stock history
        for (int i = 0; i < Stocks.Count; i++)
        {
            StockHistory.Add(new List<float>());
            StockHistory[i].Add(Stocks[i].CurrentPrice);
        }
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
            stock.ChangeRate *= stock.ChangeRateDelta;
            stock.CurrentPrice += stock.ChangeRate + UnityEngine.Random.Range(-1,1 );
            stock.CurrentPrice = Mathf.Max(stock.CurrentPrice, 0.1f); // Prevent negative prices

            //Figure out which stock we are updating
            int TargetStockIndex = Stocks.FindIndex(x => x.Name == stock.Name);
            List<float> SpecStockHist = StockHistory[TargetStockIndex];
            
            //Add the new value to the stock history
            SpecStockHist.Add(stock.CurrentPrice);
            StockHistory[TargetStockIndex] = SpecStockHist;
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

    List<int> NormalizeToRange(List<float> values, int minTarget, int maxTarget)
    {
        if (values == null || values.Count == 0)
            return new List<int>();

        float minValue = Mathf.Min(values.ToArray());
        float maxValue = Mathf.Max(values.ToArray());

        // Avoid division by zero if all values are the same
        if (Mathf.Approximately(maxValue, minValue))
        {
            // If all values are the same, map them all to the midpoint of the target range
            int midpoint = Mathf.RoundToInt((minTarget + maxTarget) / 2.0f);
            return new List<int>(new int[values.Count].Select(_ => midpoint));
        }

        List<int> result = new List<int>();

        foreach (float value in values)
        {
            // Normalize the value to the range 0 to 1
            float normalized = (value - minValue) / (maxValue - minValue);

            // Scale to the target range
            int scaledValue = Mathf.Clamp(Mathf.RoundToInt(normalized * (maxTarget - minTarget) + minTarget), minTarget, maxTarget);

            result.Add(scaledValue);
        }

        return result;
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

            stockItem.Find("PlayerShares").GetComponent<TextMeshProUGUI>().text = $"x{stock.PlayerShares}";

            List<float> currentStockHistory = StockHistory[i];
            //List<int> intList = currentStockHistory.Select(f => Mathf.RoundToInt(f)).ToList();
            List<float> trimmedList = currentStockHistory.TakeLast(15).ToList();
            List<int> normalizedList = NormalizeToRange(trimmedList, 0, 100);

            stockItem.GetComponentInChildren<Window_Graph>().valueList = normalizedList;
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
    public void UpdateStock(string StockName, StockMods ValueMod, float Value, StockMods ChangeRateMod, float ChangeRate, StockMods DeltaMod, float ChangeRateDelta)
    {
        Debug.Log("dsfdsfs " + StockName);
        // Find the stock you want to update
        int TargetStockIndex = Stocks.FindIndex(x => x.Name == StockName);
        Stock TargetStock = Stocks[TargetStockIndex];

        // Update the value of the stock
        switch (ValueMod)
        {
            case StockMods.Add:
                {
                    TargetStock.CurrentPrice += Value;
                    break;
                }
            case StockMods.Overwrite:
                {
                    TargetStock.CurrentPrice = Value;
                    break;
                }
            case StockMods.Multiply:
                {
                    TargetStock.CurrentPrice *= Value;
                    break;
                }
            case StockMods.Ignore:
                {
                    break;
                }
        }

        // Update the changerate of the stock
        switch (ChangeRateMod)
        {
            case StockMods.Add:
                {
                    TargetStock.ChangeRate += ChangeRate;
                    break;
                }
            case StockMods.Overwrite:
                {
                    TargetStock.ChangeRate = ChangeRate;
                    break;
                }
            case StockMods.Multiply:
                {
                    TargetStock.ChangeRate *= ChangeRate;
                    break;
                }
            case StockMods.Ignore:
                {
                    break;
                }
        }

        // update the change rate delta of the stock
        switch (DeltaMod)
        {
            case StockMods.Add:
                {
                    TargetStock.ChangeRateDelta += ChangeRateDelta;
                    break;
                }
            case StockMods.Overwrite:
                {
                    TargetStock.ChangeRateDelta = ChangeRateDelta;
                    break;
                }
            case StockMods.Multiply:
                {
                    TargetStock.ChangeRateDelta *= ChangeRateDelta;
                    break;
                }
            case StockMods.Ignore:
                {
                    break;
                }
        }

        //Update the stock list with the new modified stock
        Stocks[TargetStockIndex] = TargetStock;
    }
}
