// Open Closed Principle (OCP)


class ValidationService
{
    public bool ValidateEmail(string email)
    {
        // Email validation logic
        return true;
    }
}

class authenticationService
{
    public bool Authenticate(string username, string password)
    {
        // here validate using mail
        return true;
    }
}

// now new requirement comes as valaidate with phone number also

class newValidationService : ValidationService
{
    public bool ValidatePhoneNumber(string phoneNumber)
    {
        // Phone number validation logic
        return true;
    }
}

class newAuthenticationService : authenticationService
{
    public bool AuthenticateWithPhone(string phoneNumber, string password)
    {
        // Phone number authentication logic
        // Email authentication logic
        return true;
    }
}