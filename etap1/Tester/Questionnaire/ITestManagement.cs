using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Questionnaire
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITestManagement" in both code and config file together.
    [ServiceContract]
    public interface ITestManagement
    {
        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        int createCategory(String title, int category);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        int createQuestion(String text, int category, int correct_answer);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        int createAnswer(String text);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        bool addAnswerToQuestion(int answer, int question);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        bool removeAnswerFromQuestion(int answer, int question);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        bool deleteCategory(int category);

        [OperationContract]
        [FaultContract(typeof(RemoteProcessingError))]
        bool deleteQuestion(int question);
    }
}
