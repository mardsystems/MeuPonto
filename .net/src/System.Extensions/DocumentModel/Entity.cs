using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace System.DocumentModel
{
    public abstract class Entity
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        public DateTime? CreationDate { get; set; }

        [Timestamp]
        public string? ETag { get; set; }
    }
}
