namespace Trybe.Domain.Entidades
{
    public class Usuario
    {

        public Guid id { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; internal set; }
        //public string Email { get; set; }
        //public int Telefone { get; set; }
    }
}
