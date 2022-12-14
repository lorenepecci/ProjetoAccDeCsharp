namespace Trybe.Domain.Entidades
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

    }
}
