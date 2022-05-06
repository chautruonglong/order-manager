#!/bin/bash

set -x

export MSSQL_DB="AspNetTutorial"
export MSSQL_USER="sa"
export MSSQL_PASSWORD="Ctlbi@0775516337"

export CONNECTION_STRING="Data Source=sql-server-tutorial,1433;Database=${MSSQL_DB};Integrated Security=false;User ID=${MSSQL_USER};Password=${MSSQL_PASSWORD};"

export DOCKER_EXTERNAL_RESOURCES="/asp-net-tutorial/resources"
export EXTERNAL_RESOURCES="/root/app/resources"

export SECRET_KEY="JRi824hxq7m1vjVlpE88fWxLfOfF9nszm5CiE9hWFffvSRQTZ7"

export API_BASE_URL="http://api.mvg-sky.com/api"

docker compose build
docker compose up -d

# shellcheck disable=SC2046
docker rmi $(docker images -f "dangling=true" -aq)
