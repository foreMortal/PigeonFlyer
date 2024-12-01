using UnityEngine;

public class ShopObjectPigeon : ObjectInShop
{
    [Space]
    [SerializeField] private int PigeonIndex;
    [SerializeField] private GameObject pickButton;

    private void Awake()
    {
        menuObject = new PigeonMenuObject(PigeonIndex);
    }

    public override void UpdateUI(bool bought, int playersCoins)
    {
        base.UpdateUI(bought, playersCoins);

        if (bought)
        {
            pickButton.SetActive(true);

            if(shop.ChoosenPigeon == PigeonIndex) pickButton.SetActive(false);
        }
    }

    public override void Buy()
    {
        pass = false;
        base.Buy();
    }

    public void ChoosePieon()
    {
        shop.ChoosePigeon(PigeonIndex);
        shop.BuyObject(0, index, menuObject, true);
    }
}
