FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as builder
COPY . /code
RUN dotnet publish -c release -o /app /code/src/Services/ReferenceBook/ReferenceBook.Api && \
    dotnet tool restore --tool-manifest /code/.config/dotnet-tools.json && \
    find ~/.nuget/packages/dotnet-ef -name dotnet-ef.dll | xargs -I% cp % /code/ef.dll

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=builder /app /app
CMD ["/app/ReferenceBook.Api"]