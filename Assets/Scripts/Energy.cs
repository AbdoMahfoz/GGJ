using UnityEngine;

public class Energy : StateChanger
{
    public int MaxEnergy;
    int CurrentEnergy;
    public RectTransform Image;
    void Start()
    {
        CurrentEnergy = MaxEnergy;
        MechanicsUpdater.Subscribe(CallBack);
    }
    void Update()
    {
        CheckIfRevertRequested();
    }
    void CallBack(int e)
    {
        CurrentEnergy -= e;
        if (CurrentEnergy <= 0.0f)
        {
            StateChanger.RevertToDefaultState();
        }
        else
        {
            Image.transform.localScale = new Vector3((CurrentEnergy / (float)MaxEnergy), 1.0f, 1.0f);
        }
    }
    protected override void RevertState()
    {
        CurrentEnergy = MaxEnergy;
        Image.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
