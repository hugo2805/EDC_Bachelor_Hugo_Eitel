using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SqliteTest : MonoBehaviour
{
    public Text txt;
    void Start()
    {
        Init();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        // Création de la base
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "ChatonNFT_Base";
		
        // Connexion avec la base
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Création de la table NFT
        IDbCommand dbcmd1;
        dbcmd1 = dbcon.CreateCommand();
        string q_createTable1 =
            "CREATE TABLE IF NOT EXISTS nft ("+
            "id int(11) NOT NULL,"+
            "nom text NOT NULL,"+
            "description text NOT NULL,"+
            "petc float NOT NULL,"+
            "owner int(11) NOT NULL"+
            "pbtc float NOT NULL,"+
            "peur float NOT NULL,"+
            "pusd float NOT NULL"+
            ") ENGINE=InnoDB DEFAULT CHARSET = utf8;";
		
        dbcmd1.CommandText = q_createTable1;
        dbcmd1.ExecuteReader();
        
        // Création de la table User
        IDbCommand dbcmd2;
        dbcmd2 = dbcon.CreateCommand();
        string q_createTable2 =
            "CREATE TABLE IF NOT EXISTS user (" +
            "id int(11) NOT NULL AUTO_INCREMENT," +
            "pseudo text NOT NULL," +
            "mdp text NOT NULL," +
            "PRIMARY KEY (id)" +
            ") ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET = utf8;";
        
        dbcmd2.CommandText = q_createTable2;
        dbcmd2.ExecuteReader();

        // Insérer des valeurs
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT OR IGNORE INTO user (id, pseudo, mdp) VALUES(1, 'testeur1', 'test'),(2, 'testeur2', 'test');";
        cmnd.ExecuteNonQuery();
        
        // Lire et afficher l'ensemble des valeurs de la table
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query ="SELECT * FROM ChatonNFT_Base";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
            txt.text += "id: " + reader[0].ToString() + " val: " + reader[1].ToString() + " et ";
        }

        // Déconnexion de la base
        dbcon.Close();

    }
    

}
