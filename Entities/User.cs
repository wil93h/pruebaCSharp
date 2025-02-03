using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

namespace pruebaCSharp.Entities;

public class User
{
    [Column("id")] 
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    [Column("email")] 
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [Column("password_hash")]
    public string Password { get; set; } = string.Empty;
    
    [Column("created_at")] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void SetPassword(string password) => Password = BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string password) => BCrypt.Net.BCrypt.Verify(password, Password);
}