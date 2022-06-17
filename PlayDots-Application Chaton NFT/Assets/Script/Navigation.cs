using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Navigation : MonoBehaviour
{
 private NFT nft;
 public GameObject chat;
 public GameObject chatonNP;
 public GameObject chatonP;
 
 private void Start()
 {
     nft = gameObject.GetComponent<NFT>();
 }
 
 public void OpenChatonNP()
 {
   chatonNP.SetActive(true);
   chatonNP.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = nft.image;
   chatonNP.transform.GetChild(5).gameObject.GetComponent<Text>().text = nft.nom;
   chatonNP.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = nft.proprio;
   chatonNP.transform.GetChild(2).gameObject.transform.GetChild(2).GetComponent<Text>().text = nft.auteur;
   chatonNP.transform.GetChild(6).gameObject.transform.GetChild(0).GetComponent<Text>().text = nft.description;
 }
 
 public void OpenChatonP()
 {
     chatonP.SetActive(true);
     chatonP.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<Text>().text = nft.auteur;
     chatonP.transform.GetChild(5).gameObject.GetComponent<Text>().text = nft.nom;
     chatonP.transform.GetChild(6).gameObject.GetComponent<Text>().text =
         "Ce NFT vaut " + nft.pEth + " ETH, soit " + nft.pBtc + " BTC, soit " + nft.pEur + " €, soit "+nft.pUsd+" $";
     //Debug.Log(nft.id);
     chat.GetComponent<chat3D>().SetUp3DCat(nft.id);
 }


}
