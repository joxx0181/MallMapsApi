using FakeItEasy;
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Data;
using MallMapsApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallApiUnitTest
{
    public class LoginHandlerTest
    {
        [Fact]
        public void GetSessionUser_Mapping_Verifiy()
        {
            var fakeUser = new FirmUser(0, "Admin", "Admin", "Redektør", "123123123", 10203040);
            var expected = new SessionUserDecorator(fakeUser);

            var firmUsers = new List<FirmUser>();
            firmUsers.Add(fakeUser);

            var fakeCrud = A.Fake<ICrudAccess>();

            A.CallTo(() => fakeCrud.Get<FirmUser>(A<Dictionary<string, object>>.Ignored)).Returns(firmUsers);

            LoginHandler handler = new LoginHandler(fakeCrud);

            var actual = handler.Verifiy("Admin", "Admin");

            Assert.Equal(expected.SessionKey, actual.SessionKey);
            Assert.Equal(expected.Role, actual.Role);
        }

        [Fact]
        public void IsSessionUserNull_Mapping_Verifiy()
        {
            var fakeCrud = A.Fake<ICrudAccess>();

            A.CallTo(() => fakeCrud.Get<FirmUser>(A<Dictionary<string, object>>.Ignored)).Returns(null);

            LoginHandler handler = new LoginHandler(fakeCrud);

            var actual = handler.Verifiy("Admin", "Admin");

            Assert.Null(actual);
        }

        [Fact]
        public void UserWasAdded_Mapping_CreateUser()
        {
            var expected = "User added";

            var fakeCrud = A.Fake<ICrudAccess>();

            LoginHandler handler = new LoginHandler(fakeCrud);
            A.CallTo(() => fakeCrud.InsertScalar<FirmUser>(A<FirmUser>.Ignored)).Returns(-1);
            var actual = handler.CreateUser("Admin", "Admin", "Redektør", 10203040);


            Assert.Equal(expected, actual);

        }
    }
}
