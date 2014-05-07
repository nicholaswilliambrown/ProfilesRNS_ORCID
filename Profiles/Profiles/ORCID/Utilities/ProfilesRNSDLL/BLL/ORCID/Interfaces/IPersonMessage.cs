using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfilesRNSDLL.BLL.ORCID.Interfaces
{
    public interface IPersonMessage
    {
        List<BO.ORCID.PersonMessage> GetByPersonID(int personID);
        List<BO.ORCID.PersonMessage> GetByPersonIDAndRecordStatusID(int personID, int recordStatusID);
        BO.ORCID.PersonMessage Create(BO.ORCID.Person person, BO.ORCID.REFPermission refPermission);
        void CreateUploadMessages(BO.ORCID.Person person, string loggedInInternalUsername);
    }
}
