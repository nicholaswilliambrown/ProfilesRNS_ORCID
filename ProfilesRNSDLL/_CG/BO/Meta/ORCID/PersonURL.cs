using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Runtime.Serialization; 
using System.ComponentModel; 
 
namespace ProfilesRNSDLL.BO.ORCID
{ 
    public partial class PersonURL
    { 
        public override int TableId { get { return 3621;} }
        public enum FieldNames : int { PersonURLID = 43751, PersonID = 43752, PersonMessageID = 43753, URLName = 43754, URL = 43755, DecisionID = 43756 }
        public override string TableSchemaName { get { return "ORCID"; } }
    } 
} 
