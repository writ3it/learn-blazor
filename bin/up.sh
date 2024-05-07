#!/bin/env bash

if ! [ -x "$(command -v docker)" ]; then
    echo "Install docker first."
    exit
fi

docker compose up -d