using EntityLayer.Concrete;

namespace ContactTestProject.MockData;

public class ContactMockData
{
    public static List<Contact> GetContactList()
    {
        return new List<Contact>
        {
            new Contact
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@example.com",
                CreatedDate = DateTime.Now,
                Status = true,


            },
            new Contact
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test2@example.com",
                CreatedDate = DateTime.Now,
                Status = true,

            },

            new Contact
            {
                Id = 3,
                FirstName = "Test",
                LastName = "Test",
                Email = "Test2@example.com",
                CreatedDate = DateTime.Now,
                Status = true,

            }
        };

    } 
    
    public static List<Contact> GetEmptyTodos()
    {
        return new List<Contact>();
    }

    public static Contact NewContact()
    {
        return new Contact
        {
            Id = 0,
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            CreatedDate = DateTime.Now,
            Status = true,
                
        };
    }
    
}