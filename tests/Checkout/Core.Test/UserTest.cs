namespace Core.Test;

using Core.Enums;
using Core.Models;

public class UserTest
{
    [Fact]
    public void TestUserCanBeCreated()
    {
        var name = "Exemplo";
        var email = "Email";
        var password = "Password";

        var user = new User(
            name: name,
            email: email,
            password: password
        );

        Assert.IsType<int>(user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
    }

    [Fact]
    public void TestUserCanBeCreatedWithDefaultRoleCustomer()
    {
        var user = new User(
            name: "Exemplo",
            email: "Email",
            password: "Password"
        );

        Assert.Equal(ERole.CUSTOMER, user.Role);
    }
}
