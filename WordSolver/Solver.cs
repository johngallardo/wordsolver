using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace WordSolver
{
    public class Solver
    {
        public Solver()
        {   
        }

        public void LoadDictionary()
        {
            var wordlist = new List<string>(300000);
            Debug.WriteLine("Starting to load dictionary.");
            Stopwatch sw = Stopwatch.StartNew();

            var dictionaryStream = Application.GetResourceStream(
                new Uri(@"Dictionaries.zip", UriKind.Relative));

            var wordStream = Application.GetResourceStream(dictionaryStream,
                new Uri(DictionaryFile, UriKind.Relative));

            using (var stream = wordStream.Stream)
                wordlist.AddRange(LoadWords(stream));

            Debug.WriteLine("Dictionary load completed in {0} milliseconds", sw.ElapsedMilliseconds);
            m_words = wordlist;
            m_isLoaded = true;
        }

        private static IEnumerable<string> LoadWords(Stream stream)
        {
            var reader = new StreamReader(stream);
            string line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }

        public IEnumerable<Word> GetWords(Constraints constraints)
        {
            if (constraints == null)
                yield break;

            if (!m_isLoaded)
            {
                LoadDictionary();
            }

            if (m_words == null)
                yield break;

            foreach (var candidate in m_words)
            {
                Word solvedWord;
                if (constraints.TryCandidateWord(candidate, out solvedWord))
                    yield return solvedWord;
            }
        }

        public string DictionaryFile 
        {
            get { return m_dictionary; }
            set
            {
                if (value != m_dictionary)
                {
                    m_isLoaded = false;
                    m_dictionary = value;
                }
            }
        }

        private string m_dictionary;
        private bool m_isLoaded;
        private IEnumerable<string> m_words;
    }
}
