for f in "$1"/*.js; do
    printf '/* eslint-disable */\n//@ts-nocheck\n\n' | cat - "${f}" > /tmp/eslint-fix && mv /tmp/eslint-fix "${f}"
done