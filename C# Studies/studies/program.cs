using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    static void Main()
    {
        PlayGreeting();
        DisplayHeader();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeEffect("📝 What's your name? ");
        Console.ResetColor();
        string userName = Console.ReadLine();

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
                                      
        Security ChatBot
--------------------------------";
        Console.WriteLine(asciiArt);
        Thread.Sleep(1000);
    }

    static void PlayGreeting()
    {
        try
        {
            using (SoundPlayer player = new SoundPlayer("greeting.wav"))
            {
                player.PlaySync();
            }
        }
        catch { /* Ignore if sound file not found */ }
    }

    static void StartChat(string userName)
    {
        TypeEffect($"\n[ChatBot]: Welcome {userName}! I'm your Cybersecurity Assistant.");
        TypeEffect("You can ask me about:");
        TypeEffect("- Passwords (e.g., 'How to create strong passwords?')");
        TypeEffect("- Scams (e.g., 'How to recognize online scams?')");
        TypeEffect("- Privacy (e.g., 'How to protect my privacy online?')");
        TypeEffect("Type 'exit' to end our chat.\n");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n🧑 You: ");
            Console.ResetColor();
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                TypeEffect($"[ChatBot]: Stay safe online, {userName}! Goodbye!");
                break;
            }

            string response = GetKeywordResponse(userInput);
            TypeEffect($"[ChatBot]: {response}");
        }
    }

    static string GetKeywordResponse(string userInput)
    {
        string input = userInput.ToLower();
        
        if (ContainsAny(input, "password", "passwords", "credential", "login"))
        {
            return "🔐 Password Safety Tip:\n" +
                   "Use strong, unique passwords for each account (minimum 12 characters with mix of letters, numbers and symbols).\n" +
                   "Consider using a password manager to generate and store complex passwords securely.";
        }
        else if (ContainsAny(input, "scam", "fraud", "phishing", "hoax"))
        {
            return "🚨 Scam Alert:\n" +
                   "Be cautious of unsolicited messages asking for personal information or money.\n" +
                   "Verify sender identities and never click suspicious links in emails or texts.";
        }
        else if (ContainsAny(input, "privacy", "private", "data protection", "tracking"))
        {
            return "🛡️ Privacy Protection:\n" +
                   "Regularly review privacy settings on your accounts and devices.\n" +
                   "Use VPNs on public networks and be mindful of what personal information you share online.";
        }
        else
        {
            return "I specialize in cybersecurity topics. Try asking about:\n" +
                   "- Creating strong passwords\n" +
                   "- Recognizing online scams\n" +
                   "- Protecting your privacy online";
        }
    }

    static bool ContainsAny(string input, params string[] keywords)
    {
        foreach (string keyword in keywords)
        {
            if (input.Contains(keyword))
                return true;
        }
        return false;
    }

    static void TypeEffect(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(20);
        }
        Console.WriteLine();
    }
}