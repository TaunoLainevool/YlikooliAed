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
    public List<Questions> questionList = new List<Questions>(); //TODO list for DB
    public static DBconnection Instance { get; private set; }
    


    void Awake()
    {
         if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
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


                        Questions q = new Questions();

                        // Assuming your table columns correspond to the class fields in order or by name:
                        // Adjust the column indexes or names according to your actual table schema

                        q.id = reader.GetInt32(reader.GetOrdinal("id"));
                        q.title = reader.GetString(reader.GetOrdinal("title"));
                        q.question = reader.GetString(reader.GetOrdinal("question"));
                        q.option_a = reader.GetString(reader.GetOrdinal("option_a"));
                        q.option_b = reader.GetString(reader.GetOrdinal("option_b"));
                        q.option_c = reader.GetString(reader.GetOrdinal("option_c"));
                        q.option_d = reader.GetString(reader.GetOrdinal("option_d"));

                        // For the correct_answer, assuming it's a single character stored as string or char in DB
                        string correctAnswerStr = reader.GetString(reader.GetOrdinal("correct_answer"));
                        if (!string.IsNullOrEmpty(correctAnswerStr))
                            q.correct_answer = correctAnswerStr[0];
                        else
                            q.correct_answer = ' '; // or some default char

                        questionList.Add(q);

                        // Debug.Log($"ID: {q.id}");
                        // Debug.Log($"Title: {q.title}");
                        // Debug.Log($"Question: {q.question}");
                        // Debug.Log($"Option A: {q.option_a}");
                        // Debug.Log($"Option B: {q.option_b}");
                        // Debug.Log($"Option C: {q.option_c}");
                        // Debug.Log($"Option D: {q.option_d}");
                        // Debug.Log($"Correct Answer: {q.correct_answer}");

                        //Debug.Log(reader[2].ToString()); // Example: print first column


                    }
                }

                AssignQuestionsToDialogue();
                conn.Close();
            }

            forwardedPort.Stop();
            sshClient.Disconnect();

        }

        // foreach (var question in questionList)
        //     {
        //         Debug.Log(question.question);
        //     }
    }

    public DBdialogue npcDialogue;

    void AssignQuestionsToDialogue()
    {
        if (npcDialogue == null)
        {
            Debug.LogError("npcDialogue reference is null!");
            return;
        }

        npcDialogue.dialogueLines = questionList.Select(q => q.question).ToArray();
        npcDialogue.gameTitle = questionList.Select(q => q.title).ToArray();
        // npcDialogue.choices = questionList.Select(q => q.option_a).ToArray();

        Debug.Log($"Assigned {npcDialogue.dialogueLines.Length} dialogue lines from questions.");
    

        var dialogueChoicesList = new List<DialogueChoice>();

        for (int i = 0; i < questionList.Count; i++)
        {
            var q = questionList[i];
            DialogueChoice choice = new DialogueChoice();

            choice.dialogueIndex = i; // or whichever dialogue line index this corresponds to
            choice.choices = new string[] { q.option_a, q.option_b, q.option_c, q.option_d };
            choice.nextDialogueIndexes = new int[] { i + 1, i + 1, i + 1, i + 1 }; // example: all lead to next line
            choice.isPointable = true; // or false depending on your logic

            // Assuming correctAnswers marks which option is correct, e.g. option_a is correct if q.correct_answer == 'a'
            choice.correctAnswers = new bool[4];
            choice.correctAnswers[0] = (q.correct_answer == 'A');
            choice.correctAnswers[1] = (q.correct_answer == 'B');
            choice.correctAnswers[2] = (q.correct_answer == 'D');
            choice.correctAnswers[3] = (q.correct_answer == 'D');

            dialogueChoicesList.Add(choice);
        }

        npcDialogue.choices = dialogueChoicesList.ToArray();
}



}
