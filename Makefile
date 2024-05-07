
up:
	docker compose --profile prod up -d
down:
	docker compose --profile prod down
watch:
	docker compose --profile dev -f docker-compose.yaml -f docker-compose.dev.yaml up -d --build
	cd app; dotnet watch 