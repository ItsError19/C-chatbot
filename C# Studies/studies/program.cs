using System;
using System.Collections.Generic;
using System.Media;  // Required for playing .wav files
using System.Threading;
using System.IO;

class Program
{
    static void Main()
    {
        PlayGreeting(); // Play voice greeting
        DisplayHeader();
        
        // USER INPUT SECTION
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeEffect("📝 What's your name? ");
        Console.ResetColor();
        string userName = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeEffect("🎯 What is your purpose? ");
        Console.ResetColor();
        string userPurpose = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeEffect("🔍 What can I ask you about? ");
        Console.ResetColor();
        string userTopic = Console.ReadLine();

        // DISPLAY SECURITY TOPICS SECTION
        Console.ForegroundColor = ConsoleColor.Yellow;
        TypeEffect("\n══════════════════════════════════════════════");
        TypeEffect("💡 You can ask me about:");
        TypeEffect("   1️⃣ Password Safety");
        TypeEffect("   2️⃣ Phishing");
        TypeEffect("   3️⃣ Safe Browsing");
        TypeEffect("══════════════════════════════════════════════\n");
        Console.ResetColor();

        // CHATBOT RESPONSE SECTION
        Console.ForegroundColor = ConsoleColor.Green;
        TypeEffect($"\n✅ [ChatBot]: Nice to meet you, {userName}! 🎉");
        TypeEffect($"   Your purpose is '{userPurpose}', and you're interested in '{userTopic}'.\n");
        Console.ResetColor();

        StartChat(userName);
    }

    static void DisplayHeader()
    {
        Console.Clear();
        string asciiArt = @"
   _____ _           _     _         
  / ____| |         | |   (_)        
 | |    | |__   __ _| |__  _ _ __    
 | |    | '_ \ / _` | '_ \| | '_ \   
 | |____| | | | (_| | | | | | | | |  
  \_____|_| |_|\__,_|_| |_|_|_| |_|  
                                      
            CMD ChatBot
--------------------------------";
        Console.WriteLine(asciiArt); // No typing effect here, just directly display the header
        Thread.Sleep(1500); // Pause for effect
    }

    static void PlayGreeting()
    {
        try
        {
            string filePath = "greeting.wav"; // Ensure the file exists in the same directory as the program
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.PlaySync(); // Play the sound synchronously before proceeding
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing greeting sound: " + ex.Message);
        }
    }

    static void StartChat(string userName)
    {
        TypeEffect("\n══════════════════════════════════════════════");
        TypeEffect($"[ChatBot]: Hello {userName}! Type 'exit' to end the chat.");
        TypeEffect("══════════════════════════════════════════════");

        Dictionary<string, string> responses = new Dictionary<string, string>
        {
            {"hello", $"Hi {userName}! How can I help you today?"},
            {"how are you", $"I'm just a bot, but I'm doing great! How about you, {userName}?"},
            {"what's your name", "I'm ChatBot, your friendly assistant!"},
            {"bye", $"Goodbye {userName}! Have a great day!"},
            {"what's your purpose", $"I'm here to provide helpful information and answer your questions, {userName}!"},
            {"what can i ask you about", $"You can ask me about general topics, password safety, phishing, and safe browsing, {userName}."}
        };

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n🧑 You: ");
            Console.ResetColor();
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                TypeEffect($"\n[ChatBot]: Chat ended. Bye {userName}!");
                break;
            }

            if (responses.ContainsKey(userInput))
            {
                TypeEffect($"[ChatBot]: {responses[userInput]}");
            }
            else if (userInput == "1")
            {
                TypeEffect("\n🔐 PASSWORD SAFETY");
                TypeEffect("--------------------------------");
                TypeEffect("Use unique, long passwords with a mix of characters.");
                TypeEffect("Consider using a password manager to keep track of them safely.");
            }
            else if (userInput == "2")
            {
                TypeEffect("\n🎣 PHISHING AWARENESS");
                TypeEffect("--------------------------------");
                TypeEffect("Be cautious with emails and messages asking for sensitive information.");
                TypeEffect("Check sender details and avoid clicking unknown links.");
            }
            else if (userInput == "3")
            {
                TypeEffect("\n🌍 SAFE BROWSING TIPS");
                TypeEffect("--------------------------------");
                TypeEffect("Use a secure browser, enable pop-up blockers, and avoid downloading files from untrusted sources.");
            }
            else
            {
                TypeEffect($"[ChatBot]: Sorry {userName}, I don't understand that.");
            }
        }
    }

    // Simulate typing effect
    static void TypeEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(50); // Delay for effect (you can adjust the speed here)
        }
        Console.WriteLine(); // Move to the next line after typing is complete
    }
}

