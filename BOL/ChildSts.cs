namespace BOL;
using System.ComponentModel.DataAnnotations;
public class ChildSts{
   
    public int Chid{get;set;}
    public string Chname{get;set;}
    public string Payment{get;set;}
    public string Date{get;set;}
     [Key]
    public string? Chkey{get;set;}

}
