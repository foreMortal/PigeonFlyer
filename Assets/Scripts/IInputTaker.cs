using System;

public interface IInputTaker
{
    public Action<bool> GetInputAction();
}
