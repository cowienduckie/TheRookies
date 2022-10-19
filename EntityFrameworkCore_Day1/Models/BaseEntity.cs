using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models;

public abstract class BaseEntity<T>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public T Id { get; set; }
}