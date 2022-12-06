using FakeItEasy;
using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Controllers.Views;
using MallMapsApi.Data;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallApiUnitTest
{
    public class ComponentHandlerTest
    {

        [Fact]
        public void GetComponents_Mapping_GetByID()
        {
            int[] fakeX = new int[0];
            int[] fakeY = new int[0];
            byte[] fakeimg = new byte[] { 10, 20, 33, 21, 10, 23 };
            var fakecomponent = new Component(1, fakeimg, "beskrivelse", new SqlGeometry(),1);
            var fakecomponent2 = new Component(1, null, null, new SqlGeometry(), 1);
            var fakeGeoV1 = new GeodataV("GEOMETRYCOLLECTION", fakeX, fakeY, 0);
            var fakeGeoV2 = new GeodataV("GEOMETRYCOLLECTION", fakeX, fakeY, 0);

            var expected = new IconComponentDecorator(fakecomponent,fakeGeoV1);
            var expected2 = new BasicComponentDecorator(fakecomponent2,fakeGeoV2);

            var components = new List<Component>();
            components.Add(fakecomponent);
            components.Add(fakecomponent2);

            var fakeCrud = A.Fake<ICrudAccess>();

            A.CallTo(() => fakeCrud.Get<Component>()).Returns(components);

            ComponentHandler handler = new ComponentHandler(fakeCrud);

            var actual = handler.GetById(1);

            IconComponentDecorator item1 = ((IEnumerable<IconComponentDecorator>)actual["IconComponent"]).FirstOrDefault();
            BasicComponentDecorator item2 = ((IEnumerable<BasicComponentDecorator>)actual["BasicComponent"]).FirstOrDefault();

            Assert.Equal(expected.GeoData.Type, item1.GeoData.Type);
            Assert.Equal(expected.GeoData.XInput, item1.GeoData.XInput);
            Assert.Equal(expected.GeoData.YInput, item1.GeoData.YInput);
            Assert.Equal(expected.GeoData.Srid, item1.GeoData.Srid);
            Assert.Equal(expected.Image, item1.Image);
            Assert.Equal(expected.MapId, item1.MapId);
            Assert.Equal(expected.Description, item1.Description);
            Assert.Equal(expected.Zindex, item1.Zindex);

            Assert.Equal(expected2.Zindex, item2.Zindex);
            Assert.Equal(expected2.MapId, item2.MapId);
            Assert.Equal(expected2.GeoData.Type, item2.GeoData.Type);
            Assert.Equal(expected2.GeoData.XInput, item2.GeoData.XInput);
            Assert.Equal(expected2.GeoData.YInput, item2.GeoData.YInput);
            Assert.Equal(expected2.GeoData.Srid, item2.GeoData.Srid);
        }

        [Fact]
        public void IsComponentsNull_Mapping_GetByID()
        {
            var expected1 = new List<IconComponentDecorator>();
            var expected2 = new List<BasicComponentDecorator>();

            var fakeCrud = A.Fake<ICrudAcess>();

            A.CallTo(() => fakeCrud.Get<Component>()).Returns(new HashSet<Component>());

            ComponentHandler handler = new ComponentHandler(fakeCrud);

            var actual = handler.GetById(1);
            var item1 = ((IEnumerable<IconComponentDecorator>)actual["IconComponent"]).ToList();
            var item2 = ((IEnumerable<BasicComponentDecorator>)actual["BasicComponent"]).ToList();


            Assert.Equal(expected1.Count, item1.Count);
            Assert.Equal(expected2.Count, item2.Count);
        }
    }
}
