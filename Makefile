DOTNET        := dotnet
INFRA_PROJECT := UserManagement.Infrastructure
STARTUP_PROJECT := UserManagement.WebApi
MIGRATION_NAME ?= InitialCreate

.PHONY: help migrations-add migrations-push migrations-list migrations-remove migrations-script

help: ## Show available targets
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "  \033[36m%-20s\033[0m %s\n", $$1, $$2}'

migrations-add: ## Generate a new migration, e.g. make migrations-add MIGRATION_NAME=AddJabatan
	$(DOTNET) ef migrations add $(MIGRATION_NAME) \
		--project $(INFRA_PROJECT) \
		--startup-project $(STARTUP_PROJECT) \
		--output-dir Migrations

migrations-push: ## Apply pending migrations to the database
	$(DOTNET) ef database update \
		--project $(INFRA_PROJECT) \
		--startup-project $(STARTUP_PROJECT)

migrations-list: ## List applied and pending migrations
	$(DOTNET) ef migrations list \
		--project $(INFRA_PROJECT) \
		--startup-project $(STARTUP_PROJECT)

migrations-remove: ## Remove the last migration (reverts model snapshot only)
	$(DOTNET) ef migrations remove \
		--project $(INFRA_PROJECT) \
		--startup-project $(STARTUP_PROJECT)

migrations-script: ## Generate a SQL script of the pending changes, e.g. make migrations-script SCRIPT=upgrade.sql
	$(DOTNET) ef migrations script \
		--project $(INFRA_PROJECT) \
		--startup-project $(STARTUP_PROJECT) \
		-o $(SCRIPT)
