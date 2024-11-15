#!/bin/sh

set -e

config_dir=/app

sed -i "s|#{CONNECTION_STRING}#|$CONNECTION_STRING|g" $config_dir/appsettings.json

echo "Config loaded:"
cat $config_dir/appsettings.json

dotnet ./TourOfHeroes.API.dll
