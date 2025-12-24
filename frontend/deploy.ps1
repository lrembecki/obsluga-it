npm run build:prod
cd .\dist\angular-project\browser
Compress-Archive .\* -DestinationPath app.zip -Force
az webapp deploy --resource-group rg-obsluga-prd-01 --name web-obsluga-it-prd-01 --src-path .\app.zip
cd ..\..\..