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
    private DataLoaderService loader;
    private ShopDataHandler dataHandler;

    public int ChoosenPigeon { get { return choosenPigeon; } }

    private void Awake()
    {
        loader = new();

        loader.GetDataLoaded(path, out dataHandler);

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

        loader.SaveData(path, dataHandler);
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
