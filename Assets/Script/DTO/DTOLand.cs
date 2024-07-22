using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTOLand
{
    private string _name;
    private string _mapId;
    private double _area;
    private double _longitude;
    private double _latitude;

    public DTOLand(string name, string mapId, double area, double longitude, double latitude)
    {
        _name = name;
        _mapId = mapId;
        _area = area;
        _longitude = longitude;
        _latitude = latitude;
    }

    public void ParseJson(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }

    public string MapId => _mapId;
    public double Area => _area;
    public double Longitude => _longitude;
    public double Latitude => _latitude;
    public string Name => _name;
}
