using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.DTO
{
    public record UpdateSuperHeroDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string? Place { get; set; }
    }
}
