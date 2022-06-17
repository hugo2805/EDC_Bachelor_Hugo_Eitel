using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFT : MonoBehaviour
{
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

    public List<Sprite> nftImg = new List<Sprite>();
    public List<Sprite> tuileImg = new List<Sprite>();

    private void Start()
    {
        image = nftImg[id];
        tuile = tuileImg[id];
    }
}
