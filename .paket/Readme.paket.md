## General

1. PM> `.paket/paket.bootstrapper.exe` only needed for downloading or updating paket.exe
2. PM> `.paket/paket.exe restore` restore packages.
3. PM> `.paket/paket.exe auto-restore on` restore packages on build.
4. PM> `.paket/paket.exe update` update packages.
5. PM> `.paket/paket.exe install` install packages.
6. PM> `.paket/paket.exe install -f --createnewbindingfiles` install packages and create app.configs with redirects.

## Create packages

1. Build in release
2. PM> `.paket/paket.bootstrapper.exe` only needed for downloading paket.exe
3. PM> `.paket/paket.exe pack output publish symbols`
4. Packages are in the publish folder.

Docs: https://fsprojects.github.io/Paket/getting-started.html