﻿using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace LowlandTech.TinyTools
{
    public static class InterpolationExtensions
    {
        public static string Interpolate<T>(this string template, T model)
        {
            Guard.Against.NullOrEmpty(template, nameof(template), "Template should be supplied.");
            Guard.Against.Null(model, nameof(model), "Model should be supplied.");

            if (model is IDictionary<string, string> dictionary)
            {
                foreach (var entry in dictionary)
                {
                    template = template.Replace($"{{{entry.Key}}}", entry.Value);
                }
            }
            else
            {
                var props = model.GetType().GetProperties();
                foreach (var prop in props.Where(p => p.GetValue(model) != null))
                {
                    template = template.Replace($"{{{prop.Name}}}", prop.GetValue(model).ToString());
                }
            }
            return template;
        }
    }
}
