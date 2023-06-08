using System;

// Model клас
public class UserModel
{
    public string Username { get; set; }
    public string Email { get; set; }
}

// View клас
public class UserView
{
    public void DisplayUserDetails(string username, string email)
    {
        Console.WriteLine("User Details:");
        Console.WriteLine("Username: " + username);
        Console.WriteLine("Email: " + email);
    }
}

// Controller клас
public class UserController
{
    private UserModel model;
    private UserView view;

    public UserController(UserModel model, UserView view)
    {
        this.model = model;
        this.view = view;
    }

    public void UpdateUserData(string username, string email)
    {
        model.Username = username;
        model.Email = email;
    }

    public void RenderUserDetails()
    {
        view.DisplayUserDetails(model.Username, model.Email);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Створення моделі
        UserModel model = new UserModel();

        // Створення представлення
        UserView view = new UserView();

        // Створення контролера з моделлю і представленням
        UserController controller = new UserController(model, view);

        // Оновлення даних користувача через контролер
        controller.UpdateUserData("JohnDoe", "johndoe@example.com");

        // Відображення даних користувача через контролер та представлення
        controller.RenderUserDetails();
    }
}
