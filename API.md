# Redirif API
Only the `/api` endpoint is enabled without a master token set.
Other endpoints can be enabled by setting a `API_MASTER_TOKEN` environment variable which will be used as the master API token, which can generate nerd API tokens, so use a random password generator.
## Authentication
 - `/api` - Doesn't need any authentication.
 - `/api/redirect/*` - Requires a nerd or master token in the `Api-Token` header.
 - `/api/master/*` - Requires a master token in the `Api-Token` header.

Nerd API tokens can be created using the `/api/master/create` endpoint.

#### GET `/api`
Provides basic information about the Redirif server.
 - `appVersion` - Version of the Redirif server.
 - `frameworkVersion` - The version of ASP.NET running.
 - `totalRedirects` - Total numbers of redirects the server has.
 - `apiEnabled` - `true` if the `/api/redirect/*` and `/api/master/*` API endpoints are enabled.
##### Sample response:
```json
{
  "appVersion": "1.0.1.0",
  "frameworkVersion": "5.0.3",
  "totalRedirects": 12,
  "apiEnabled": true
}
```

#### POST `/api/redirect/create`
Creates a redirect from JSON in the request body. And reponds with the redirect key to the created redirect.
##### Sample body:
```json
{
   "Url":"https://www.youtube.com/watch?v=dQw4w9WgXcQ",
   "Title":"Cringe",
   "ImageUrl":"https://shitass.studio/",
   "Description":" Cringe",
   "SmallImage":true,
   "EmbedColor":"ff0000",
   "SiteName":"Cringe"
}
```
##### Sample reponses:
##### 200
```
X-lA
```
The redirect link can be constructed like so: `{RedirifServerUrl}/r/{Body}` e.g. `https://r.waifusfor.sale/r/X-lA`
##### 400
```
Url must be specified
```
You must provide the JSON with the `Url` property with a non-null value.

#### GET `/api/master/list`
Responds with an array of the currently generated nerd API tokens.
##### Sample response:
```
[
	"v23aZdOOMNfhhnUcJbjeCUGARsMj5OgBbSRS20MOo6H7eu2K7S6DeB6EmzPcLJUPLImG8IvfdCmY6hTqWAlZ9oTU0TVlNjeamK5wvAvA02DWfFjLFP1CKdDfqgf9MQcR3GZCkXyj34sRMv11xX3ToLYMJFtxm0NU1NrYofnw7mMNlGXm3QaQAR9UNbUkHA6Nh9Vuhnr4IlCgRNBz77q6vhIDMoAnFUM9qQ95VILFcSKc556FtjGEU4nUq36FTxM5"
]
```

#### GET `/api/master/create`
Generates a random nerd API token and sends it in the response.
##### Sample response:
```
v23aZdOOMNfhhnUcJbjeCUGARsMj5OgBbSRS20MOo6H7eu2K7S6DeB6EmzPcLJUPLImG8IvfdCmY6hTqWAlZ9oTU0TVlNjeamK5wvAvA02DWfFjLFP1CKdDfqgf9MQcR3GZCkXyj34sRMv11xX3ToLYMJFtxm0NU1NrYofnw7mMNlGXm3QaQAR9UNbUkHA6Nh9Vuhnr4IlCgRNBz77q6vhIDMoAnFUM9qQ95VILFcSKc556FtjGEU4nUq36FTxM5
```

#### DELETE `/api/master/delete`
Deletes a nerd API token specified in the request body.
##### Sample body:
```
v23aZdOOMNfhhnUcJbjeCUGARsMj5OgBbSRS20MOo6H7eu2K7S6DeB6EmzPcLJUPLImG8IvfdCmY6hTqWAlZ9oTU0TVlNjeamK5wvAvA02DWfFjLFP1CKdDfqgf9MQcR3GZCkXyj34sRMv11xX3ToLYMJFtxm0NU1NrYofnw7mMNlGXm3QaQAR9UNbUkHA6Nh9Vuhnr4IlCgRNBz77q6vhIDMoAnFUM9qQ95VILFcSKc556FtjGEU4nUq36FTxM5
```
##### Sample responses:
 - 200 - Nerd API token succesfully deleted.
 - 404 - Nerd API token doesn't exist.


#### GET `/api/master/listRedirects`

Responds with an array of all the redirects.

| Argument | Description                                             |
| -------- | ------------------------------------------------------- |
| start    | Specifies the start from which to return the redirects. |
| size     | Specifies the number of redirects to return.            |



##### Sample response:
```json
{
   "X-lA":{
      "Url":"https://www.youtube.com/watch?v=dQw4w9WgXcQ",
      "Title":"Cringe",
      "ImageUrl":"https://shitass.studio/",
      "Description":" Cringe",
      "SmallImage":true,
      "EmbedColor":"ff0000",
      "SiteName":"Cringe"
   }
}
```

#### DELETE `/api/master/deleteRedirect`
Deletes the redirect link specified in the request body.
##### Sample body:
```
X-lA
```
##### Sample responses:
 - 200 - Redirect deleted succesfully.
 - 404 - Redirect not found.