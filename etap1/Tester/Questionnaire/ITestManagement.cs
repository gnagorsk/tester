﻿using System;
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
        int createCategory(String title, int category);
        [OperationContract]
        int createQuestion(String text, int category, int correct_answer);
        [OperationContract]
        int createAnswer(String text);
        [OperationContract]
        bool addAnswerToQuestion(int answer, int question);
        [OperationContract]
        bool removeAnswerFromQuestion(int answer, int question);
        [OperationContract]
        bool deleteCategory(int category);
        [OperationContract]
        bool deleteQuestion(int question);
    }
}