using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class ContactDbContext :DbContext
{
    public const string DEFAULT_SCHEMA = "Contact";

    public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Contact> Contacts { get; set; }
    
    public DbSet<Instructor> Instructors { get; set; }
    
    public DbSet<Testimonial> Testimonials { get; set; }
    
    
    public DbSet<Comment> Comments { get; set; }
    
    
}