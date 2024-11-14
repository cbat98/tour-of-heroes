#!/bin/sh

set -e

wwwroot_dir=/usr/share/nginx/html

sed -i "s|#{apiHost}#|$API_HOST|g" $wwwroot_dir/assets/config.json
sed -i "s|#{apiRoute}#|$API_ROUTE|g" $wwwroot_dir/assets/config.json

echo "Config loaded:"
cat $wwwroot_dir/assets/config.json
