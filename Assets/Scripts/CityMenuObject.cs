using System;

[Serializable]
public class CityMenuObject : IMenuObject
{
    public int cityIndex, distanceTillBoss;
    public string cityName;
    public int bestDistance;
    public int bestCollect;
    public int bossesDefeated;

    public MenuObject GetObjectType()
    {
        return MenuObject.City;
    }

    public CityMenuObject(int Index, string Name, int dtb=250)
    {
        cityIndex = Index;
        cityName = Name;
        bestDistance = 0;
        bestCollect = 0;
        distanceTillBoss = dtb;
        bossesDefeated = 0;
    }
}

