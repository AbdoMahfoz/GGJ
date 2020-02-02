using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Assist : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    private Text messageText;

    private void Awake()
    {
        messageText = transform.Find("messageText").GetComponent<Text>();
    }

    private void Start()
    {
        textWriter.AddWriter(messageText, "Hala bel 5ames", .1f, true);
    }
}
