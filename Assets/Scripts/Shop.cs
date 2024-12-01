using UnityEngine;
using UnityEngine.UI;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private ObjectInShop[] objects;
    [SerializeField] private Text coinsText;

    private bool[] objectsInShop = new bool[6];

    private int choosenPigeon;
    private int coins = 300;
    private ShopDataHandler dataHandler;

    public int ChoosenPigeon { get { return choosenPigeon; } }

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            dataHandler = YandexGame.savesData.shopDataHandler;

        dataHandler ??= new ShopDataHandler();

        choosenPigeon = dataHandler.choosenPigeon;
        coins = dataHandler.coins;
        objectsInShop = dataHandler.boughtObjects;

        coinsText.text = coins.ToString();

        objectsInShop[0] = true;
        objectsInShop[3] = true;

        foreach (var o in objects)
        {
            o.UpdateUI(objectsInShop[o.Index], coins);
        }
    }

    public void BuyObject(int cost, int index, IMenuObject menuObject, bool pass=true)
    {
        coins -= cost;
        objectsInShop[index] = true;
        foreach(var o in objects)
        {
            o.UpdateUI(objectsInShop[o.Index], coins);
        }
        coinsText.text = coins.ToString();

        if(pass) mainMenu.BuyNewObject(menuObject);

        dataHandler.choosenPigeon = choosenPigeon;
        dataHandler.coins = coins;
        dataHandler.boughtObjects = objectsInShop;

        YandexGame.savesData.shopDataHandler = dataHandler;
        YandexGame.SaveProgress();
    }

    public void ChoosePigeon(int index)
    {
        choosenPigeon = index;

        foreach (var o in objects)
        {
            o.UpdateUI(objectsInShop[o.Index], coins);
        }
    }
}
