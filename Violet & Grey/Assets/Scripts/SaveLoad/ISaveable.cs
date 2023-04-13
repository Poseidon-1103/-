
public class ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager.GetInstance().Register(this);
    }

    GameSaveData GenrateSaveDate()
    {
        return new GameSaveData();
    }

    void RestorGameData()
    {

    }
}
