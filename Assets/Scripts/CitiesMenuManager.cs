using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CitiesMenuManager : MonoBehaviour
{
    [SerializeField] private Sprite[] cities, houses, pigeons;
    [SerializeField] private Text bestDistance, bestCollect;
    [SerializeField] private Image targetCity, targetHouse, targetPigeon;

    private List<CityMenuObject> citiesObjects = new();

    private int cityIndex = 0;

    public void AddNewCityMenu(CityMenuObject newCity)
    {
        citiesObjects.Add(newCity);
    }



    public void StartLevel()
    {
        SceneManager.LoadScene(citiesObjects[cityIndex].cityName);
    }
}
