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


    void Start()
    {
        //Lancement de la requete
        StartCoroutine(GetRequest(uri));
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
                    Debug.Log(JsObj);
                    Debug.Log(JsObj["ethereum"]);
                    Debug.Log(JsObj["ethereum"]["btc"]);
                    Debug.Log(JsObj["ethereum"]["eur"]);
                    Debug.Log(JsObj["ethereum"]["usd"]);

                    //Assignement des valeur
                    btc = JsObj["ethereum"]["btc"];
                    eur = JsObj["ethereum"]["eur"];
                    usd = JsObj["ethereum"]["usd"];
                    
                    //Test du calcul de la valeur de mon NFT en euro
                    Debug.Log((0.08f*(float)JsObj["ethereum"]["eur"])/1f);

                }
            }

            yield return new WaitForSeconds(30f);
        }
    }
}
        
