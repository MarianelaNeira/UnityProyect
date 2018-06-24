using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DataBase : MonoBehaviour
{
    private static IDbConnection connection;
    private static IDbCommand commnand;
    private static IDataReader reader;
    private static string query;

    public static void OpenDB()
    {
        connection = (IDbConnection)new SqliteConnection(PlayerPrefs.GetString("StringConn"));
        connection.Open();
    }

    public static ICollection<User> GetUsers()
    {
        OpenDB();
        commnand = connection.CreateCommand();
        query = string.Format("SELECT * FROM User");
        commnand.CommandText = query;
        reader = commnand.ExecuteReader();

        ICollection<User> users = new List<User>();

        while (reader.Read())
        {
            User user = new User()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Pass = reader.GetString(3),
                Score = reader.GetInt32(4)
            };
            users.Add(user);
        }
        reader.Close();
        reader = null;
        commnand.Dispose();
        commnand = null;
        connection.Close();
        connection = null;

        return users;
    }

    public static User GetUser(string email, string pass)
    {
        OpenDB();
        commnand = connection.CreateCommand();
        query = string.Format("SELECT * FROM User WHERE Email = \"{0}\" AND Pass = \"{1}\"", email, pass);
        commnand.CommandText = query;
        reader = commnand.ExecuteReader();
        User user = new User() { };
        while (reader.Read())
        {
            user = new User()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Pass = reader.GetString(3),
                Score = reader.GetInt32(4)
            };
        }
        reader.Close();
        reader = null;
        commnand.Dispose();
        commnand = null;
        connection.Close();
        connection = null;
        return user;
    }

    public static void InsertScore(string email, int score)
    {
        OpenDB();
        query = string.Format("UPDATE User set Score=\"{0}\" WHERE Email =\"{1}\" ", score, email);
        commnand = connection.CreateCommand();
        commnand.CommandText = query;
        commnand.ExecuteReader();
        commnand.Dispose();
        commnand = null;
        connection.Close();
        connection = null;
    }


    public static void InsertUser(string name, string email, string pass)
    {
        OpenDB();
        query = string.Format("insert into User (Name, Email, Pass, Score) values (\"{0}\",\"{1}\",\"{2}\",\"{3}\")", name, email, pass, 0);
        commnand = connection.CreateCommand();
        commnand.CommandText = query;
        commnand.ExecuteReader();
        commnand.Dispose();
        commnand = null;
        connection.Close();
        connection = null;

    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Score { get; set; }
    }
}
