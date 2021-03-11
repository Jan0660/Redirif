# Redirif Configuration
The first time you start Redirif a sample configuration file with all of the available options and their default values will be created in `data/config.json`.
## Options:
### `ShowSupport`
 - Type: boolean
 - Default: `true`
 - Toggles link to the support server in the footer.
### `ApiMasterToken`
 - Type: string
 - Default: `null` (API is disabled)
 - The API master token, note that if you set the `API_MASTER_TOKEN` environment variable this option will be overriden.
### `ApiOnly`
 - Type: boolean
 - Default: `false`
 - If `true`, disables the home page making your Redirif instance only accesible using the API.
### `EnableCors`
 - Type: boolean
 - Default: `true`
 - If `false`, disables Cors.