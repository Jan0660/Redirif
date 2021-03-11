# Redirif

A self-hostable URL shortener that forces chosen metadata to discord.

## Installation

You git clone this repo and run `dotnet run` to run the webapp.



This repository also contains a `Dockerfile` and a `docker-compose.yaml`  
 so you can run it in docker too by git cloning this repository
and running `docker-compose up -d` to build and deploy Redirif.


## Notes

* The docker compose forwards the webapp on port 9030. 
* Redirects are currently stored in a JSON file called `redirects.json`. (planning support for MongoDB)

## Credits

* Special thanks to `Alyner#7105` on discord for helping with CSS.
* https://www.reddit.com/r/discordapp/comments/82p8i6/a_basic_tutorial_on_how_to_get_the_most_out_of/

# Documentation

 * [API](API.md)
 * [Configuration](Configuration.md)