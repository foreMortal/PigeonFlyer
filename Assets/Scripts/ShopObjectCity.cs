using UnityEngine;

public class ShopObjectCity : ObjectInShop
{
    [Space]
    [SerializeField] private int cityIndex;
    [SerializeField] private string cityName;

    private void Awake()
    {
        menuObject = new CityMenuObject(cityIndex, cityName);
    }
}
