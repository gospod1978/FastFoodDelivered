#pragma checksum "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c53632c059cc28b7cc4516d4a389705ad24f7b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Areas_AllByCity), @"mvc.1.0.view", @"/Views/Areas/AllByCity.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/_ViewImports.cshtml"
using AspNetCoreTemplate.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/_ViewImports.cshtml"
using AspNetCoreTemplate.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
using AspNetCoreTemplate.Common;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c53632c059cc28b7cc4516d4a389705ad24f7b5", @"/Views/Areas/AllByCity.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82bc5e49829c4a4d7ea5749c4439e0d5e66697ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Areas_AllByCity : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AspNetCoreTemplate.Web.ViewModels.Areas.AreaIndexViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Areas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cities", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "All", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 4 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
  
    this.ViewData["Title"] = $"Welcome to {GlobalConstants.SystemName}";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 8 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
 if (this.TempData["InfoMessageAreas"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"alert alert-success\">\n        ");
#nullable restore
#line 11 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
   Write(this.TempData["InfoMessageAreas"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n");
#nullable restore
#line 13 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"text-center\">\n    <h1 class=\"display-4\">");
#nullable restore
#line 16 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
                     Write(this.ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n    <p>Learn about <a href=\"https://docs.microsoft.com/aspnet/core\">building Web apps with ASP.NET Core</a>.</p>\n</div>\n\n");
#nullable restore
#line 20 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
 if (Model.Areas.Count() == 0)
{

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\n");
#nullable restore
#line 27 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
         foreach (var area in Model.Areas)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"media col-md-4\">\n                <img");
            BeginWriteAttribute("src", " src=\"", 705, "\"", 722, 1);
#nullable restore
#line 30 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
WriteAttributeValue("", 711, area.Image, 711, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"100\" height=\"80\" class=\"mr-3\"");
            BeginWriteAttribute("alt", " alt=\"", 760, "\"", 780, 1);
#nullable restore
#line 30 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
WriteAttributeValue("", 766, area.AreaName, 766, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                <div class=\"media-body\">\n                    <h5 class=\"mt-0\"><a");
            BeginWriteAttribute("href", " href=\"", 863, "\"", 879, 1);
#nullable restore
#line 32 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
WriteAttributeValue("", 870, area.Url, 870, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 32 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
                                                    Write(area.AreaName);

#line default
#line hidden
#nullable disable
            WriteLiteral("></h5>\n                </div>\n            </div>\n");
#nullable restore
#line 35 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n");
#nullable restore
#line 37 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Areas/AllByCity.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<hr />\n<div>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6c53632c059cc28b7cc4516d4a389705ad24f7b59841", async() => {
                WriteLiteral("Create New Area");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6c53632c059cc28b7cc4516d4a389705ad24f7b511475", async() => {
                WriteLiteral("Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AspNetCoreTemplate.Web.ViewModels.Areas.AreaIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
