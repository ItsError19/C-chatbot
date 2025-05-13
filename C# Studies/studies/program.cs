using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    private static Random _random = new Random();
    private static string _currentTopic = "";
    private static string _userName = "";

    static void Main()
    {
        PlayGreeting();
        DisplayHeader();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypeEffect("📝 What's your name? ");
        Console.ResetColor();
        _userName = Console.ReadLine();

        StartChat();
    }

    static void DisplayHeader()
    {
        Console.Clear();
        Console.WriteLine(@"
   _____ _           _     _         
  / ____| |         | |   (_)        
 | |    | |__   __ _| |__  _ _ __    
 | |    | '_ \ / _` | '_ \| | '_ \   
 | |____| | | | (_| | | | | | | | |  
  \_____|_| |_|\__,_|_| |_|_|_| |_|  
                                      
        Security ChatBot
--------------------------------");
        Thread.Sleep(1000);
    }

    static void PlayGreeting()
    {
        try
        {
            using (var player = new SoundPlayer("greeting.wav"))
            {
                player.PlaySync();
            }
        }
        catch { /* Ignore if sound file not found */ }
    }

    static void StartChat()
    {
        TypeEffect($"\n[ChatBot]: Welcome {_userName}! I'm your Cybersecurity Assistant.");
        ShowMainMenu();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\n🧑 You: ");
            Console.ResetColor();
            string userInput = Console.ReadLine()?.ToLower() ?? "";

            if (userInput == "exit")
            {
                TypeEffect($"[ChatBot]: Stay safe online, {_userName}! Goodbye!");
                break;
            }

            if (userInput.Contains("menu") || userInput.Contains("help"))
            {
                ShowMainMenu();
                _currentTopic = "";
                continue;
            }

            string response = ProcessUserInput(userInput);
            TypeEffect($"[ChatBot]: {response}");
        }
    }

    static void ShowMainMenu()
    {
        TypeEffect("\nYou can ask me about:");
        TypeEffect("- Passwords (creating, securing, managing)");
        TypeEffect("- Phishing (recognition, prevention)");
        TypeEffect("- Scams (identification, protection)");
        TypeEffect("- Privacy (online protection, settings)");
        TypeEffect("Or say 'menu' anytime to see these options again.");
    }

    static string ProcessUserInput(string userInput)
    {
        // Handle follow-up questions first
        if (!string.IsNullOrEmpty(_currentTopic))
        {
            if (ContainsAny(userInput, "more", "another", "else", "different"))
            {
                return GetTopicResponse(_currentTopic, true);
            }
            
            if (ContainsAny(userInput, "explain", "what do you mean", "clarify", "confused"))
            {
                return GetDetailedExplanation(_currentTopic);
            }
        }

        // Detect new topic
        if (ContainsAny(userInput, "password", "passwords", "credential", "login"))
        {
            _currentTopic = "password";
            return GetTopicResponse(_currentTopic, false);
        }
        else if (ContainsAny(userInput, "phish", "phishing"))
        {
            _currentTopic = "phishing";
            return GetTopicResponse(_currentTopic, false);
        }
        else if (ContainsAny(userInput, "scam", "fraud", "hoax"))
        {
            _currentTopic = "scam";
            return GetTopicResponse(_currentTopic, false);
        }
        else if (ContainsAny(userInput, "privacy", "private", "data protection", "tracking"))
        {
            _currentTopic = "privacy";
            return GetTopicResponse(_currentTopic, false);
        }

        // General responses
        string[] generalResponses = {
            $"I'm happy to discuss cybersecurity topics with you {_userName}. What specifically would you like to know?",
            $"I can help with password safety, phishing prevention, scam recognition, and privacy protection. What interests you {_userName}?",
            $"Let me know what cybersecurity topic you'd like to explore {_userName}. For example, you could ask about creating strong passwords."
        };
        _currentTopic = "";
        return generalResponses[_random.Next(generalResponses.Length)];
    }

    static string GetTopicResponse(string topic, bool isFollowUp)
    {
        var prefix = isFollowUp ? "Here's another tip" : "Great question";
        
        return topic switch
        {
            "password" => GetPasswordResponse(prefix),
            "phishing" => GetPhishingResponse(prefix),
            "scam" => GetScamResponse(prefix),
            "privacy" => GetPrivacyResponse(prefix),
            _ => "I'd be happy to discuss cybersecurity topics. What specifically interests you?"
        };
    }

    static string GetPasswordResponse(string prefix)
    {
        string[] responses = {
            $"{prefix} about passwords: Use long passphrases (like 'PurpleTiger$JumpsHigh') instead of simple passwords.",
            $"{prefix} about passwords: Never reuse passwords across different accounts - a breach in one service could compromise all.",
            $"{prefix} about passwords: Enable two-factor authentication wherever possible, even if you have strong passwords.",
            $"{prefix} about passwords: Consider using a password manager - it's like a vault for all your passwords."
        };
        return responses[_random.Next(responses.Length)];
    }

    static string GetPhishingResponse(string prefix)
    {
        string[] responses = {
            $"{prefix} about phishing: Check sender email addresses carefully - scammers often use addresses that look similar to real ones.",
            $"{prefix} about phishing: Hover over links before clicking to see the actual URL. If it looks suspicious, don't click!",
            $"{prefix} about phishing: Be cautious of emails creating urgency ('Your account will be closed!') - common phishing tactic.",
            $"{prefix} about phishing: Look for poor grammar and spelling - many phishing attempts originate from non-native speakers."
        };
        return responses[_random.Next(responses.Length)];
    }

    static string GetScamResponse(string prefix)
    {
        string[] responses = {
            $"{prefix} about scams: If an offer seems too good to be true, it probably is. Trust your instincts!",
            $"{prefix} about scams: Never share verification codes with anyone - legitimate companies will never ask for these.",
            $"{prefix} about scams: Be wary of urgent requests for money or information - scammers often create false emergencies."
        };
        return responses[_random.Next(responses.Length)];
    }

    static string GetPrivacyResponse(string prefix)
    {
        string[] responses = {
            $"{prefix} about privacy: Regularly review app permissions on your devices - many apps request more access than they need.",
            $"{prefix} about privacy: Use private browsing when you don't want history saved, but remember it doesn't make you anonymous.",
            $"{prefix} about privacy: Consider using a VPN when on public WiFi to encrypt your internet traffic.",
            $"{prefix} about privacy: Be mindful of what you post on social media - even 'private' accounts can be compromised."
        };
        return responses[_random.Next(responses.Length)];
    }

    static string GetDetailedExplanation(string topic)
    {
        return topic switch
        {
            "password" => "🔐 Password security is fundamental. Strong passwords should be long (12+ characters), " +
                          "unique for each account, and include a mix of character types. Password managers help " +
                          "generate and store these securely so you don't have to remember them all.",
            
            "phishing" => "🎣 Phishing is when attackers pretend to be trustworthy entities to steal sensitive information. " +
                          "They often use email, text messages, or fake websites. Always verify the sender's identity " +
                          "and never enter credentials unless you're certain of the website's authenticity.",
            
            "scam" => "🚨 Online scams come in many forms: fake tech support, romance scams, investment frauds. " +
                      "They typically create urgency or offer unrealistic rewards. Never send money or information " +
                      "to unverified parties, no matter how convincing they seem.",
            
            "privacy" => "🛡️ Online privacy means controlling what personal information you share and who can access it. " +
                         "This includes social media posts, app permissions, location data, and browsing history. " +
                         "Regularly check privacy settings on all your accounts and devices.",
            
            _ => "Cybersecurity is about protecting systems, networks, and data from digital attacks. " +
                 "Would you like me to explain a specific aspect like passwords, phishing, scams, or privacy?"
        };
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