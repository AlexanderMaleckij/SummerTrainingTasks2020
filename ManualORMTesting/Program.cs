using Session;

namespace ManualORMTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = Context.GetInstance(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Alexander\Desktop\Task6\ORM\Database\Session.mdf;Integrated Security=True;");



            //context.StudentGroups.Add(new StudentGroup("test add"));
            //context.StudentGroups.Where(x => x.GroupName == "test add").First().GroupName = "update test";
           // context.StudentGroups.Remove(context.StudentGroups.Where(x => x.GroupName == "test add").First());
            context.SaveChanges();

        }
    }  
}
