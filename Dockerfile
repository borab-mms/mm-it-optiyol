# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


ENV NUGET_FALLBACK_PACKAGES=""
# COPY . .

# Solution ve proje dosyalarını kopyala
COPY MM.IT.sln ./
COPY MM.IT.Common/MM.IT.Common.csproj MM.IT.Common/
COPY MM.IT.Core/MM.IT.Core.csproj MM.IT.Core/
COPY MM.IT.Core.Api/MM.IT.Core.Api.csproj MM.IT.Core.Api/
COPY MM.IT.Data/MM.IT.Data.csproj MM.IT.Data/
COPY MMIT.Optiyol.Api/MM.IT.Optiyol.Api.csproj MMIT.Optiyol.Api/

# Restore
RUN dotnet restore MM.IT.sln

# Tüm dosyaları kopyala
COPY . .

# Build & Publish
RUN dotnet publish MMIT.Optiyol.Api/MM.IT.Optiyol.Api.csproj \
    -c Release \
    -o /app/publish 

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Güvenlik: root olmayan kullanıcı
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "MM.IT.Optiyol.Api.dll"] 