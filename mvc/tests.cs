using NUnit.Framework;

[TestFixture]
public class UserControllerTests
{
    [Test]
    public void UpdateUserData_UpdatesModelData()
    {
        // Arrange
        UserModel model = new UserModel();
        UserView view = new UserView();
        UserController controller = new UserController(model, view);

        // Act
        controller.UpdateUserData("JohnDoe", "johndoe@example.com");

        // Assert
        Assert.AreEqual("JohnDoe", model.Username);
        Assert.AreEqual("johndoe@example.com", model.Email);
    }

    [Test]
    public void RenderUserDetails_DisplaysUserDetails()
    {
        // Arrange
        UserModel model = new UserModel();
        UserView view = new UserView();
        UserController controller = new UserController(model, view);

        // Set up the model data
        model.Username = "JohnDoe";
        model.Email = "johndoe@example.com";

        // Capture the console output
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            controller.RenderUserDetails();

            // Assert
            string expectedOutput = string.Format("User Details:\nUsername: {0}\nEmail: {1}\n", model.Username, model.Email);
            Assert.AreEqual(expectedOutput, sw.ToString());
        }
    }
}
