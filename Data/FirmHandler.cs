using MallMapsApi.Data.DTO;
using MallMapsApi.Interface;
using System.Data;

namespace MallMapsApi.Data
{
    // Create FirmHandler for firm implement interface IFirm
    public class FirmHandler : IFirm
    {
        // Database caller
        private readonly ICrudAccess _crud;

        // Data mapper for firm objects
        public DataMapper mapper;
            
        // Constructor declartion and inject database caller
        public FirmHandler(ICrudAccess crud)
        {
            _crud = crud;
        }

        // Create a new firm with Firm parameters   
        public string CreateFirm(int cvr, string name)
        {
            // Create an new instance of DataMapper
            mapper = new DataMapper();

            // Insert firm
            _crud.Insert<Firm>(mapper.FirmMapper(cvr, name));

            // Return firm added
            return "firm added";
        }
    }
}
