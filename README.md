# WebApiOpenWeather

Será necessário acessar a pasta do projeto para executar a api
Quando for acessada, devará ser feito o build:
>>> dotnet build

Após o build executar o comando com oos paramêtros:
>>> dotnet run --project WebApiWeatherMap 01/11/2020, 03/11/2020, "Sao Paulo"

O resultado pode ser acessado pelo local host do powershell/cmd

ex.: https://localhost:5001/api/OpenWeather

Testes estão na dll UnitTests.
Swagger está na dll WebApiSwagger
