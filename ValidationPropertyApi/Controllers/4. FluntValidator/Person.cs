using Flunt.Notifications;

namespace ValidationPropertyApi.Controllers.FluntValidator;

public class Person : Notifiable<Notification>
{
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string EmailAddress { get; set; }
}