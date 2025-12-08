using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Assignment8
{
    /// <summary>
    /// Core spell checker class that uses HashSet<string> for efficient word lookups and text analysis.
    /// This class demonstrates key HashSet concepts including fast Contains() operations,
    /// automatic uniqueness enforcement, and set-based text processing.
    /// </summary>
    public class SpellChecker
    {
        // Core HashSet for dictionary storage - provides O(1) word lookups
        private HashSet<string> dictionary;
        
        // Text analysis results - populated after analyzing a file
        private List<string> allWordsInText;
        private HashSet<string> uniqueWordsInText;
        private HashSet<string> correctlySpelledWords;
        private HashSet<string> misspelledWords;
        private string currentFileName;
        
        public SpellChecker()
        {
            dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            allWordsInText = new List<string>();
            uniqueWordsInText = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            correctlySpelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            misspelledWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            currentFileName = "";
        }
        
        /// <summary>
        /// Gets the number of words in the loaded dictionary.
        /// </summary>
        public int DictionarySize => dictionary.Count;
        
        /// <summary>
        /// Gets whether a text file has been analyzed.
        /// </summary>
        public bool HasAnalyzedText => !string.IsNullOrEmpty(currentFileName);
        
        /// <summary>
        /// Gets the name of the currently analyzed file.
        /// </summary>
        public string CurrentFileName => currentFileName;
        
        /// <summary>
        /// Gets basic statistics about the analyzed text.
        /// </summary>
        public (int totalWords, int uniqueWords, int correctWords, int misspelledWords) GetTextStats()
        {
            return (
                allWordsInText.Count,
                uniqueWordsInText.Count,
                correctlySpelledWords.Count,
                misspelledWords.Count
            );
        }
        
        /// <summary>
        /// Load words from the specified file into the dictionary HashSet.
        /// </summary>
        public bool LoadDictionary(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                    return false;

                string[] lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    string normalized = NormalizeWord(line);
                    if (!string.IsNullOrEmpty(normalized))
                    {
                        dictionary.Add(normalized);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Load and analyze a text file, populating all internal collections.
        /// </summary>
        public bool AnalyzeTextFile(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                    return false;

                string content = File.ReadAllText(filename);
                string[] tokens = content.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                allWordsInText.Clear();
                uniqueWordsInText.Clear();
                currentFileName = filename;

                foreach (string token in tokens)
                {
                    string normalized = NormalizeWord(token);
                    if (!string.IsNullOrEmpty(normalized))
                    {
                        allWordsInText.Add(normalized);
                        uniqueWordsInText.Add(normalized);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// After analyzing text, categorize unique words into correct and misspelled.
        /// </summary>
        public void CategorizeWords()
        {
            correctlySpelledWords.Clear();
            misspelledWords.Clear();

            foreach (string word in uniqueWordsInText)
            {
                if (dictionary.Contains(word))
                {
                    correctlySpelledWords.Add(word);
                }
                else
                {
                    misspelledWords.Add(word);
                }
            }
        }
        
        /// <summary>
        /// Check if a specific word is in the dictionary and/or appears in analyzed text.
        /// </summary>
        public (bool inDictionary, bool inText, int occurrences) CheckWord(string word)
        {
            string normalized = NormalizeWord(word);
            bool inDict = dictionary.Contains(normalized);
            bool inTxt = uniqueWordsInText.Contains(normalized);
            int count = 0;

            if (inTxt)
            {
                foreach (string w in allWordsInText)
                {
                    if (w == normalized)
                    {
                        count++;
                    }
                }
            }

            return (inDict, inTxt, count);
        }
        
        /// <summary>
        /// Return a sorted list of all misspelled words from the analyzed text.
        /// </summary>
        public List<string> GetMisspelledWords(int maxResults = 50)
        {
            if (misspelledWords.Count == 0)
                return new List<string>();

            return misspelledWords.OrderBy(w => w).Take(maxResults).ToList();
        }
        
        /// <summary>
        /// Return a sample of unique words found in the analyzed text.
        /// </summary>
        public List<string> GetUniqueWordsSample(int maxResults = 20)
        {
            if (uniqueWordsInText.Count == 0)
                return new List<string>();

            return uniqueWordsInText.OrderBy(w => w).Take(maxResults).ToList();
        }
        
        // Helper method for consistent word normalization
        private string NormalizeWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return "";
                
            // Remove punctuation and convert to lowercase
            word = Regex.Replace(word.Trim(), @"[^\w]", "");
            return word.ToLowerInvariant();
        }
    }
}