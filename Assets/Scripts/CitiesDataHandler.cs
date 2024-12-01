using System;

[Serializable]
public class CitiesDataHandler 
{
    public CityMenuObject[] cities = new CityMenuObject[] { };
    public int pigeonIndex;

    public CitiesDataHandler()
    {
        pigeonIndex = 0;
        cities = new CityMenuObject[1] { new CityMenuObject(0, "FirstCity")};
    }
}
