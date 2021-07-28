using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core_MVC.CustomTagHelper
{
    /// <summary>
    /// HtmlTargetElement, The HTML element on which the Custom Tage Helper will be applied
    /// </summary>
    [HtmlTargetElement("table", Attributes ="source-model")]
    public class TableTagHelper : TagHelper
    {
        /// <summary>
        /// ModelExpression: The Lambda Expression or the data to be passed to Helper
        /// </summary>
        public ModelExpression DataModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">The Current View Contect under which the Tag Helper is executing</param>
        /// <param name="output">The Expected HTML Rendered output</param>
        /// <returns></returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // read the data from ModelExpressoion
            // Since the Model passed to the TagHelper will be collection, it will be read as
            // IEnumerable
            IEnumerable model = DataModel.Model as IEnumerable;
            if (model == null)
            {
                return;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                // generate rendering
                foreach (var record in model)
                {
                    PropertyInfo[] properties = record.GetType().GetProperties();
                    string html = "";
                    for (int i = 0; i < properties.Length; i++)
                    {
                        html += $" {record.GetType().GetProperty(properties[i].Name).GetValue(record, null)}  ";
                    }
                    html += "";
                    sb.Append(html);
                }
                // set the rendered output
                output.Content.SetHtmlContent(sb.ToString());
            }
        }
    }
}
