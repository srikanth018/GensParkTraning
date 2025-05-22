// Single Responsibility Principle (SRP)

// lets say

class User {
    void register();
    void login();
    void logout();
    void placeOrder();
    void cancelOrder();
    void makePayment();
    void validateEmail();
    void sendEmail();
    void sendSMS();
    void sendPushNotification();
}

// modified as-----------------------------
class UserService {
    void Register();
    void Login();
    void Logout();
}

class OrderService {
    void PlaceOrder();
    void CancelOrder();
}

class PaymentService {
    void MakePayment();
}

class NotificationService {
    void SendEmail();
    void SendSMS();
    void SendPushNotification();
}

class EmailValidator {
    bool ValidateEmail(string email);
}
