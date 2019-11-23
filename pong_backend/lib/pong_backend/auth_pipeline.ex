defmodule PongBackend.AuthPipeline do
  @claims %{typ: "access"}

  use Guardian.Plug.Pipeline,
    otp_app: :pong_backend,
    module: PongBackend.Guardian,
    error_handler: PongBackend.AuthErrorHandler

  plug(Guardian.Plug.VerifySession, claims: @claims)
  plug(Guardian.Plug.VerifyHeader, claims: @claims, realm: "Bearer")
  plug(Guardian.Plug.EnsureAuthenticated)
  plug(Guardian.Plug.LoadResource, ensure: true)
end