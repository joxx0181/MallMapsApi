using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Data;
using Microsoft.SqlServer.Types;

namespace MallApiUnitTest
{
    public class DataMapperTest
    {
        [Fact]

        public void GetFirmUser_Mapping_FirmUserMapper()
        {
            var expected = new FirmUser(0, "Admin", "Admin", "Rediktør", null, 10203040);

            DataMapper dataMapper = new DataMapper();

            var actual = dataMapper.FirmUserMapper("Admin", "Admin", "Rediktør", 10203040);

            Assert.Equal(expected.ID, actual.ID);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.Password, actual.Password);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.SessionKey, actual.SessionKey);
            Assert.Equal(expected.Firmid, actual.Firmid);

        }

        [Fact]
        public void GetDictonaryOfComponents_Mapping_ComponentMapper()
        {
            byte[] fakeimg = new byte[] { 10, 20, 33, 21, 10, 23 };

            var fakecomponent = new Component(1, fakeimg, "beskrivelse", new SqlGeometry(), 1);
            var fakecomponent2 = new Component(1, null, null, new SqlGeometry(), 1);

            var fakeComponentList = new List<Component>();
            fakeComponentList.Add(fakecomponent);
            fakeComponentList.Add(fakecomponent2);

            var expectedFakeComponentDecorator = new IconComponentDecorator(fakecomponent,null);
            var expectedFakeComponentDecorator2 = new BasicComponentDecorator(fakecomponent2,null);

            DataMapper dataMapper = new DataMapper();
            var actual = dataMapper.ComponentMapper(fakeComponentList);

            IconComponentDecorator item1 = (IconComponentDecorator)actual["IconComponent"];
            BasicComponentDecorator item2 = (BasicComponentDecorator)actual["BasicComponent"];

            Assert.Equal(expectedFakeComponentDecorator.GeoData, item1.GeoData);
            Assert.Equal(expectedFakeComponentDecorator.Image, item1.Image);
            Assert.Equal(expectedFakeComponentDecorator.MapId, item1.MapId);
            Assert.Equal(expectedFakeComponentDecorator.Description, item1.Description);
            Assert.Equal(expectedFakeComponentDecorator.Zindex, item1.Zindex);

            Assert.Equal(expectedFakeComponentDecorator2.Zindex, item2.Zindex);
            Assert.Equal(expectedFakeComponentDecorator2.MapId, item2.MapId);
            Assert.Equal(expectedFakeComponentDecorator2.GeoData, item2.GeoData);

        }

        [Fact]

        public void IsComponentsNull_Mapping_ComponentMapper()
        {
            DataMapper dataMapper = new DataMapper();

            var actual = dataMapper.ComponentMapper(null);

            Assert.Null(actual);
        }

        [Fact]
        public void IsComponentsEmpty_Mapping_ComponentMapper()
        {

            DataMapper dataMapper = new DataMapper();

            var actual = dataMapper.ComponentMapper(new List<Component>());

            Assert.Null(actual);
        }
    }
}
