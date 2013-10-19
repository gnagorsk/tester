﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Questions", "QuestionCategory", "Question", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Questionnaire.Question), "Category", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Questionnaire.Category))]
[assembly: EdmRelationshipAttribute("Questions", "QuestionAnswer", "Question", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Questionnaire.Question), "Answer", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Questionnaire.Answer))]
[assembly: EdmRelationshipAttribute("Questions", "CorrectQuestionAnswer", "Question", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Questionnaire.Question), "Answer", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Questionnaire.Answer))]

#endregion

namespace Questionnaire
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class QuestionsContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new QuestionsContainer object using the connection string found in the 'QuestionsContainer' section of the application configuration file.
        /// </summary>
        public QuestionsContainer() : base("name=QuestionsContainer", "QuestionsContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new QuestionsContainer object.
        /// </summary>
        public QuestionsContainer(string connectionString) : base(connectionString, "QuestionsContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new QuestionsContainer object.
        /// </summary>
        public QuestionsContainer(EntityConnection connection) : base(connection, "QuestionsContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Question> QuestionSet
        {
            get
            {
                if ((_QuestionSet == null))
                {
                    _QuestionSet = base.CreateObjectSet<Question>("QuestionSet");
                }
                return _QuestionSet;
            }
        }
        private ObjectSet<Question> _QuestionSet;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Category> CategorySet
        {
            get
            {
                if ((_CategorySet == null))
                {
                    _CategorySet = base.CreateObjectSet<Category>("CategorySet");
                }
                return _CategorySet;
            }
        }
        private ObjectSet<Category> _CategorySet;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Answer> AnswerSet
        {
            get
            {
                if ((_AnswerSet == null))
                {
                    _AnswerSet = base.CreateObjectSet<Answer>("AnswerSet");
                }
                return _AnswerSet;
            }
        }
        private ObjectSet<Answer> _AnswerSet;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the QuestionSet EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToQuestionSet(Question question)
        {
            base.AddObject("QuestionSet", question);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the CategorySet EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCategorySet(Category category)
        {
            base.AddObject("CategorySet", category);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the AnswerSet EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToAnswerSet(Answer answer)
        {
            base.AddObject("AnswerSet", answer);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Questions", Name="Answer")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Answer : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Answer object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="text">Initial value of the text property.</param>
        public static Answer CreateAnswer(global::System.Int32 id, global::System.String text)
        {
            Answer answer = new Answer();
            answer.id = id;
            answer.text = text;
            return answer;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String text
        {
            get
            {
                return _text;
            }
            set
            {
                OntextChanging(value);
                ReportPropertyChanging("text");
                _text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("text");
                OntextChanged();
            }
        }
        private global::System.String _text;
        partial void OntextChanging(global::System.String value);
        partial void OntextChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Questions", Name="Category")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Category : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Category object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="text">Initial value of the text property.</param>
        public static Category CreateCategory(global::System.Int32 id, global::System.String text)
        {
            Category category = new Category();
            category.id = id;
            category.text = text;
            return category;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String text
        {
            get
            {
                return _text;
            }
            set
            {
                OntextChanging(value);
                ReportPropertyChanging("text");
                _text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("text");
                OntextChanged();
            }
        }
        private global::System.String _text;
        partial void OntextChanging(global::System.String value);
        partial void OntextChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Questions", "QuestionCategory", "Question")]
        public EntityCollection<Question> Question
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Question>("Questions.QuestionCategory", "Question");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Question>("Questions.QuestionCategory", "Question", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Questions", Name="Question")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Question : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Question object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="title">Initial value of the title property.</param>
        /// <param name="text">Initial value of the text property.</param>
        public static Question CreateQuestion(global::System.Int32 id, global::System.String title, global::System.String text)
        {
            Question question = new Question();
            question.id = id;
            question.title = title;
            question.text = text;
            return question;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String title
        {
            get
            {
                return _title;
            }
            set
            {
                OntitleChanging(value);
                ReportPropertyChanging("title");
                _title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("title");
                OntitleChanged();
            }
        }
        private global::System.String _title;
        partial void OntitleChanging(global::System.String value);
        partial void OntitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String text
        {
            get
            {
                return _text;
            }
            set
            {
                OntextChanging(value);
                ReportPropertyChanging("text");
                _text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("text");
                OntextChanged();
            }
        }
        private global::System.String _text;
        partial void OntextChanging(global::System.String value);
        partial void OntextChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Questions", "QuestionCategory", "Category")]
        public Category Category
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Questions.QuestionCategory", "Category").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Questions.QuestionCategory", "Category").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Category> CategoryReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Category>("Questions.QuestionCategory", "Category");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Category>("Questions.QuestionCategory", "Category", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Questions", "QuestionAnswer", "Answer")]
        public EntityCollection<Answer> Answers
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Answer>("Questions.QuestionAnswer", "Answer");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Answer>("Questions.QuestionAnswer", "Answer", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Questions", "CorrectQuestionAnswer", "Answer")]
        public Answer CorrectAnswer
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Answer>("Questions.CorrectQuestionAnswer", "Answer").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Answer>("Questions.CorrectQuestionAnswer", "Answer").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Answer> CorrectAnswerReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Answer>("Questions.CorrectQuestionAnswer", "Answer");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Answer>("Questions.CorrectQuestionAnswer", "Answer", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
