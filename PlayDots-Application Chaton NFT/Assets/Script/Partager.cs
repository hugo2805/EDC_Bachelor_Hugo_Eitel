using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partager : MonoBehaviour
{
    private NFT nft;
    public NativeShare ObjShare = new NativeShare();

    private void Start()
    {
        nft = gameObject.GetComponent<NFT>();
    }

    public void partage()
    {
        ObjShare.AddFile("Assets/UI/Tuile/Chat1.png");
        ObjShare.Share();
    }

}
