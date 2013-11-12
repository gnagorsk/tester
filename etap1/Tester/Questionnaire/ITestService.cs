using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;

namespace Questionnaire
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITestManagement" in both code and config file together.
    [DataContract()]
    public class RemoteProcessingError
    {
        [DataMember()]
        public string Title;
        [DataMember()]
        public string ExceptionMessage;
    }
    
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        Dictionary<int, String> getCategories();

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        Dictionary<int, String> getQuestions(int category);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        Dictionary<int, String> getQuestionAnswers(int question);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        bool validateAnswer(int question, int answer);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        Dictionary<int, String> getSubcategories(int category);
    }
}
