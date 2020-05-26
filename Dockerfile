FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get install -y nodejs

COPY *.sln ./
COPY Flashcards.WebAPI/*.csproj ./Flashcards.WebAPI/
RUN dotnet restore

COPY Flashcards.WebAPI/. ./Flashcards.WebAPI/
RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/publish .
# ENTRYPOINT [ "dotnet", "Flashcards.WebAPI.dll" ]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Flashcards.WebAPI.dll
