namespace ApiVida.Entities
{
    public class UserEntity
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime registerDate   { get; set; }
        public DateTime lastRegisterUpdate { get; set; }
        public string email { get; set; }
    }
}
