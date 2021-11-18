using System.ComponentModel.DataAnnotations;

namespace EF_PB;

public record struct ProjectDTO([Required, StringLength(30)] string Name);