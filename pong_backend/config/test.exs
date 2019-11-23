use Mix.Config

# Configure your database
config :pong_backend, PongBackend.Repo,
  username: "postgres",
  password: "postgres",
  database: "pong_backend_test",
  hostname: "localhost",
  pool: Ecto.Adapters.SQL.Sandbox

# We don't run a server during test. If one is required,
# you can enable the server option below.
config :pong_backend, PongBackendWeb.Endpoint,
  http: [port: 4002],
  server: false

# Print only warnings and errors during test
config :logger, level: :warn
