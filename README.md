<h1 align="center">Requests Tab Plugin</h1>
<h3 align="center">Part of the <a href="https://jellyfin.org">Jellyfin Project</a></h3>

<p align="center">
This is a plugin built with DotNet8, that will allow admins to set a url for the request tab.
</p>

## Install

1. Open the dashboard in Jellyfin, then select `Plugins` and open `Repositories` at the top.

2. Click the `+` button, and add the repository URL below, naming it whatever you like. Save.

```
https://raw.githubusercontent.com/dkanada/jellyfin-plugin-intros/master/manifest.json
```

3. Select `Catalog` at the top and click on 'Intros' at the very bottom of the list. Install the most recent version.

4. Restart Jellyfin and go back to the plugin settings. Select `Installed` at the top and then 'Intros' to configure.

## Build

1. Clone or download this repository.

2. Ensure you have the DotNet SDK set up and installed.

3. Build the plugin with following commands.

```sh
dotnet publish --configuration release --output bin
```

4. Place the resulting binary in your `plugins` folder.