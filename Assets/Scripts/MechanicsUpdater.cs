using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.UI;

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
    public AttributesText Text;
    public Text ErrorText;
    public Image BlackBg;
    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "attributes.ini");
    bool IsErrored = false;
    string lastError = "";
    public AttriubteElement[] Attributes;
    bool errorApplied = false;
    static List<Action<int>> CallBacks = new List<Action<int>>();
    static Dictionary<string, int> attributes = new Dictionary<string, int>();
    void InitializeDictionary()
    {
        foreach (var attr in Attributes)
        {
            attributes[attr.Name] = attr.DefaultValue;
        }
        Text.Buffer = string.Join("\n", attributes.Select(u => $"{u.Key}={u.Value}"));
        Text.BufferLoaded = true;
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
                reader.Close();
                throw new InvalidAttributesFileException($"Line {lineIdx + 1} is not in the correct format");
            }
            string key = line[0]; string value = line[1];
            if (ExisitingTokens.Contains(key))
            {
                reader.Close();
                throw new InvalidAttributesFileException($"File contains more than a single definition for attriubte {key}");
            }
            if (attributes.ContainsKey(key))
            {
                ExisitingTokens.Add(key);
                try
                {
                    int val = int.Parse(value);
                    if (val < 0 || val > 10)
                    {
                        mustBeReWritten = true;
                        val = Mathf.Clamp(val, 0, 10);
                    }
                    if (attributes[key] != val)
                    {
                        changesExist = true;
                    }
                    attributes[key] = val;
                }
                catch (FormatException)
                {
                    reader.Close();
                    throw new InvalidAttributesFileException($"Attribute {key}'s value is non-numeric: {value}");
                }
            }
            else
            {
                mustBeReWritten = true;
            }
            lineIdx++;
        }
        reader.Close();
        if (mustBeReWritten)
        {
            RewriteFile();
        }
        else if (ExisitingTokens.Count < attributes.Keys.Count)
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
            try
            {
                if (LoadAndValidate())
                {
                    StateChanger.RevertToDefaultState();
                    Text.Buffer = string.Join("\n", attributes.Select(u => $"{u.Key}={u.Value}"));
                    Text.BufferLoaded = true;
                }
                if (IsErrored)
                {
                    IsErrored = false;
                }
            }
            catch (InvalidAttributesFileException ex)
            {
                if (!IsErrored)
                {
                    IsErrored = true;
                }
                if (ex.Message != lastError)
                {
                    lastError = ex.Message;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Watcher died: {ex.Message}");
            }
            finally
            {
                Thread.Sleep(1000);
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
    void Update()
    {
        if (IsErrored)
        {
            if (!errorApplied)
            {
                errorApplied = true;
                BlackBg.gameObject.SetActive(true);
                ErrorText.gameObject.SetActive(true);
                ErrorText.text = lastError;
            }
        }
        else if (errorApplied)
        {
            errorApplied = false;
            BlackBg.gameObject.SetActive(false);
            ErrorText.gameObject.SetActive(false);
        }
    }
    public static void Subscribe(Action<int> CallBack)
    {
        CallBacks.Add(CallBack);
    }
    public static int GetValueOf(string Attribute)
    {
        int val = attributes[Attribute];
        foreach (var callBack in CallBacks)
        {
            callBack(val);
        }
        return val;
    }
}
