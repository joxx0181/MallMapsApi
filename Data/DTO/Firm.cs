using MallMapsApi.CustomAttributes;

namespace MallMapsApi.Data.DTO
{
    /// <summary>
    /// DTO model of Firm
    /// </summary>
    [Table(Name = "Firm")]
    public class Firm
    {
        /// <summary>
        /// Cvr number
        /// </summary>
        [Column(Name = "cvrnr")]
        public int Cvr { get; set; }
        /// <summary>
        /// The name of the firm
        /// </summary>
        [Column(Name="name")]
        public string Name { get; set; }
        /// <summary>
        /// Create an instance of Firm Class
        /// </summary>
        /// <param name="cvr"></param>
        /// <param name="name"></param>
        public Firm(int cvr, string name)
        {
            Cvr = cvr;
            Name = name;
        }

        //TODO : Replace constructor and use Activator with Arguments (this message can be seen under View -> TaskList)
        /// <summary>
        /// Empty Constructor for Reflection Activator
        /// </summary>
        public Firm()
        {

        }

    }
}
