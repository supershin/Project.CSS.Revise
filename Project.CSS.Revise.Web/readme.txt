
// Scaffold UAT
Scaffold-DbContext "Data Source=10.0.20.14;Initial Catalog=css_uat_2;User ID=css;Password=css@2022;TrustServerCertificate=True;" Microsoft.EntityFrameWorkCore.SqlServer -outputdir Data -context CSSContext -contextdir Data -DataAnnotations -UseDatabaseNames -Force

// Package gen Excel
Install-Package ClosedXML

