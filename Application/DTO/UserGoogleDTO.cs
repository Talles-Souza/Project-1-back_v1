

namespace Application.DTO
{
    public  class UserGoogleDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }// serviço utilizado para login
        public string? Sub { get; set; } // id que vem do google
        public string Picture { get; set; } // imagem que vem do google
    }
}
