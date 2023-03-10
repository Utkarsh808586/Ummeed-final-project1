namespace BOL;
using System.ComponentModel.DataAnnotations;

public class Account{
    public int Amount{get;set;}
    
    public int Accid{get;set;}
    [Key]
    public int Acckey{get;set;}
}