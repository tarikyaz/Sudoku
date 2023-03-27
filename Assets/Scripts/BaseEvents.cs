

using System;

public static class BaseEvents 
{
    public static Action OnLevelStart;
    public static Action<Level> OnLevelInit;
    public static Action<bool> OnLevelFinish;


    public static void CallLevelStart()
    {
        OnLevelStart?.Invoke();
    }
    public static void CallOnLevelInit(Level level)
    {
        OnLevelInit?.Invoke(level);
    }

    public static void CallOnLevelFinish( bool isWin)
    {
        OnLevelFinish?.Invoke(isWin);
    }
}
