interface INotification
{
    void SendEmail();
    void SendSMS();
    void SendPushNotification();
    void SendWhatsApp();
}

// A system that only sends emails
class EmailNotificationService : INotification
{
    public void SendEmail()
    {
        // Send Email
    }

    public void SendSMS()
    {
        throw new NotImplementedException(); // Not needed
    }

    public void SendPushNotification()
    {
        throw new NotImplementedException(); //  Not needed
    }

    public void SendWhatsApp()
    {
        throw new NotImplementedException(); //  Not needed
    }
}


/////////////////////////
/// 
/// 
interface IEmailNotification
{
    void SendEmail();
}

interface ISMSNotification
{
    void SendSMS();
}

interface IPushNotification
{
    void SendPushNotification();
}

interface IWhatsAppNotification
{
    void SendWhatsApp();
}


class EmailNotificationService : IEmailNotification
{
    public void SendEmail()
    {
        // Send Email
    }
}

class SMSAndPushNotificationService : ISMSNotification, IPushNotification
{
    public void SendSMS()
    {
        // Send SMS
    }

    public void SendPushNotification()
    {
        // Send Push Notification
    }
}
