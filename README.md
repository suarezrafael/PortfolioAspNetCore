# PortfolioAspNetCore Docker e Racher Desktop DockerHub
## Exemplo de projeto web app com docker e rancher desktop publicando o app no docker hub

1. Criar projeto App Web ASPNET Core usando .Net 6
2. Clicar com botão direito no projeto `Adicionar`  em seguida `Suporte do Docker` Selecione o Sistema operacional `Windows` ou `Linux` 
3. O arquivo Dockerfile será criando dentro do projeto


`FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
`

`FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ['PortfolioAspNetCore/PortfolioAspNetCore.csproj', 'PortfolioAspNetCore/']
RUN dotnet restore 'PortfolioAspNetCore/PortfolioAspNetCore.csproj'
COPY . .
WORKDIR '/src/PortfolioAspNetCore'
RUN dotnet build 'PortfolioAspNetCore.csproj' -c Release -o /app/build`

`
FROM build AS publish
RUN dotnet publish "PortfolioAspNetCore.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", 'PortfolioAspNetCore.dll']
`

4. Abra o console do desenvolvedor e execute o comando para construir a imagem do app 

    `nerdctl build -t aspnetcore-portfolio -f PortfolioAspNetCore/Dockerfile .`

 
5. Rodar a aplicação local 

    `nerdctl run -d -p 5000:80 --name aspnetcore-portfolio-demo1 aspnetcore-portfolio`
      
6. publicar no docker hub register:

    `nerdctl login -u rafaelvsuarez`
 
 7. Colocar tag na imagem:

    `nerdctl tag aspnetcore-portfolio rafaelvsuarez/aspnetcore-portfolio-app`
 
 8. Publicar Imagem:

    `nerdctl push rafaelvsuarez/aspnetcore-portfolio-app`
    
 9. Baixar imagem:

    `nerdctl pull rafaelvsuarez/aspnetcore-portfolio-app`
    
 10. Rodar imagem baixada:

    `nerdctl run -d -p 5000:80 --name docker-hub-portfolio-app rafaelvsuarez/aspnetcore-portfolio-app`
