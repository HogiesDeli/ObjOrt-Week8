namespace Week8;
using System.Data;
using MySql.Data.MySqlClient;
class BusinessLogic
{
    static void Main(string[] args)
    {
        bool _continue = true;
        User user;

        GuiTier appGUI = new GuiTier();
        DataTier database = new DataTier();

        // start GUI
        user = appGUI.Login();


        if (database.LoginCheck(user))
        {

            while (_continue)
            {
                int option = appGUI.Dashboard(user);
                switch (option)
                {
                    // check enrollment
                    case 1:
                        DataTable tableEnrollment = database.CheckEnrollment(user);
                        if (tableEnrollment != null)
                            appGUI.DisplayEnrollment(tableEnrollment);
                        break;
                    // Add A Course
                    case 2:
                        DataTable tableCourse = database.ListCourse();
                        if (tableCourse != null)
                            appGUI.ShowCourse(tableCourse);
                        Console.WriteLine("Please input courseID");
                        int courseID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please input a semester in TermYear format, e.g: Fall2022, Spring2021");
                        string semester = Console.ReadLine();
                        database.AddCourse(user, courseID, semester);

                        tableEnrollment = database.CheckEnrollment(user);
                        if (tableEnrollment != null)
                            appGUI.DisplayEnrollment(tableEnrollment);
                        break;

                    // Drop A Course
                    case 3:
                        tableEnrollment = database.CheckEnrollment(user);
                        if (tableEnrollment != null)
                            appGUI.DisplayEnrollment(tableEnrollment);
                        
                        Console.WriteLine("Please input a courseID");
                        courseID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please input a semester in TermYear format, e.g: Fall2022, Spring2021");
                        semester = Console.ReadLine();
                        database.DropCourse(user, courseID, semester);

                        tableEnrollment = database.CheckEnrollment(user);
                        if (tableEnrollment != null)
                            appGUI.DisplayEnrollment(tableEnrollment);
                        break;

                    // Log Out
                    case 4:
                        _continue = false;
                        Console.WriteLine("Log out, Goodbye.");
                        break;
                    // default: wrong input
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }

            }
        }
        else
        {
            Console.WriteLine("Login Failed, Goodbye.");
        }
    }
}
