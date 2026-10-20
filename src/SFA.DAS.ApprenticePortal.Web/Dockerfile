#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["SFA.DAS.ApprenticePortal.Web/SFA.DAS.ApprenticePortal.Web.csproj", "SFA.DAS.ApprenticePortal.Web/"]
COPY ["SFA.DAS.ApprenticePortal.SharedUi/SFA.DAS.ApprenticePortal.SharedUi.csproj", "SFA.DAS.ApprenticePortal.SharedUi/"]
COPY ["SFA.DAS.ApprenticePortal.Authentication/SFA.DAS.ApprenticePortal.Authentication.csproj", "SFA.DAS.ApprenticePortal.Authentication/"]
RUN dotnet restore "SFA.DAS.ApprenticePortal.Web/SFA.DAS.ApprenticePortal.Web.csproj"
COPY . .
WORKDIR "/src/SFA.DAS.ApprenticePortal.Web"
RUN dotnet build "SFA.DAS.ApprenticePortal.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SFA.DAS.ApprenticePortal.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SFA.DAS.ApprenticePortal.Web.dll"]