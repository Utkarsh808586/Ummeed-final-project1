namespace DAL;

using Microsoft.EntityFrameworkCore;

using BOL;

public class CollectionContext:DbContext{
    public DbSet<User> User{get;set;}
    public DbSet<AdminUser> AdminUser{get;set;}
    public DbSet<ChildSts> ChildSts{get;set;}
    public DbSet<Account> Account{get;set;}
    public DbSet<Contributors> Contributors{get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        string url = @"server=localhost;user=root;password=Dhanraj@123;database=ABC";
        optionsBuilder.UseMySQL(url);
    }

}