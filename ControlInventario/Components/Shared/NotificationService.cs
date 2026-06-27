using System;
using System.Threading;

namespace ControlInventario.Components.Shared
{
    public class NotificationService : INotificationService, IDisposable
    {
        private Timer? _timer;
        public event Action? OnShow;
        public string Message { get; private set; } = string.Empty;
        public string Type { get; private set; } = string.Empty;
        public bool IsVisible { get; private set; }

        public void ShowSuccess(string msg) => Show(msg, "success");
        public void ShowError(string msg) => Show(msg, "danger");
        public void ShowWarning(string msg) => Show(msg, "warning");

        private void Show(string msg, string type)
        {
            _timer?.Dispose();
            Message = msg;
            Type = type;
            IsVisible = true;
            OnShow?.Invoke();

            _timer = new Timer(_ => 
            {
                Hide();
            }, null, 3000, Timeout.Infinite);
        }

        public void Hide()
        {
            IsVisible = false;
            OnShow?.Invoke();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
