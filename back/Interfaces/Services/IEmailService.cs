namespace EitechPfe.Interfaces
{
    public interface IEmailService
    {
        Task SendSubscriptionReminderEmail(string emailAddress);
    }
}
