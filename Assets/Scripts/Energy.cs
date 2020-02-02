using UnityEngine;
using UnityEngine.UI;

public class Energy : StateChanger
{
    public int MaxEnergy;
    int CurrentEnergy;
    public Image Image;
    public Text Text;
    void Start()
    {
        CurrentEnergy = MaxEnergy;
        MechanicsUpdater.Subscribe(CallBack);
        Text.text = "100%";
    }
    void Update()
    {
        CheckIfRevertRequested();
    }
    void CallBack(int e)
    {
        CurrentEnergy -= e;
        Text.text = $"{Mathf.RoundToInt((CurrentEnergy / (float)MaxEnergy) * 100.0f)}%";
        if (CurrentEnergy <= 0.0f)
        {
            StateChanger.RevertToDefaultState();
        }
        else
        {
            Image.fillAmount = Mathf.RoundToInt((CurrentEnergy / (float)MaxEnergy) * 24) / 24.0f;
        }
    }
    protected override void RevertState()
    {
        CurrentEnergy = MaxEnergy;
        Text.text = "100%";
        Image.fillAmount = 1.0f;
    }
}
