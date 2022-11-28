using MallMapsApi.Controllers.Decorators;
using MallMapsApi.Data.DTO;

namespace MallMapsApi.Data
{
    public class DataMapper
    {
        public FirmUser FirmUserMapper(string username, string password, string role, int cvrnr)
        {
            FirmUser user = new FirmUser(0, username, password, role, null, cvrnr);
            return user;
        }

        /// <summary>
        /// Maps the compoents and returns a dictonary of Basic and icon ComponentDecorators.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,object> ComponentMapper(List<Component> components) 
        {
            Dictionary<string,object> componentDict = new Dictionary<string,object>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Img != null)
                    componentDict.Add("IconComponent", new IconComponentDecorator(components[i]));
                else
                    componentDict.Add("BasicComponent", new BasicComponentDecorator(components[i]));
            }
            if (componentDict.Count > 0)
                return componentDict;

            return null;
        }
    }
}
