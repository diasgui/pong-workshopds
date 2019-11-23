# This file is responsible for configuring your application
# and its dependencies with the aid of the Mix.Config module.
#
# This configuration file is loaded before any dependency and
# is restricted to this project.

# General application configuration
use Mix.Config

config :pong_backend,
  ecto_repos: [PongBackend.Repo]

# Configures the endpoint
config :pong_backend, PongBackendWeb.Endpoint,
  url: [host: "localhost"],
  secret_key_base: "vCxghmAbOM/Z+mmQEnmAHokUeBX4/kHTxNeWoV6VPrCsgkibZuTT9Aaz1VkU1e5D",
  render_errors: [view: PongBackendWeb.ErrorView, accepts: ~w(json)],
  pubsub: [name: PongBackend.PubSub, adapter: Phoenix.PubSub.PG2]

# Configures Elixir's Logger
config :logger, :console,
  format: "$time $metadata[$level] $message\n",
  metadata: [:request_id]

# Use Jason for JSON parsing in Phoenix
config :phoenix, :json_library, Jason

# Configures the Guardian
config :economizai, PongBackend.Guardian,
  allowed_algos: ["HS512"], # optional
  verify_module: Guardian.JWT,  # optional
  issuer: "PongBackend",
  ttl: { 30, :days },
  allowed_drift: 2000,
  verify_issuer: true, # optional
  secret_key: "FxJkz+zrXPDyqPGnQY/9zZg95zMZGxhHPMyWtPljxuriY0ATI74R7AZU3UNBi6Ux" # Insert previously generated secret key!

# Import environment specific config. This must remain at the bottom
# of this file so it overrides the configuration defined above.
import_config "#{Mix.env()}.exs"
