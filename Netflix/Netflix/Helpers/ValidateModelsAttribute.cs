﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netflix.Helpers
{
   
        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    List<string> errors = new List<string>();
                    context.ModelState.Values.ToList().ForEach(x =>

                    {
                        x.Errors.ToList().ForEach(y =>
                        {
                            errors.Add(y.ErrorMessage);
                        });
                    });
                    var response = new
                    {
                        Code = 400,
                        Message = "Les champs requis sont obligatoires",
                        Errors = errors

                    };
                    context.Result = new BadRequestObjectResult(response);
                }
            }
        }
    
}
