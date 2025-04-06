using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordValidator
{
    private HashSet<string> validWords = new HashSet<string>();

    private static string DICTIONARY_FILE_NAME = "wordlist";

    public WordValidator()
    {
        LoadDictionary();
    }

    private void LoadDictionary()
    {
        var wordFile = Resources.Load<TextAsset>(DICTIONARY_FILE_NAME);
        string[] words = wordFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
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
