defmodule PongBackendWeb.SessionView do
  use PongBackendWeb, :view

  def render("sign_in.json", %{player: player, jwt: jwt}) do
    %{
      status: :ok,
      data: %{
        token: jwt,
        id: player.id
      },
      message: "You are successfully logged in! Add this token to authorization header to make authorized requests."
    }
  end

end