using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFT : MonoBehaviour
{
    //variables
    public int id;
    public string nom;
    public Sprite image;
    public Sprite tuile;
    public string auteur;
    public string proprio;
    public string description;
    public float pEth;
    public float pBtc;
    public float pEur;
    public float pUsd;
    
    //Objets
    public GameObject Nft;
    public Text txtNom;
    public Text txtAuteur;
    public Text txtValeur;
    

    public GameObject sqlite;
    
    
    

    public List<Sprite> nftImg = new List<Sprite>();
    public List<Sprite> tuileImg = new List<Sprite>();

    private void Start()
    {
        image = nftImg[id-1];
        tuile = tuileImg[id-1];

        Nft.GetComponent<Image>().sprite = tuile;
        txtNom.text = nom;
        txtAuteur.text = auteur;
        txtValeur.text = pEth+" ETH";


    }

    public void SetIdAchat()
    {
        sqlite.GetComponent<Sqlite>().idAchat = id;
    }
    public void SetIdAchatZ()
    {
        sqlite.GetComponent<Sqlite>().idAchat = -1;
    }
}
