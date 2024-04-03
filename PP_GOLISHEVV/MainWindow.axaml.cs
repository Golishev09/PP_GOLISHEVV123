using Avalonia.Controls;

namespace PP_GOLISHEVV;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    string connectionString = "server=localhost;database=abd1_11;port=3306;User Id=root;password=Qwerty_123456";
    
    public void Authorization(object? sender, RoutedEventArgs e)
    {
        string username = Login.Text;
        string password = Password.Text;
        bool isClient = IsUserValidClient(username, password);
        bool isEmployee = IsUserValidEmployee(username, password);
        if (isClient || isEmployee)
        {
            if (isClient)
            {
                Uslugi client = new Uslugi();
                client.AddButton.IsVisible = false;
                client.EditButton.IsVisible = false;
                client.DeleteButton.IsVisible = false;
                Hide();
                client.Show();
            }
            else
            {
                Uslugi employee = new Uslugi();
                employee.AddButton.IsVisible = true;
                employee.EditButton.IsVisible = true;
                employee.DeleteButton.IsVisible = true;
                employee.Rec.IsVisible = false;
                employee.Title = "Услуги (права сотрудника)";
                Hide();
                employee.Show();
            } 
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль");
        }
    }
    
    private bool IsUserValidEmployee(string username, string password)
        {
            
            bool isValid = false;
        
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM empl WHERE Email = @Username AND Password= @Password";
        
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
        
                    connection.Open();
        
                    object result = command.ExecuteScalar();
                    int count = Convert.ToInt32(result);
        
                    if (count == 1)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
    private bool IsUserValidClient(string username, string password) //проверка пользователей по БД
    {
        bool isValid = false;
    
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT COUNT(1) FROM client WHERE Email = @Username AND Password= @Password";
    
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
    
                connection.Open();
    
                object result = command.ExecuteScalar();
                int count = Convert.ToInt32(result);
    
                if (count == 1)
                {
                    isValid = true;
                }
            }
        }
        return isValid;
    }

    private void Registration(object? sender, RoutedEventArgs e)
    {
        RegisterForm reg = new RegisterForm();
        Hide();
        reg.Show();
    }
    public void Exit_Program(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}