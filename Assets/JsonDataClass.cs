using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Xml.Serialization;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System;
[Serializable]
public class JsonDataClass
{
    public string status;
    public List<Result> result;

}
[Serializable]
public class Result
{
    public double mag;
    public double lng;
    public double lat;
    public string lokasyon;
    public double depth;
    public string coordinates;
    public string title;
    public string rev;
    public string timestamp;
    public string date_stamp;
    public string date;
}
