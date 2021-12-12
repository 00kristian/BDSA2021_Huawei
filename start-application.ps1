$project = "Server"

$password = "TilJuleBalINisseLand10000"

Write-Host "Starting SQL Server"
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge
$database = "ProjectBankDB"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

Start-Sleep -s 10

Write-Host "Configuring Connection String"
cd ./Server

dotnet user-secrets set "ConnectionStrings:ProjectBankDB" "$connectionString"

cd ../Infrastructure

dotnet user-secrets set "ConnectionStrings:ProjectBankDB" "$connectionString"
dotnet ef database update -s . --context ProjectBankContext

cd ../Server

Write-Host "Starting App"
dotnet run