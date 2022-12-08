using FakeItEasy;
using MallMapsApi.Controllers.Views;
using MallMapsApi.Data;
using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallApiUnitTest
{
    public class MapHandlerTest
    {
        [Fact]
        public void SaveMallMap_Insert_MapHandler()
        {
            var expected = "Completed";

            var fakeMapV = new MapV(1, 0, new List<ComponentV>());
            int[] fakeX = new int[] { 10, 20, 30, 10 };
            int[] fakeY = new int[] { 10, 30, 20, 10 };
            fakeMapV.Components.Add(new ComponentV("", 0, new GeodataV("POLYGON", fakeX, fakeY, 4321)));

            var fakeMap = new Map(1, 0);

            var fakeCrud = A.Fake<ICrudAccess>();

            MapHandler handler = new MapHandler(fakeCrud);

            A.CallTo(() => fakeCrud.InsertScalar<Map>(fakeMap)).Returns(fakeMap.Id);

            var actual = handler.CreateMap(fakeMapV);

            Assert.Equal(expected, actual);
        }
    }
}
