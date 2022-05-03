using System;

namespace API.Models
{
    public class Hash
    {
        public Guid UniqueGuid { get; set; }
        public byte[] HashResult { get; set; }
    }
}
