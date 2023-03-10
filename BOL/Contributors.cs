namespace BOL;
using System.ComponentModel.DataAnnotations ;

public class Contributors{
    
    
    public string Cname{get;set;}
   
    public int Paid_amount{get;set;}

    public string? Cmail{get;set;}
    [Key] 
    public int Ckey{get;set;}
}