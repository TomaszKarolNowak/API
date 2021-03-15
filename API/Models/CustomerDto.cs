using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    // TODO: The class name should be renamed, but because of no information about
    // the tasks context it is left as below.
    public class CustomerDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Timestamp
        /// </summary>
        public long T { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public double V { get; set; }
    }
}
