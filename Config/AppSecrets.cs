namespace HemFixBack.Config
{
    public class AppSecrets
    {
        public JwtSecrets Jwt { get; set; }
    }

    public class JwtSecrets
    {
        public string Key { get; set; }
    }
}
