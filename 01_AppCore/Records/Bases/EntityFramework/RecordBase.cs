using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Records.Bases
{
    public abstract class RecordBase
    {
        public int  Id { get; set; }
        public string Guid { get; set; }
    }
}
