

using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.ViewModels
{

    public class SkadooshVM : ViewModelBase
    {

        public SkadooshVM()
        {

        }

        public async void UnitTestTableInitialize()
        {

            var s = new Survey() { SurveyTitle = "Favorite Color", ChannelName="test12345", Description="First Unit Test" };
            var table = this.AzureClient.GetTable<Survey>();
            await table.InsertAsync(s);

            var question = new Question() { QuestionText = "What is your favority color?", SurveyId = s.Id, IsMultiSelect = false };
            await AzureClient.GetTable<Question>().InsertAsync(question);


            var options = new List<Option>();
            options.Add(new Option() { OptionText = "Red", QuestionId = question.Id });
            options.Add(new Option() { OptionText = "Blue", QuestionId = question.Id });
            options.Add(new Option() { OptionText = "Orange", QuestionId = question.Id });
            options.Add(new Option() { OptionText = "Green", QuestionId = question.Id });
            foreach (var opt in options)
            {
                await AzureClient.GetTable<Option>().InsertAsync(opt);
            }
        }

    }
}
