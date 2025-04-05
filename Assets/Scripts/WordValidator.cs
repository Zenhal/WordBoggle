using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordValidator
{
    private HashSet<string> validWords = new HashSet<string>();

    private static string DICTIONARY_FILE_PATH = "Assets/Resources/wordlist.txt";

    public WordValidator()
    {
        LoadDictionary();
    }

    private void LoadDictionary()
    {
        string[] words = File.ReadAllLines(DICTIONARY_FILE_PATH);
        foreach (string word in words)
        {
            validWords.Add(word.ToUpper());
        }
        Debug.Log(validWords.Count);
    }

    public bool IsValidWord(string word)
    {
        return validWords.Contains(word.ToUpper());
    }
}
