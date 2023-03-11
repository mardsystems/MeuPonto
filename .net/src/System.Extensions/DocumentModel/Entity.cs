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
        public Guid? Id { get; set; }

        public string? PartitionKey { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? Version { get; set; }

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object? obj) => obj is Entity entity
            && entity.Id == Id;
    }
}
