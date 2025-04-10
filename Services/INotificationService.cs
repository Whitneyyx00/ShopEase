// Services/INotificationService.cs
using System;
using System.Threading.Tasks;

namespace ShopEase.Client.Services
{
    public interface INotificationService
    {
        event Action<string, NotificationType> OnShow;
        Task ShowNotification(string message, NotificationType type);
    }

    public enum NotificationType
    {
        Success,
        Info,
        Warning,
        Error
    }
}