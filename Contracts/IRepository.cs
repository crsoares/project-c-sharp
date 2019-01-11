namespace Project.Contracts
{
    public interface IRepository
    {
        dynamic all();
        dynamic find(int id);
        dynamic create(dynamic obj);
        dynamic update(int id, dynamic obj);
        dynamic delete(int id);

    }
}