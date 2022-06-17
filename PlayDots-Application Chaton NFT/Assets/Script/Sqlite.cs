using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Sqlite : MonoBehaviour
{
    public GameObject coinGeko;
    public InputField ChampPseudo;
    public InputField ChampPassword;
    private string pseudo;
    private string password;
    private IDbConnection dbcon;
    public Text txt;
    public bool log;
    public GameObject feedback;
    public GameObject Galerie;
    public GameObject Marche;
    public GameObject EcranConn;
    public List<string> nftInit = new List<string>();
    public List<string> nftRequest = new List<string>();
    public List<float> nftPrice = new List<float>();
    public List<GameObject> TuileGalerie = new List<GameObject>();
    public List<GameObject> TuileMarche = new List<GameObject>();
    public int id;
    public int idAchat = -1;

    public void Init()
    {
        // Création de la base
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Création de la table NFT
        IDbCommand dbcmd1;
        dbcmd1 = dbcon.CreateCommand();
        string q_createTable1 =
            "DROP TABLE IF EXISTS nft; "+
            "CREATE TABLE IF NOT EXISTS nft ("+
            "id int(11) NOT NULL,"+
            "nom text NOT NULL,"+
            "auteur text NOT NULL,"+
            "description text NOT NULL,"+
            "owner int(11) NOT NULL,"+
            "petc float NOT NULL,"+
            "pbtc float NOT NULL,"+
            "peur float NOT NULL,"+
            "pusd float NOT NULL)";
		
        dbcmd1.CommandText = q_createTable1;
        dbcmd1.ExecuteReader();
        
        // Création de la table User
        IDbCommand dbcmd2;
        dbcmd2 = dbcon.CreateCommand();
        string q_createTable2 =
            "CREATE TABLE IF NOT EXISTS TUser (" +
            "id int(11) NOT NULL," +
            "pseudo text NOT NULL," +
            "mdp text NOT NULL," +
            "PRIMARY KEY (id))";
        
        dbcmd2.CommandText = q_createTable2;
        dbcmd2.ExecuteReader();

        // Insérer des valeurs
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT OR IGNORE INTO TUser (id,pseudo,mdp) VALUES(1,'testeur1','test')";
        cmnd.ExecuteNonQuery();
        IDbCommand cmnd1 = dbcon.CreateCommand();
        cmnd1.CommandText = "INSERT OR IGNORE INTO TUser (id,pseudo,mdp) VALUES(2,'testeur2','test')";
        cmnd1.ExecuteNonQuery();
        
        for(int i=0; i<8;i++)
        {
            IDbCommand cmnd2 = dbcon.CreateCommand();
            cmnd2.CommandText = nftRequest[i];
            cmnd2.ExecuteNonQuery();
            
        }
        
        // Lire et afficher l'ensemble des valeurs de la table
        /*IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query ="SELECT * FROM Tuser";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            //Debug.Log("id: " + reader[0].ToString());
            //Debug.Log("pseudo: " + reader[1].ToString());
            //Debug.Log("password: " + reader[2].ToString());
        }*/
    }
    
    //Fonction appelée par le bouton de connexion
    public void btnConn()
    {
        pseudo = ChampPseudo.text;
        password = ChampPassword.text;
        VerifConnexion(pseudo, password);
    }


    //Fonction de vérification des identifiants
    private void VerifConnexion(string pseudo1, string password1)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query ="SELECT * FROM TUser WHERE pseudo ='"+pseudo1+"' AND mdp = '"+password1+"'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        
        while (reader.Read())
        {
            id = int.Parse(reader[0].ToString());
            //Debug.Log("id: " + reader[0].ToString());
        }
        
        if (id!= null)
        {
            InitMarche();
            initGalerie(id);
            feedback.SetActive(false);
            Galerie.SetActive(true);
            EcranConn.SetActive(false);
            Marche.SetActive(true);
            ChampPseudo.text = "";
            ChampPassword.text = "";
            
        }
        else
        {
            feedback.SetActive(true);
            ChampPseudo.text = "";
            ChampPassword.text = "";
        }
        
    }

    public void InitNftTable()
    {
        for(int i=0; i<8;i++)
        {
            /*Le vector3 contient les valeurs des autres devises
             * devises.x = btc
             * devises.y = eur
             * devises.z = usd
             */
            Vector3 devises = coinGeko.GetComponent<CoinGeko>().Calculprix(nftPrice[i]);
            string request = "INSERT OR IGNORE INTO nft VALUES" + nftInit[i] +","+ CTP(nftPrice[i]) +","+ CTP(devises.x) +","+ CTP(devises.y) +","+ CTP(devises.z)+")";
            nftRequest.Add(request);
        }
        //Debug.Log("-- FIN DE SQLITE--");
        Init();
    }
    
    string CTP(float f)
    {
        string s = f.ToString();
        s = s.Replace(',', '.');
        return s;
    }

    public void initGalerie(int id)
    {
        Debug.Log("Galerie");

        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query ="SELECT * FROM nft WHERE owner ='"+id+"'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            TuileGalerie[i].gameObject.SetActive(true);
            TuileGalerie[i].gameObject.GetComponent<NFT>().id = int.Parse(reader[0].ToString());
            TuileGalerie[i].gameObject.GetComponent<NFT>().nom = reader[1].ToString();
            TuileGalerie[i].gameObject.GetComponent<NFT>().auteur = reader[2].ToString();
            TuileGalerie[i].gameObject.GetComponent<NFT>().description = reader[3].ToString();
            if (int.Parse(reader[4].ToString()) == 1)
            {
                TuileGalerie[i].gameObject.GetComponent<NFT>().proprio = "testeur1";
            }
            TuileGalerie[i].gameObject.GetComponent<NFT>().pEth = float.Parse(reader[5].ToString());
            TuileGalerie[i].gameObject.GetComponent<NFT>().pBtc = float.Parse(reader[6].ToString());
            TuileGalerie[i].gameObject.GetComponent<NFT>().pEur = float.Parse(reader[7].ToString());
            TuileGalerie[i].gameObject.GetComponent<NFT>().pUsd = float.Parse(reader[8].ToString());
            //TuileGalerie.Remove(TuileGalerie[0].gameObject);
            i++;
        }
    }

    public void InitMarche()
    {
        Debug.Log("Marche");
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query ="SELECT * FROM nft";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            TuileMarche[i].gameObject.SetActive(true);
            TuileMarche[i].gameObject.GetComponent<NFT>().id = int.Parse(reader[0].ToString());
            TuileMarche[i].gameObject.GetComponent<NFT>().nom = reader[1].ToString();
            TuileMarche[i].gameObject.GetComponent<NFT>().auteur = reader[2].ToString();
            TuileMarche[i].gameObject.GetComponent<NFT>().description = reader[3].ToString();
            if (int.Parse(reader[4].ToString()) == 1)
            {
                TuileMarche[i].gameObject.GetComponent<NFT>().proprio = "testeur1";
            }
            TuileMarche[i].gameObject.GetComponent<NFT>().pEth = float.Parse(reader[5].ToString());
            TuileMarche[i].gameObject.GetComponent<NFT>().pBtc = float.Parse(reader[6].ToString());
            TuileMarche[i].gameObject.GetComponent<NFT>().pEur = float.Parse(reader[7].ToString());
            TuileMarche[i].gameObject.GetComponent<NFT>().pUsd = float.Parse(reader[8].ToString());
            //TuileMarche.Remove(TuileMarche[0].gameObject);
            i++;
        }
    }

    private void Achat(int idAchat)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_update = dbcon.CreateCommand();
        string query = "UPDATE nft SET owner = 1 WHERE id = " + id;
        cmnd_update.CommandText = query;
        cmnd_update.ExecuteNonQuery();
        
        InitMarche();
        initGalerie(1);
        
    }

    public void Acheter()
    {
        Achat(idAchat);
    }
    
    private void Vente(int idAchat)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_update = dbcon.CreateCommand();
        string query = "UPDATE nft SET owner = 0 WHERE id = " + id;
        cmnd_update.CommandText = query;
        cmnd_update.ExecuteNonQuery();
        
        InitMarche();
        initGalerie(1);
       
    }

    public void Vendre()
    {
        Vente(idAchat);
    }
}

/*Fonction ralatives à la BDD
 * Initialisation (générer les vues à la volée en fonction des donées)
 * Sauver les valeurs des devises
 * Achat d'un NFT
 * Vente d'un NFT
 * Gérer les données affichée en fonction de l'utilisateur
 *
 *
 *
 * Autres choses
 * Partager une image
 * http://www.daniel4d.com/blog/sharing-image-unity-android/
 * 
 

"1,'Sacha','Hugo Eitel','C est une representation du chat de l auteur',0.08,1"

"2,'Fripouille',Hugo Eitel','Un chat quelconque',0.05,0"
    
"3,'Mistigri','Hugo Eitel','Un chat gris tigré aux yeux vert',1,0"
    
"4,'Félix','Hugo Eitel','Un chat roux au venre blanc',1.2,1"
    
"5,'Garfield','Hugo Eitel','Il adore les lasagnes et deteste le lundi',2,1"
    
"6,'CatWood','Hugo Eitel','Ce chat en bois fais reference à un personnage de la serie audio clyde vanilla',0.06,0"
    
"7,'Metallichat','Hugo Eitel,'Ce chat fait reference au celebre Metallica',0.5,0"
    
"8,'IronCat','Hugo Eitel','Ce chat fait reference au celebre super hero Iron Man',0.9,1"

*/