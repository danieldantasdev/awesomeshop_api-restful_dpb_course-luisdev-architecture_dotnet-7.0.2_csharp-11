using Behavioral.Infrastructure.Notifications.Visitors;

namespace Behavioral.Infrastructure.Notifications;

public interface IMarketingMessage
{
    void Accept(INotificationVisitor visitor);
    string From { get; }
    string To { get; }
    string Content { get; }
}