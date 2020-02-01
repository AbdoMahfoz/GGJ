using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class InvalidAttributesFileException : Exception
{
    public InvalidAttributesFileException(string message) : base(message) { }
}
public class MechanicsUpdater : MonoBehaviour
{
    [Serializable]
    public struct AttriubteElement
    {
        public string Name;
        public int DefaultValue;
    }
    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "attributes.ini");
    bool IsErrored = false;
    string lastError = "";
    public AttriubteElement[] Attributes;
    static Dictionary<string, int> attributes = new Dictionary<string, int>();
    void InitializeDictionary()
    {
        foreach (var attr in Attributes)
        {
            attributes[attr.Name] = attr.DefaultValue;
        }
    }
    void RewriteFile()
    {
        StreamWriter writer = File.CreateText(filePath);
        foreach (var attr in attributes)
        {
            writer.WriteLine($"{attr.Key}={attr.Value}");
        }
        writer.Close();
    }
    bool LoadAndValidate()
    {
        bool mustBeReWritten = false;
        bool changesExist = false;
        StreamReader reader = File.OpenText(filePath);
        SortedSet<string> ExisitingTokens = new SortedSet<string>();
        int lineIdx = 0;
        while (reader.Peek() != -1)
        {
            string[] line = reader.ReadLine().Split('=');
            if (line.Length != 2)
            {
                throw new InvalidAttributesFileException($"Line {lineIdx} is not in the correct format");
            }
            string key = line[0]; string value = line[1];
            if (!attributes.ContainsKey(key))
            {
                mustBeReWritten = true;
            }
            if (ExisitingTokens.Contains(key))
            {
                throw new InvalidAttributesFileException($"File contains more than a single definition for attriubte {key}");
            }
            ExisitingTokens.Add(key);
            try
            {
                int val = int.Parse(value);
                if (attributes[key] != val)
                {
                    changesExist = true;
                }
                attributes[key] = val;
            }
            catch (FormatException)
            {
                throw new InvalidAttributesFileException($"Attribute {key}'s value is non-numeric: {value}");
            }
            lineIdx++;
        }
        reader.Close();
        if (mustBeReWritten)
        {
            RewriteFile();
        }
        if (ExisitingTokens.Count < attributes.Keys.Count)
        {
            StreamWriter writer = File.AppendText(filePath);
            foreach (var missingAttribute in attributes.Where(u => !ExisitingTokens.Contains(u.Key)))
            {
                writer.WriteLine($"{missingAttribute.Key}={missingAttribute.Value}");
            }
            writer.Close();
        }
        return changesExist;
    }
    void Watch()
    {
        while (true)
        {
            Thread.Sleep(1000);
            try
            {
                if (LoadAndValidate())
                {
                    Debug.Log("Updated Attributes:\n" +
                    string.Join("\n", attributes.Select(u => $"{u.Key}={u.Value}")));
                }
                if (IsErrored)
                {
                    IsErrored = false;
                    Debug.Log("Errors resolved, reloading game");
                    Debug.Log("Updated Attributes:\n" +
                    string.Join("\n", attributes.Select(u => $"{u.Key}={u.Value}")));
                }
            }
            catch (InvalidAttributesFileException ex)
            {
                if (!IsErrored)
                {
                    Debug.Log("Switched to error screen");
                }
                IsErrored = true;
                if (ex.Message != lastError)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
    }
    void Start()
    {
        InitializeDictionary();
        if (!File.Exists(filePath))
        {
            RewriteFile();
        }
        Task.Run(Watch);
    }
    public static int GetValueOf(string Attribute)
    {
        return attributes[Attribute];
    }
}
