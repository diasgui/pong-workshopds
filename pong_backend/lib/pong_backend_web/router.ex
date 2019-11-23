defmodule PongBackendWeb.Router do
  use PongBackendWeb, :router

  pipeline :api do
    plug :accepts, ["json"]
  end

  scope "/api", PongBackendWeb do
    pipe_through :api
  end
end
