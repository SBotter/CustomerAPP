namespace CustomerAPP.Client.Index.CustomerIndex
{
    public interface ICustomerIndex
    {
        public void AddCustomerToIndex(List<Customer> customers);
        public List<Customer> Search(string searchTerm);
    }
}
