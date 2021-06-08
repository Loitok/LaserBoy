using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuy : MonoBehaviour
{
    public enum ItemType
    {
        Gold50000,
        Gold150000,
        Gold250000,
        Gold500000,
        Golem_White,
        Golem_Black
    }

    public ItemType itemType;

    public Text priceText;

    private string defaultText;

    void Start()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }
    public void ClickBuy()
    {
        switch(itemType)
        {
            case ItemType.Gold50000:
                IAPManager.Instance.Buy50000Gold();
                break;
            case ItemType.Gold150000:
                IAPManager.Instance.Buy150000Gold();
                break;
            case ItemType.Gold250000:
                IAPManager.Instance.Buy250000Gold();
                break;
            case ItemType.Gold500000:
                IAPManager.Instance.Buy500000Gold();
                break;
            case ItemType.Golem_White:
                IAPManager.Instance.BuyGolemWhite();
                break;
            case ItemType.Golem_Black:
                IAPManager.Instance.BuyGolemBlack();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!IAPManager.Instance.IsInitialized())
            yield return null;

        string loadedPrice = "";

        switch (itemType)
        {
            case ItemType.Gold50000:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.GOLD_50000);
                break;
            case ItemType.Gold150000:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.GOLD_150000);
                break;
            case ItemType.Gold250000:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.GOLD_250000);
                break;
            case ItemType.Gold500000:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.GOLD_500000);
                break;
            case ItemType.Golem_White:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.Golem_White);
                break;
            case ItemType.Golem_Black:
                loadedPrice = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.Golem_Black);
                break;
        }

        priceText.text = defaultText + " " + loadedPrice;
    }
}
