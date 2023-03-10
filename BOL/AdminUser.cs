namespace BOL;
using System.ComponentModel.DataAnnotations;

public class AdminUser{
    public string Uname{get;set;}
    public string Password{get;set;}
    public string Role {get;set;}
    [Key]
    public int uid{get;set;}
}