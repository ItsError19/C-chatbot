using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    private static Random _random = new Random();

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
        TypeEffect("- Phishing (e.g., 'Give me phishing tips')");
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
            string[] passwordTips = {
                "🔐 Password Tip 1: Use long passphrases (like 'PurpleTiger$JumpsHigh') instead of simple passwords.",
                "🔐 Password Tip 2: Never reuse passwords across different accounts - a breach in one service could compromise all your accounts.",
                "🔐 Password Tip 3: Enable two-factor authentication wherever possible, even if you have strong passwords.",
                "🔐 Password Tip 4: Consider using a password manager - it's like a vault for all your passwords and can generate strong ones for you."
            };
            return passwordTips[_random.Next(passwordTips.Length)];
        }
        else if (ContainsAny(input, "scam", "fraud", "hoax"))
        {
            string[] scamTips = {
                "🚨 Scam Alert 1: If an offer seems too good to be true, it probably is. Trust your instincts!",
                "🚨 Scam Alert 2: Never share verification codes with anyone - legitimate companies will never ask for these.",
                "🚨 Scam Alert 3: Be wary of urgent requests for money or information - scammers often create false emergencies."
            };
            return scamTips[_random.Next(scamTips.Length)];
        }
        else if (ContainsAny(input, "phish", "phishing"))
        {
            string[] phishingTips = {
                "🎣 Phishing Tip 1: Check sender email addresses carefully - scammers often use addresses that look similar to real ones.",
                "🎣 Phishing Tip 2: Hover over links before clicking to see the actual URL. If it looks suspicious, don't click!",
                "🎣 Phishing Tip 3: Be cautious of emails creating urgency ('Your account will be closed!') - this is a common phishing tactic.",
                "🎣 Phishing Tip 4: Look for poor grammar and spelling - many phishing attempts originate from non-native speakers."
            };
            return phishingTips[_random.Next(phishingTips.Length)];
        }
        else if (ContainsAny(input, "privacy", "private", "data protection", "tracking"))
        {
            string[] privacyTips = {
                "🛡️ Privacy Tip 1: Regularly review app permissions on your devices - many apps request more access than they need.",
                "🛡️ Privacy Tip 2: Use private/incognito browsing when you don't want your history saved, but remember it doesn't make you anonymous.",
                "🛡️ Privacy Tip 3: Consider using a VPN when on public WiFi to encrypt your internet traffic.",
                "🛡️ Privacy Tip 4: Be mindful of what you post on social media - even 'private' accounts can be compromised."
            };
            return privacyTips[_random.Next(privacyTips.Length)];
        }
        else
        {
            string[] generalResponses = {
                "I specialize in cybersecurity topics. Try asking about password safety, scam prevention, or online privacy.",
                "For cybersecurity advice, you can ask me about: creating strong passwords, recognizing scams, or protecting your privacy.",
                "I'd be happy to help with cybersecurity questions about passwords, phishing, scams, or privacy protection."
            };
            return generalResponses[_random.Next(generalResponses.Length)];
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