using Domain.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        [Column("id")]
        [Key] public int Id { get;  set; }
        public string? Email { get;  set; }    
        public string? Password { get;set; }
        public string? Name { get;  set; }
        public string? Document { get;  set; }
        public string? Phone { get;  set; }
        public string? Service { get; set; }// serviço utilizado para login
        public string? Sub { get; set; } // id que vem do google
        public string? Picture { get; set; } // imagem que vem do google
        public User()
        {

        }

        public User(string email,string name) => Validation(email, name);

        public User(int id, string email,string name)
        {
            
            Id = id;
            Validation(email, name);
        }

        private void Validation(string email, string name)
        {
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email must be informed");
            DomainValidationException.When(string.IsNullOrEmpty(name), "Name must be entered correctly");
         

            Email = email;
            Name = name;
           
          
        }
        public override string? ToString()
        {
            return $"{Id}";
        }
    }
}
