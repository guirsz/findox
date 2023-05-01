namespace Findox.Api.Domain
{
    public class DatabaseConfigurations
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string CommandTimeout { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"Server={Server};Port={Port};Database={Database};User Id={UserId};Password={Password};CommandTimeout={CommandTimeout};";
            }
        }
    }
}
