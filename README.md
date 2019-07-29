
# AspNetCore Issue 12352

This is a sample that reproduces the problem described at [https://github.com/aspnet/AspNetCore/issues/12352](https://github.com/aspnet/AspNetCore/issues/12352).

A video demonstration can be found here: [aspnetcore-12352.mp4](aspnetcore-12352.mp4)


# Steps to reproduce

1. Open an elevated PowerShell prompt.
2. Run the following to generate the self-contained application:

`dotnet publish -c release -r win10-x64`

3. Create a new local user:

`New-LocalUser -Name issue12352`

4. Set the permissions for the user to run as a service using `secpol.msc`.

5. Install the application as a service:

`install-service.ps1`

6. Start the service using `services.msc`.
7. Inspect the Event Viewer using `eventvwr.msc` and observe that there are no logs about the service being started.

## Steps to fix

1. Stop the service.

2. uncomment line 30 in the source code `//webHostService.ServiceName = 
"Application";`.

3. Publish the application again:

`dotnet publish -c release -r win10-x64`

4. Start the service

5. Observe that the logging of service state changes is working now.
