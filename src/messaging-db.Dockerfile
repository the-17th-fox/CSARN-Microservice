FROM mcr.microsoft.com/mssql/server:2019-latest

COPY CSARN.MessagingMicroservice/MessagingDatabase/build/MessagingDatabase_Create.sql /opt/scripts/

CMD bash -c "opt/mssql/bin/sqlservr  --accept-eula && sleep 60 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'CsarnMessagingDbServerPass17' -d messaging-db -i /opt/scripts/MessagingDatabase_Create.sql"
