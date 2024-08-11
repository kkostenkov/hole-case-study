namespace Managers.Social
{
    public class LoginResult
    {
        public bool IsSuccess;
        public string Token;
        public string Provider;

        public override string ToString()
        {
            return $"Success: {this.IsSuccess}, Provider: {this.Provider}, Token: {this.Token}";
        }
    }
}