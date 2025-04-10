// Services/NotificationService.cs
using System;
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public class NotificationService : INotificationService
    {
        public event Action<string, NotificationType>? OnShow;

        public Task ShowNotification(string message, NotificationType type)
        {
            OnShow?.Invoke(message, type);
            return Task.CompletedTask;
        }
    }
}