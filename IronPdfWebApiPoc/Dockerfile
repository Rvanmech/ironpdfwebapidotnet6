#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# https://stackoverflow.com/questions/72392631/c-sharp-ironpdf-shuts-down-net-6-web-api-under-linux-docker
# base runtime image (Debian 11 w/ .NET6 runtime)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# install necessary packages
RUN apt update \
    && apt install -y libgdiplus libxkbcommon-x11-0 libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libxrender1 libfontconfig1 libxshmfence1
# update write permissions
RUN chmod 777 .

# base development image (Debian 11 w/ .NET6 SDK)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IronPdfWebApiPoc/IronPdfWebApiPoc.csproj", "IronPdfWebApiPoc/"]
RUN dotnet restore "IronPdfWebApiPoc/IronPdfWebApiPoc.csproj"
COPY . .
WORKDIR "/src/IronPdfWebApiPoc"
RUN dotnet build "IronPdfWebApiPoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IronPdfWebApiPoc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IronPdfWebApiPoc.dll"]
