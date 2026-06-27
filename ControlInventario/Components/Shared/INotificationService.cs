using System;

namespace ControlInventario.Components.Shared
{
    public interface INotificationService
    {
        event Action? OnShow;
        string Message { get; }
        string Type { get; } // success, danger, warning
        bool IsVisible { get; }
        void ShowSuccess(string msg);
        void ShowError(string msg);
        void ShowWarning(string msg);
        void Hide();
    }
}
