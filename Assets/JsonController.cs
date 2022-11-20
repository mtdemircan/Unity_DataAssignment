using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System.Xml.Serialization;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Npgsql;
using UnityEngine.UI;

public class JsonController : MonoBehaviour
{
    public string jsonURL;
    public GameObject buttonPrefab;
    public GameObject parent;
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        WWW _www = new WWW(jsonURL);

        yield return _www;
        if (_www.error == null)
        {
            while (true)
            {
                
                processJsonData(_www.text);
                yield return new WaitForSeconds(10);
            }
            
        }
        else
        {
            Debug.Log("err");
        }
    }

    private void processJsonData(string _url)
    {
        JsonDataClass jsnData = JsonUtility.FromJson<JsonDataClass>(_url);
        NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;UserId=postgres;Password=tahamaha;Database=UnityDB;");
        conn.Open();
        NpgsqlCommand command = conn.CreateCommand();
        string query = "DELETE FROM \"UnityTable\"";
        command.CommandText = query;
        command.ExecuteNonQuery();
        GameObject tmp;
        int y = 1000;
        
        foreach (Result r in jsnData.result)
        {

            tmp=Instantiate(buttonPrefab);
            tmp.transform.SetParent(parent.transform, true);
            tmp.transform.position = new Vector2(tmp.transform.position.x-20,y);
            Text ButtonText = tmp.GetComponentInChildren<Text>();
            ButtonText.text = "Lokasyon: "+r.lokasyon+" Şiddet: "+r.mag+" Tarih: "+r.date;
            y -= 100;
            query = "INSERT INTO \"UnityTable\" VALUES ('"+r.lokasyon+"',"+r.mag+",'"+r.date+"')";
            command.CommandText = query;
            command.ExecuteNonQuery();
            Destroy(tmp, 10f); 
            
        }
        conn.Close();

    }
}
