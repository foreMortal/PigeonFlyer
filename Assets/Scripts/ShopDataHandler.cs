public class ShopDataHandler
{
    public bool[] boughtObjects;
    public int coins, choosenPigeon;

    public ShopDataHandler()
    {
        choosenPigeon = 0;
        coins = 0;
        boughtObjects = new bool[6];
    }
}
