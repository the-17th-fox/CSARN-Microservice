## Deploying:
- First of all, all dependent sql-db-project must be builded before starting the composing. 
- - [CSARN.MessagingMicroservice > MessagingDatabase] 

- Development environment (by default):
```powershell
docker-compose -f docker-compose.yml -f docker-compose.Development.yml up -d
```
- Production environment:
```powershell
docker-compose -f docker-compose.yml -f docker-compose.Production.yml up -d
```