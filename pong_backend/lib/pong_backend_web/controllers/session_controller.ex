defmodule PongBackendWeb.SessionController do
  use PongBackendWeb, :controller

  alias PongBackend.Accounts

  def sign_in(conn, %{"id" => id}) do
    player = Accounts.get_player!(id)
    {:ok, jwt, _full_claims} = PongBackend.Guardian.encode_and_sign(player, %{}, permissions: %{player: []})

    conn
    |> put_status(200)
    |> render("sign_in.json", player: player, jwt: jwt)
  end  
end