defmodule PongBackendWeb.RegistrationView do
  use PongBackendWeb, :view

  def render("success.json", %{player: player, jwt: jwt}) do
    %{
      status: :ok,
      data: %{
        token: jwt,
        id: player.id,
        wins: player.wins,
        losses: player.losses
      },
      message: """
        Now you can sign in using your id at /api/sign_in. You will receive JWT token.
        Please put this token into Authorization header for all authorized requests.
      """
    }
  end
end