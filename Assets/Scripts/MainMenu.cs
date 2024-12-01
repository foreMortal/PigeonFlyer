using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CoinsScriptableObject srptObj;
    [SerializeField] private string path;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Sprite[] cities, houses, pigeons;
    [SerializeField] private Text bestDistance, bestCollect;
    [SerializeField] private Image targetCity, targetHouse, targetPigeon;

    private List<CityMenuObject> citiesObjects = new();

    private CitiesDataHandler citiesData;
    private DataLoaderService dataLoaderService;
    private int cityIndex = 0, number;
    private int pigeonIndex = 0;

    private void Awake()
    {
        dataLoaderService = new DataLoaderService();

        dataLoaderService.GetDataLoaded(path, out citiesData);

        citiesData ??= new();

        citiesObjects.AddRange(citiesData.cities);

        cityIndex = citiesObjects[number].cityIndex;
        UpdateUI();
    }

    public void BuyNewObject(IMenuObject obj)
    {
        switch (obj.GetObjectType())
        {
            case MenuObject.City: AddNewCity((CityMenuObject)obj); break;
            case MenuObject.Pigeon: ChoosePigeon((PigeonMenuObject)obj); break;
        }
    }

    private void ChoosePigeon(PigeonMenuObject pigeon)
    {
        pigeonIndex = pigeon.index;
        citiesData.pigeonIndex = pigeonIndex;

        dataLoaderService.SaveData(path, citiesData);

        UpdateUI();
    }

    private void AddNewCity(CityMenuObject newCity)
    {
        citiesObjects.Add(newCity);

        citiesData.cities = citiesObjects.ToArray();

        UpdateButtons();

        dataLoaderService.SaveData(path, citiesData);
    }

    public void ChangeCity(int delta)
    {
        number += delta;
        cityIndex = citiesObjects[number].cityIndex;
        UpdateUI();
    }

    private void UpdateUI()
    {
        targetCity.sprite = cities[cityIndex];
        targetHouse.sprite = houses[cityIndex];
        targetPigeon.sprite = pigeons[pigeonIndex];

        bestDistance.text = citiesObjects[number].bestDistance.ToString() + " <color=red>m</color>";
        bestCollect.text = citiesObjects[number].bestCollect.ToString();

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (number < citiesObjects.Count - 1) buttons[1].SetActive(true);
        else buttons[1].SetActive(false);

        if (number > 0) buttons[0].SetActive(true);
        else buttons[0].SetActive(false);
    }

    public void StartLevel()
    {
        srptObj.levelNumber = number;
        srptObj.maxDistance = citiesObjects[number].bestDistance;
        srptObj.maxCoins = citiesObjects[number].bestCollect;
        srptObj.distanceTillBoss = citiesObjects[number].distanceTillBoss;

        SceneManager.LoadScene(citiesObjects[number].cityName);
    }
}
