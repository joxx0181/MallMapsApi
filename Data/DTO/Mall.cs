using MallMapsApi.CustomAttributes;
namespace MallMapsApi.Data.DTO
{
    /// <summary>
    /// DTO model for Mall
    /// </summary>
    [Table(Name ="Mall")]
    public class Mall
    {

        /// <summary>
        /// ID for database
        /// </summary>
        [Column(Name = "id",IgnoreSql = true)]
        public int Id { get; set; }
        /// <summary>
        /// id reference to firm
        /// </summary>
        [Column(Name = "firmid")]
        public int FirmId { get; set; }

        /// <summary>
        /// mall location
        /// </summary>
        [Column(Name = "location")]
        public string Location { get; set; }
        /// <summary>
        /// Construct Mall from parsing parameters
        /// </summary>
        /// <param name="id">Id for DB</param>
        /// <param name="firmId">Id reference to firm</param>
        /// <param name="location">where is mall located in the world?</param>
        public Mall(int id, int firmId, string location)
        {
            Id = id;
            FirmId = firmId;
            Location = location;
        }

        //TODO : Replace constructor and use Activator with Arguments (this message can be seen under View -> TaskList)
        /// <summary>
        ///  Constructor for Activator
        /// </summary>
        public Mall()
        {
            //Adding this to prevent vs warning about nullable location
            Location = string.Empty;
        }
    }
}
