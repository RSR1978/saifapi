namespace saif_api.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string userName, string password);
        Token NewToken();
        bool verifyToken(string token);
    }
}