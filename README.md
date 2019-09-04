# SuperOffice.DevNet.Online.SystemUser.NetCore.Console

Application that demonstrates how to use .NET Core (2.2) and exchange a SuperOffice Online system user token for a system user ticket.

## Getting Started From Scratch

Create a .net core console application. Right-click project and in the Add menu, click Connected Service.

![AddService](Assets/images/AddConnectedService.png)

In the Connected Services pane, click the __Microsoft WCF Web Services Reference Provider__ option.

![Connected Services](Assets/images/SelectWCFServiceReference.png)

In the Configure WCF Web Service Reference pain, click __Browse__, then locate and select the __PartnerSystemUserService.wsdl__ file.

![WSDL Select](Assets/images/BrowsePartnerSystemUserServiceWSDL.png)

When the service definition has finished loading, expand the available Services and click the __IPartnerSystemUserService__ interface, then click __Finish__.

![Generate Code](Assets/images/SelectInterfaceThenFinish.png)

Observe the generated code based on the WSDL definition.

![Generate Code](Assets/images/SoSystemUserServiceCode.png)

Review the code in the project to see how to call this SOAP service and exchange a system user token for a system user ticket.
