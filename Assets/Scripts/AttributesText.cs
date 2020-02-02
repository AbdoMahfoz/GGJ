using UnityEngine;
using UnityEngine.UI;

public class AttributesText : MonoBehaviour
{
    public string Buffer;
    public bool BufferLoaded = false;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        if (BufferLoaded)
        {
            text.text = Buffer;
            Buffer = "";
            BufferLoaded = false;
        }
    }
}
