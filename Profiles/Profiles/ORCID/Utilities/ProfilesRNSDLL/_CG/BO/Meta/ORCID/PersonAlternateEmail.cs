using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Runtime.Serialization; 
using System.ComponentModel; 
 
namespace ProfilesRNSDLL.BO.ORCID
{ 
    public partial class PersonAlternateEmail
    { 
        public override int TableId { get { return 3579;} }
        public enum FieldNames : int { PersonAlternateEmailID = 43520, PersonID = 43521, EmailAddress = 43522, PersonMessageID = 43523 }
        public override string TableSchemaName { get { return "ORCID"; } }
    } 
} 
