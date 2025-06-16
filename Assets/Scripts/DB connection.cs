using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renci.SshNet;
using System;
using System.Data.SqlClient;
using UnityNpgsql;
using System.IO;
using System.Linq;

public class DBconnection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // See https://aka.ms/new-console-template for more information


        var lines = File.ReadAllLines("C:\\Users\\carle\\Desktop\\UNI stuff\\SEM2\\devProj\\DB connection test\\credentials.txt");
        var dict = lines
            .Where(line => !string.IsNullOrWhiteSpace(line) && line.Contains('='))
            .Select(line => line.Split('=', 2))
            .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());



        string lin2User = dict["lin2User"];
        string lin2Pass = dict["lin2Pass"];
        string greenyUser = dict["greenyUser"];
        string greenyPass = dict["greenyPass"];



        // SSH gateway (jump host)
        string sshHost = "lin2.tlu.ee";
        string sshUser = lin2User;
        string sshPass = lin2Pass;

        // Target database host and port
        string dbHost = "greeny.cs.tlu.ee";
        int dbPort = 5432;

        // Local port for forwarding
        uint localPort = 5433;

        // PostgreSQL database credentials
        string dbUser = greenyUser;
        string dbPass = greenyPass;
        string dbName = "aed";

        using (var sshClient = new SshClient(sshHost, sshUser, sshPass))
        {
            sshClient.Connect();
            Console.WriteLine("SSH connected to " + sshHost);

            // Forward localPort on localhost to dbHost:dbPort through SSH
            var forwardedPort = new ForwardedPortLocal("127.0.0.1", localPort, dbHost, (uint)dbPort);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();
            Console.WriteLine($"Port forwarding started: localhost:{localPort} -> {dbHost}:{dbPort}");

            // Build connection string to PostgreSQL via the forwarded port
            var connString = $"Host=127.0.0.1;Port={localPort};Username={dbUser};Password={dbPass};Database={dbName}";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                Console.WriteLine("Connected to PostgreSQL database");

                using (var cmd = new NpgsqlCommand("SELECT * FROM quizzes;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader[2].ToString()); // Example: print first column
                    }
                }

                conn.Close();
            }

            forwardedPort.Stop();
            sshClient.Disconnect();
        }
    }
    

}
