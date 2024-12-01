using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectInShop : MonoBehaviour
{
    [SerializeField] protected int index, cost;
    [SerializeField] private GameObject button;
    [SerializeField] private Color canBuyColor, notEnoughColor;
    [SerializeField] private Image image;
    [SerializeField] protected Shop shop;
    [SerializeField] private Text costText;

    protected IMenuObject menuObject;
    protected bool pass = true;
    private int playersCoins;
    public int Index { get { return index; } }

    public virtual void UpdateUI(bool bought, int playersCoins)
    {
        if (!bought)
        {
            if(playersCoins >= cost) image.color = canBuyColor;
            else image.color = notEnoughColor;
        }
        else button.SetActive(false);

        costText.text = cost.ToString(); 
        this.playersCoins = playersCoins;
    }

    public virtual void Buy()
    {
        if(playersCoins >= cost)
        {
            shop.BuyObject(cost, index, menuObject, pass);
        }
    }
}
