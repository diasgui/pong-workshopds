defmodule PongBackendWeb.RegistrationView do
  use PongBackendWeb, :view

  def render("success.json", %{player: player}) do
    %{
      status: :ok,
      id: player.id,
      message: """
        Now you can sign in using your id at /api/sign_in. You will receive JWT token.
        Please put this token into Authorization header for all authorized requests.
      """
    }
  end
end