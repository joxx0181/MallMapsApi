using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;
using MallMapsApi.Data;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using FakeItEasy;
using MallMapsApi.Controllers.Views;

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
            int[] fakeX = new int[0];
            int[] fakeY = new int[0];
            var fakecomponent = new Component(1, fakeimg, "beskrivelse", new SqlGeometry(), 1);
            var fakecomponent2 = new Component(1, null, null, new SqlGeometry(), 1);
            var fakeGeoV1 = new GeodataV("GEOMETRYCOLLECTION", fakeX, fakeY, 0);
            var fakeGeoV2 = new GeodataV("GEOMETRYCOLLECTION", fakeX, fakeY, 0);

            var fakeComponentList = new List<Component>();
            fakeComponentList.Add(fakecomponent);
            fakeComponentList.Add(fakecomponent2);

            var expectedFakeComponentDecorator = new IconComponentDecorator(fakecomponent, fakeGeoV1);
            var expectedFakeComponentDecorator2 = new BasicComponentDecorator(fakecomponent2, fakeGeoV2);

            DataMapper dataMapper = new DataMapper();
            var actual = dataMapper.ComponentMapper(fakeComponentList);

            IconComponentDecorator item1 = ((IEnumerable<IconComponentDecorator>)actual["IconComponent"]).FirstOrDefault();
            BasicComponentDecorator item2 = ((IEnumerable<BasicComponentDecorator>)actual["BasicComponent"]).FirstOrDefault();

            Assert.Equal(expectedFakeComponentDecorator.GeoData.Type, item1.GeoData.Type);
            Assert.Equal(expectedFakeComponentDecorator.GeoData.XInput, item1.GeoData.XInput);
            Assert.Equal(expectedFakeComponentDecorator.GeoData.YInput, item1.GeoData.YInput);
            Assert.Equal(expectedFakeComponentDecorator.GeoData.Srid, item1.GeoData.Srid);
            Assert.Equal(expectedFakeComponentDecorator.Image, item1.Image);
            Assert.Equal(expectedFakeComponentDecorator.MapId, item1.MapId);
            Assert.Equal(expectedFakeComponentDecorator.Description, item1.Description);
            Assert.Equal(expectedFakeComponentDecorator.Zindex, item1.Zindex);

            Assert.Equal(expectedFakeComponentDecorator2.Zindex, item2.Zindex);
            Assert.Equal(expectedFakeComponentDecorator2.MapId, item2.MapId);
            Assert.Equal(expectedFakeComponentDecorator2.GeoData.Type, item2.GeoData.Type);
            Assert.Equal(expectedFakeComponentDecorator2.GeoData.XInput, item2.GeoData.XInput);
            Assert.Equal(expectedFakeComponentDecorator2.GeoData.YInput, item2.GeoData.YInput);
            Assert.Equal(expectedFakeComponentDecorator2.GeoData.Srid, item2.GeoData.Srid);

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
            var expected = new List<Component>();

            DataMapper dataMapper = new DataMapper();

            var actual = dataMapper.ComponentMapper(new List<Component>());

            var item1 = ((IEnumerable<IconComponentDecorator>)actual["IconComponent"]).ToList();
            var item2 = ((IEnumerable<BasicComponentDecorator>)actual["BasicComponent"]).ToList();

            Assert.Equal(expected.Count,item1.Count);
            Assert.Equal(expected.Count,item2.Count);
        }

        [Fact]

        public void IsMapVMapedToMap_Mapping_MapMapper()
        {
            var fakeMapV = new MapV(1, 0, new List<ComponentV>());
            int[] fakeX = new int[] { 10, 20, 30, 10 };
            int[] fakeY = new int[] { 10, 30, 20, 10 };
            fakeMapV.Components.Add(new ComponentV("", 0, new GeodataV("POLYGON", fakeX, fakeY, 4321)));

            var expectedSqlDat = SqlGeometry.Parse("POLYGON((10 10,20 30,30 20,10 10))");
            expectedSqlDat.STSrid = 4321;

            var expected = new Map(1, 0);
            expected.Components.Add(new Component(-1, null, "",expectedSqlDat, 0));

            DataMapper mapper = new DataMapper();

            var actual = mapper.MapMapper(fakeMapV);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.MallId, actual.MallId);
            Assert.Equal(expected.Layer, actual.Layer);
            Assert.Equal(expected.MallRef, actual.MallRef);
            Assert.Equal(expected.Components.FirstOrDefault().MapID, actual.Components.FirstOrDefault().MapID);
            Assert.Equal(expected.Components.FirstOrDefault().Id, actual.Components.FirstOrDefault().Id);
            Assert.Equal(expected.Components.FirstOrDefault().GetGeoData, actual.Components.FirstOrDefault().GetGeoData);
            Assert.Equal(expected.Components.FirstOrDefault().Img, actual.Components.FirstOrDefault().Img);
            Assert.Equal(expected.Components.FirstOrDefault().Description, actual.Components.FirstOrDefault().Description);

        }
    }
}
