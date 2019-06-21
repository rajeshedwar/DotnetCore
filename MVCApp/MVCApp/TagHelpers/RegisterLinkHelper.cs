using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.TagHelpers
{
    [HtmlTargetElement("register",TagStructure=TagStructure.WithoutEndTag)]
    public class RegisterLinkHelper:TagHelper
    {
        public string url { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("href", url);
            output.Content.SetContent("Register");
        }
    }
}
