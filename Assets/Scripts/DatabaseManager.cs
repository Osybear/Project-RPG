using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using BCrypt.Net;

public class DatabaseManager : MonoBehaviour
{

    //make sure to move this script to the server
    //have the client ask the server for stuff yea pepega
    public bool CheckDatabase(string username, string password){

        string connectionString = "host=192.168.1.203;port=3306;user=game;password=15wXsQK3gLi9IhhBO44b;database=game";
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            Debug.Log("Connecting to MySQL...");
            connection.Open();

            
            string sql = "SELECT `password` FROM `users` WHERE `username` = '" + username + "';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string hashed = reader[0].ToString();
            bool verified = BCrypt.Net.BCrypt.Verify(password, hashed, false, BCrypt.Net.HashType.SHA384);

            if(verified)
                return true;
            reader.Close();
            
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return false;
        }

        connection.Close();
        Debug.Log("Connection Closed");
        return false;
    }
}
