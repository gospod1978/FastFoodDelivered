#pragma checksum "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/UsersData/ViewDocument.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bd8bb4d1e9c1ff42ac6b742dfb4d1741ff4d72c5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UsersData_ViewDocument), @"mvc.1.0.view", @"/Views/UsersData/ViewDocument.cshtml")]
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
#line 1 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/UsersData/ViewDocument.cshtml"
using AspNetCoreTemplate.Common;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bd8bb4d1e9c1ff42ac6b742dfb4d1741ff4d72c5", @"/Views/UsersData/ViewDocument.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"82bc5e49829c4a4d7ea5749c4439e0d5e66697ba", @"/Views/_ViewImports.cshtml")]
    public class Views_UsersData_ViewDocument : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AspNetCoreTemplate.Web.ViewModels.UsersData.DocumentDetails>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 4 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/UsersData/ViewDocument.cshtml"
  
    this.ViewData["Title"] = "UsersData";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>");
#nullable restore
#line 8 "/Users/gospod1978/Desktop/SoftUni/FastFoodDelivered/FastFoodDelivered/src/Web/AspNetCoreTemplate.Web/Views/UsersData/ViewDocument.cshtml"
Write(Model.DataFiles);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AspNetCoreTemplate.Web.ViewModels.UsersData.DocumentDetails> Html { get; private set; }
    }
}
#pragma warning restore 1591