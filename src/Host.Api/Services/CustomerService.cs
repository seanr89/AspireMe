

public class CustomerService(AppDbContext appDbContext)
{

    public Customer? GetCustomer(Guid id)
    {
        return appDbContext.Customers.Find(id);
    }
}