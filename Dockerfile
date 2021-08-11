FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /Source
 
COPY *.sln .
COPY Directory.Build.props .
 
#Copy the webApp
COPY WebApp/*.csproj ./WebApp/
COPY WebApp/. ./WebApp/

#Copy all the projects
COPY Contracts.Domain.Base/*.csproj ./Contracts.Domain.Base/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain.App/*.csproj ./Domain.App/
COPY Domain.Base/*.csproj ./Domain.Base/

#Copy all the source code
COPY Contracts.Domain.Base/. ./Contracts.Domain.Base/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain.App/. ./Domain.App/
COPY Domain.Base/. ./Domain.Base/

WORKDIR /Source/WebApp

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /App
COPY --from=build /Source/WebApp/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]
