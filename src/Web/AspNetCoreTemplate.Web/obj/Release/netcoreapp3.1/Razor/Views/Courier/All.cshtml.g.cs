#pragma checksum "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fe15dffc2aa34dec66ae792bcbb1d906769707c1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Courier_All), @"mvc.1.0.view", @"/Views/Courier/All.cshtml")]
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
#line 1 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
using AspNetCoreTemplate.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
using AspNetCoreTemplate.Web.Resources;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe15dffc2aa34dec66ae792bcbb1d906769707c1", @"/Views/Courier/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82bc5e49829c4a4d7ea5749c4439e0d5e66697ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Courier_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AspNetCoreTemplate.Web.ViewModels.Couriers.CourierViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Courier", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Users", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NavBarAdmin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 8 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
  
    this.ViewData["Title"] = $"All Couriers";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<div class=\"text-center\">\n    <h1 class=\"display-4\">");
#nullable restore
#line 13 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
                     Write(this.ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n</div>\n\n");
#nullable restore
#line 16 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
 if (Model.Curiers.Count() == 0)
{

}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"row\">\n");
#nullable restore
#line 23 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
         foreach (var curier in Model.Curiers)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"media col-md-4\">\n                <img");
            BeginWriteAttribute("src", " src=\"", 580, "\"", 599, 1);
#nullable restore
#line 26 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
WriteAttributeValue("", 586, curier.Image, 586, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"100\" height=\"80\" class=\"mr-3\"");
            BeginWriteAttribute("alt", " alt=\"", 637, "\"", 655, 1);
#nullable restore
#line 26 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
WriteAttributeValue("", 643, curier.Name, 643, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n                <div class=\"media-body\">\n                    <h5 class=\"mt-0\"><a");
            BeginWriteAttribute("href", " href=\"", 738, "\"", 745, 0);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 28 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
                                           Write(curier.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h5>\n                </div>\n            </div>\n");
#nullable restore
#line 31 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n");
#nullable restore
#line 33 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<hr />\n<div>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fe15dffc2aa34dec66ae792bcbb1d906769707c19005", async() => {
#nullable restore
#line 37 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
                                                                                  Write(SharedLocalizer.GetLocalizedHtmlString("CreateNewCourier"));

#line default
#line hidden
#nullable disable
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fe15dffc2aa34dec66ae792bcbb1d906769707c110940", async() => {
#nullable restore
#line 38 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Courier/All.cshtml"
                                                                                     Write(SharedLocalizer.GetLocalizedHtmlString("Back"));

#line default
#line hidden
#nullable disable
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
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public LocService SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AspNetCoreTemplate.Web.ViewModels.Couriers.CourierViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
