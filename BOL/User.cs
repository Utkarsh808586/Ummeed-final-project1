namespace BOL;
using System.ComponentModel.DataAnnotations;

public class User{
    public string Uname{get;set;}
    public  string Password{get;set;}
    public string Role{get;set;}

    public int Uid{get;set;}
    [Key]
    public int Uskey{get;set;}
}