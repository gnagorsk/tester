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
    [ServiceContract]
    public interface ITestManagement
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        Dictionary<int, String> getCategories();
        [OperationContract]
        Dictionary<int, String> getQuestions(int category);
        [OperationContract]
        Dictionary<int, String> getQuestionAnswers(int question);
        [OperationContract]
        bool validateAnswer(int question, int answer);
    }
}
