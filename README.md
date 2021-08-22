# TRIGGERGRAM

<a href="https://azure.microsoft.com/" title="Microsoft Azure"><img src="./docs/azure-icon.svg" alt="Microsoft Azure" width="25px" height="25px"></a>
<a href="https://docs.microsoft.com/en-us/azure/azure-sql/" title="Azure SQL Server"><img src="./docs/sql.svg" alt="Azure SQL Server" width="25px" height="25px"></a>
<a href="https://dotnet.microsoft.com/" title=".NET Core"><img src="./docs/dotnet.svg" alt=".NET Core" width="25px" height="25px"></a>
<a href="https://www.python.org/" title="Python"><img src="./docs/python.svg" alt="Python" width="25px" height="25px"></a>
<a href="https://www.typescriptlang.org/" title="Typescript"><img src="./docs/typescript-icon.svg" alt="Typescript" width="25px" height="25px"></a>
<a href="https://reactjs.org/" title="React"><img src="./docs/react.svg" alt="React" width="25px" height="25px"></a>
<a href="https://redux.js.org/" title="Redux"><img src="./docs/redux.svg" alt="Redux" width="25px" height="25px"></a>
<a href="https://material-ui.com/" title="Material UI"><img src="./docs/material-ui.svg" alt="Material UI" width="25px" height="25px"></a>


An [Instagram](https://www.instagram.com)-like social network for uploading and sharing photos, that is build in top of the serverless architecture using **MS Azure** cloud provider.

## Covered Azure services

 - [Functions](https://docs.microsoft.com/en-us/azure/azure-functions/);
 - [SignalR service](https://docs.microsoft.com/en-us/azure/azure-signalr/);
 - [Blob Storage](https://docs.microsoft.com/en-us/azure/storage/);
 - [Service Bus](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview);

## Service description

| Service             | Functional                                                                                                                          |
|---------------------|-------------------------------------------------------------------------------------------------------------------------------------|
| Accounts management |  1) Authentication and authorization;<br> 2) C.R.U.D. on accounts;<br> 3) User content management;<br> 4) User activity management; |
| Content processing  |  1) Auto-tagging user images using computer vision technologies;<br> 2) Semantic analysis of user comments;                         |

## Architecture

![architecture](./docs/album-on-functions.png)
