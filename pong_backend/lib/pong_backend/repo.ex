defmodule PongBackend.Repo do
  use Ecto.Repo,
    otp_app: :pong_backend,
    adapter: Ecto.Adapters.Postgres
end
