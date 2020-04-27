using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

namespace Core.Utils
{
    public class Config
    {
        private Dictionary<string, string> pairs;

        public Config() {

            pairs = new Dictionary<string, string>();
            Changed = false;
        }

        public bool Changed {

            get;
            private set;
        }
        
        //Looks at dics, gets the keye and converts to an array
        public string[] Keys {

            get { return pairs.Keys.ToArray(); }
        }
        
        //"this" refers to the object instance
        //get: called if reading a value
        //  returnes the value of the pair associated with the key
        //set: called if setting a value
        public string this[string key] {
            get {

                if (HasKey(key)) return pairs[key];
                else return null;
            }
            set {

                Changed = true;

                if (HasKey(key)) pairs[key] = value;
                else pairs.Add(key, value);
            }
        }

        public bool HasKey(string key) {

            return pairs.ContainsKey(key);
        }

        public bool HasKeys(params string[] keys) {

            foreach(string key in keys) {

                if (!HasKey(key)) return false;
            }

            return true;
        }
        
        //convert a key as type T
        public T GetKeyAs<T>(string key) where T : System.IConvertible {

            string value;

            value = this[key];
            return (T)Convert.ChangeType(value, typeof(T));
        }
        
        //adding or changing a value associated with key as type T
        public void SetKeyAs<T>(string key, T value) where T : System.IConvertible {

            string valStr = value.ToString();
            this[key] = valStr;
        }

        public class Reader
        {
            public static Config ReadFile(string path) {

                Config config = new Config();

                try {

                    StreamReader reader = new StreamReader(path);
                    while (!reader.EndOfStream) {

                        string line = reader.ReadLine();

                        line = line.Trim();

                        if (line.Length == 0 || line.StartsWith("//")) continue;

                        if (line.Contains("=")) {

                            string[] kv = line.Split('=');

                            config[kv[0].Trim()] = kv[1].Trim();
                        }
                        // TODO: Add support for sections to config files
                    }

                    config.Changed = false;

                    reader.Close();

                }
                catch (IOException e) {

                    Debug.LogError(e.Message);
                }

                return config;
            }
        }
        public class Writer
        {
            public static Config WriteFile(string path, Config config) {

                try {

                    StreamWriter writer = new StreamWriter(path);

                    foreach (string key in config.Keys) {

                        writer.WriteLine(key + " = " + config[key]);
                    }

                    writer.Close();

                }
                catch (IOException e) {

                    Debug.Log(e.Message);
                }

                return config;
            }
        }
    }
}
