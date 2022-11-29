using FakeItEasy;
using MallMapsApi.Controllers.Decorators;
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
        /// <summary>
        /// Geodata will be empty since, im not sure if we can fake the spatial data
        /// </summary>
        [Fact]
        public void GetComponents_Mapping_GetByID()
        {
            byte[] fakeimg = new byte[] { 10, 20, 33, 21, 10, 23 };
            var fakecomponent = new Component(1, 1, fakeimg, "beskrivelse", new SqlGeometry(), 1);
            var fakecomponent2 = new Component(2, 1, null, null, new SqlGeometry(), 1);
            var expected = new IconComponentDecorator(fakecomponent);
            var expected2 = new BasicComponentDecorator(fakecomponent2);

            var components = new List<Component>();
            components.Add(fakecomponent);
            components.Add(fakecomponent2);

            var fakeCrud = A.Fake<ICrudAcess>();

            A.CallTo(() => fakeCrud.Get<Component>(A<Dictionary<string, object>>.Ignored)).Returns(components);

            ComponentHandler handler = new ComponentHandler(fakeCrud);

            var actual = handler.GetById(1);

            IconComponentDecorator item1 = (IconComponentDecorator)actual["IconComponent"];
            BasicComponentDecorator item2 = (BasicComponentDecorator)actual["BasicComponent"];

            Assert.Equal(expected.GeoData, item1.GeoData);
            Assert.Equal(expected.Image, item1.Image);
            Assert.Equal(expected.MapID, item1.MapID);
            Assert.Equal(expected.Description, item1.Description);
            Assert.Equal(expected.Zindex, item1.Zindex);

            Assert.Equal(expected2.Zindex, item2.Zindex);
            Assert.Equal(expected2.MapId, item2.MapId);
            Assert.Equal(expected2.GeoData, item2.GeoData);
        }

        [Fact]
        public void IsComponentsNull_Mapping_GetByID()
        {
            var fakeCrud = A.Fake<ICrudAcess>();

            A.CallTo(() => fakeCrud.Get<Component>(A<Dictionary<string, object>>.Ignored)).Returns(null);

            ComponentHandler handler = new ComponentHandler(fakeCrud);

            var actual = handler.GetById(1);

            Assert.Null(actual);
        }
    }
}
