using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Enumerators;

namespace Sporthub.Model
{
    public class ErrorList
    {
        public ErrorList() 
        {
            IsError = false;
            Errors = new List<ErrorItem>();
        }

        public bool IsError { get; set; }
        public List<ErrorItem> Errors { get; set; }

        public ErrorList AddError(ErrorList errorList, string message, string formField)
        {
            errorList.IsError = true;
            ErrorItem item = new ErrorItem();
            item.ErrorLevel = ErrorLevel.Fatal;//TODO:
            item.Message = message;
            item.FormField = formField;
            errorList.Errors.Add(item);

            return errorList;
        }
    }
}
