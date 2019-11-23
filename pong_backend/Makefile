SHELL := /bin/bash
MY_IP ?= $$(ifconfig | grep -Eo 'inet (addr:)?([0-9]*\.){3}[0-9]*' | grep -Eo '([0-9]*\.){3}[0-9]*' | grep -v '127.0.0.1' | head -n 1)

.PHONY: deps

deps:
	@MY_IP=${MY_IP} ./start.sh
	@sleep 5
	@make ecto

ecto:
	@mix ecto.setup

run:
	@mix phx.server