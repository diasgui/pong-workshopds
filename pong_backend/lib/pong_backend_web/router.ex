defmodule PongBackendWeb.Router do
  use PongBackendWeb, :router

  pipeline :api do
    plug :accepts, ["json"]
  end

  pipeline :authenticated do
    plug PongBackend.AuthPipeline
  end

  scope "/api", PongBackendWeb do
    pipe_through :api
    post "/sign_up", RegistrationController, :sign_up
    post "/sign_in", SessionController, :sign_in
  end
end
