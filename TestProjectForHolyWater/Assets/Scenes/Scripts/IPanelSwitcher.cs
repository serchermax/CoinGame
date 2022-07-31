using System;

public interface IPanelSwitcher
{
    public event Action<bool> OnPanelSwitchEvent;    
}
