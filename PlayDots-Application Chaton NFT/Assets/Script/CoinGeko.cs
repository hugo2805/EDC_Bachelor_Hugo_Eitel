using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;
using SimpleJSON;

public class CoinGeko : MonoBehaviour
{

    //déclaration de l'url pour la requete GET
    private string uri = "https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies=btc%2Ceur%2Cusd";
    public Text txt;
    public UnityWebRequest www;
    public string result;
    public float btc;
    public float eur;
    public float usd;
    public Vector3 Prix;
    public GameObject Sqlite;
    private bool init;


    void Awake()
    {
        //Lancement de la requete
        StartCoroutine(GetRequest(uri));
        init = false;
    }
    
    IEnumerator GetRequest(string uri)
    {
        for (;;)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                
                //vérification en cas d'erreur
                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                    Debug.Log("Error: " + webRequest.error);
                else
                {
                    //debug de la réponse
                    txt.text = webRequest.downloadHandler.text;
                    //récupération et parse du JSON de réponse
                    result = webRequest.downloadHandler.text;
                    JSONObject JsObj = (JSONObject) JSON.Parse(result);
                    
                    //Test de récupération des valeur
                    /*Debug.Log(JsObj);
                    Debug.Log(JsObj["ethereum"]);
                    Debug.Log(JsObj["ethereum"]["btc"]);
                    Debug.Log(JsObj["ethereum"]["eur"]);
                    Debug.Log(JsObj["ethereum"]["usd"]);
                    Debug.Log("--FIN DE COIN GEKO--");*/

                    //Assignement des valeur
                    btc = JsObj["ethereum"]["btc"];
                    eur = JsObj["ethereum"]["eur"];
                    usd = JsObj["ethereum"]["usd"];

                    if (!init)
                    {
                        Sqlite.GetComponent<Sqlite>().InitNftTable();
                        init = true;
                    }
                    
                    
                    //Test du calcul de la valeur de mon NFT en euro
                    //Debug.Log((0.08f*(float)JsObj["ethereum"]["eur"])/1f);

                }
            }

            yield return new WaitForSeconds(30f);
        }
    }

    //Fonction de calcul du prix du NFT dans les différentes devises
    public Vector3 Calculprix(float valeur)
    {
        if (btc != null && eur != null && usd != null)
        {
            Prix.x = (valeur * btc) / 1f;
            Prix.y = (valeur * eur) / 1f;
            Prix.z = (valeur * usd) / 1f;
        }
        return Prix;
    }
}
        
