#pragma checksum "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d9e7aa95917962a98754f965bcd592710da486d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Settings_Index), @"mvc.1.0.view", @"/Views/Settings/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d9e7aa95917962a98754f965bcd592710da486d1", @"/Views/Settings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82bc5e49829c4a4d7ea5749c4439e0d5e66697ba", @"/Views/_ViewImports.cshtml")]
    public class Views_Settings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AspNetCoreTemplate.Web.ViewModels.Settings.SettingsListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "InsertSetting", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
  
    this.ViewData["Title"] = "Settings";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>");
#nullable restore
#line 7 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
Write(this.ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n\n");
#nullable restore
#line 9 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
 foreach (var setting in this.Model.Settings)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>");
#nullable restore
#line 11 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
    Write(setting.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 11 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
                 Write(setting.NameAndValue);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\n");
#nullable restore
#line 12 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/Settings/Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d9e7aa95917962a98754f965bcd592710da486d15540", async() => {
                WriteLiteral("Insert new");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AspNetCoreTemplate.Web.ViewModels.Settings.SettingsListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591