using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using MemomMvc52.Models;


namespace System.Web.Http
{
    //this is already available in the Thinktecture.IdendityManager project
    // nachi - 12 Oct 2014
    static class ModelStateDictionaryExtensions
    {
        //public static void AddErrors(this ModelStateDictionary modelState, IdentityManagerResult result)
        //{
        //    if (modelState == null) throw new ArgumentNullException("modelState");
        //    if (result == null) throw new ArgumentNullException("result");

        //    foreach (var error in result.Errors)
        //    {
        //        modelState.AddModelError("", error);
        //    }
        //}

        public static ErrorModel ToError(this ModelStateDictionary modelState)
        {
            if (modelState == null) throw new ArgumentNullException("modelState");

            return new ErrorModel
            {
                Errors = modelState.GetErrorMessages()
            };
        }

        public static string[] GetErrorMessages(this ModelStateDictionary modelState)
        {
            if (modelState == null) throw new ArgumentNullException("modelState");

            var errors =
                from error in modelState
                where error.Value.Errors.Any()
                from err in error.Value.Errors
                select String.IsNullOrWhiteSpace(err.ErrorMessage) ? err.Exception.Message : err.ErrorMessage;

            return errors.ToArray();
        }
    }
}


