<h1 align="center">Requests Tab Plugin</h1>
<h3 align="center">Part of the <a href="https://jellyfin.org">Jellyfin Project</a></h3>

<p align="center">
This is a plugin built with DotNet8, that will allow admins to set a url for the request tab.
</p>

Web UI: https://github.com/soultaco83/jellyfin-web-requeststab

## Install

1. Open the dashboard in Jellyfin, then select `My Plugins` and open `Catalog` at the top.

2. Click the gear button, and add the repository URL below, naming it whatever you like. Save.

```
https://raw.githubusercontent.com/soultaco83/Jellyfin_requests_plugin/master/manifest.json
```

3. Select `Catalog` again and click on 'RequestsAddon' at the very bottom of the list. Install the most recent version.

4. Restart Jellyfin and go back to the plugin settings. Select `Installed` at the top and then 'RequestsAddon' to configure.

## Build

1. Clone or download this repository.

2. Ensure you have the DotNet SDK set up and installed.

3. Build the plugin with following commands.

```sh
dotnet publish Jellyfin.Plugin.RequestsAddon.csproj --configuration Release --output bin
```

4. Place the resulting Jellyfin.Plugin.RequestsAddon.dll in your `plugins` folder.
